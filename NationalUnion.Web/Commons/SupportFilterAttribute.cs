using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Implements;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Repositories;

namespace NationalUnion.Web.Commons
{
    public class SupportFilterAttribute : ActionFilterAttribute
    {
        //[Dependency]
        //public IChannelPermissionBll ChannelPermissionBll { get; set; }

        public string ActionName { get; set; }
        private string _area;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// Action加上[SupportFilter]在执行actin之前执行以下代码，通过[SupportFilter(ActionName="Index")]指定参数
        /// </summary>
        /// <param name="filterContext">页面传过来的上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 读取请求上下文中的Controller/Action/Id
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(filterContext.HttpContext);
            // 取出区域的控制器Action/Id
            string ctrlName = filterContext.Controller.ToString();
            string[] routeInfo = ctrlName.Split('.');
            string controller = null;
            string action = null;
            string id = null;

            int iAreas = Array.IndexOf(routeInfo, "Areas");
            if (iAreas > 0)
            {
                // 取区域及控制器
                _area = routeInfo[iAreas + 1];
            }
            int ctrlIndex = Array.IndexOf(routeInfo, "Controllers");
            ctrlIndex++;
            controller = routeInfo[ctrlIndex].Replace("Controller", "").ToLower();

            string url = HttpContext.Current.Request.Url.ToString().ToLower();
            string[] urlArray = url.Split('/');
            int urlCtrlIndex = Array.IndexOf(urlArray, controller);
            urlCtrlIndex++;
            if (urlArray.Count() > urlCtrlIndex)
            {
                action = urlArray[urlCtrlIndex];
            }
            urlCtrlIndex++;
            if (urlArray.Count() > urlCtrlIndex)
            {
                id = urlArray[urlCtrlIndex];
            }

            // url
            action = string.IsNullOrEmpty(action) ? "Index" : action;
            int actionIndex = action.IndexOf("?", 0, System.StringComparison.Ordinal);
            if (actionIndex > 1)
            {
                action = action.Substring(0, actionIndex);
            }
            id = string.IsNullOrEmpty(id) ? "" : id;

            // url路径
            string filePath = HttpContext.Current.Request.FilePath;
            if (filterContext.HttpContext.Session != null)
            {
                var account = filterContext.HttpContext.Session["Account"] as ManagerAccount;
                if (ValiddatePermission(account, controller, action, filePath))
                {
                    return;
                }
                else
                {
                    filterContext.Result = new EmptyResult();
                    return;
                }
            }
        }

        public bool ValiddatePermission(ManagerAccount account, string controller, string action, string filePath)
        {
            bool bResult = false;
            string actionName = string.IsNullOrEmpty(ActionName) ? action : ActionName;
            if (account != null)
            {
                List<GetPermissionCode> perm = null;
                // 测试当前controller是否已赋权限值，如果没有从
                // 如果存在区域,Seesion保存（区域+控制器）
                if (!string.IsNullOrEmpty(_area))
                {
                    controller = _area + "/" + controller;
                }
                perm = (List<GetPermissionCode>)HttpContext.Current.Session[filePath];
                if (perm == null)
                {
                    using (var channelPermissionBll = new ChannelPermissionBll()
                    {
                        ChannelPermissionRepository = new ChannelPermissionRepository()
                    })
                    {
                        perm = channelPermissionBll.GetPermission(account.ManagerId, controller);   // 获取当前用户的权限列表
                        HttpContext.Current.Session[filePath] = perm;                               // 获取的劝降放入会话由Controller调用
                    }

                    //perm = ChannelPermissionBll.GetPermission(account.ManagerId, controller);
                    //HttpContext.Current.Session[filePath] = perm;
                }
                // 当用户访问index时，只要权限大于0就可以访问
                if (actionName.ToLower() == "index")
                {
                    if (perm.Count > 0)
                    {
                        return true;
                    }
                }
                // 查询当前Action 是否有操作权限，大于0表示有，否则没有
                int count = perm.Count(a => a.KeyCode.ToLower() == actionName.ToLower());
                if (count > 0)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                    HttpContext.Current.Response.Write("您没有操作权限！");
                }
            }
            return bResult;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}