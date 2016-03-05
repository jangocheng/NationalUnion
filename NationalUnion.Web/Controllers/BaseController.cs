using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class BaseController : Controller
    {

        //[Dependency]
        //public IManagerAccountBll ManagerAccountBll { get; set; }

        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public Int64 GetUserId()
        {
            if (Session["Account"] != null)
            {
                var account = (ManagerAccount)Session["Account"];

                return account.ManagerId;
            }

            //if (Request.Cookies["memberNo"] != null)
            //{
            //    var memberNoCookie = Request.Cookies["memberNo"].Value;
            //    var memberNo = Int64.Parse(memberNoCookie);

            //    return memberNo;
            //}

            return 0;
        }

        /// <summary>
        /// 获取当前用户Name
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            if (Session["Account"] != null)
            {
                var account = (ManagerAccount)Session["Account"];

                return account.ManagerName;
            }

            //if (Request.Cookies["memberNo"] != null)
            //{
            //    var memberNoCookie = Request.Cookies["memberNo"].Value;
            //    var memberNo = Int64.Parse(memberNoCookie);
            //    var manager = ManagerAccountBll.GetManagerById(memberNo);

            //    return manager.ManagerName;
            //}

            return string.Empty;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public ManagerAccount GetAccountInfo()
        {
            if (Session["Account"] != null)
            {
                return (ManagerAccount)Session["Account"];
            }

            //if (Request.Cookies["memberNo"] != null)
            //{
            //    var memberNoCookie = Request.Cookies["memberNo"].Value;
            //    var memberNo = Int64.Parse(memberNoCookie);
            //    var manager = ManagerAccountBll.GetManagerById(memberNo);

            //    return manager;
            //}

            return null;
        }

        /// <summary>
        /// 重载Json，返回ToJsonResult
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ToJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        /// <summary>
        /// 获取当前页或操作访问权限
        /// </summary>
        /// <returns></returns>
        public List<GetPermissionCode> GetPermission()
        {
            string filePath = HttpContext.Request.FilePath;
            var permis = (List<GetPermissionCode>) Session["filePath"];

            return permis;
        }
    }
}
