using System;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using NationalUnion.Application;
using Unity.Mvc4;

namespace NationalUnion.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 启用压缩
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // 注入IoC
            // Added by jiangew 2014.06.10
            var container = new UnityContainer();
            UnityDependencyRegister.ContainerRegister(ref container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError().GetBaseException();

            var sbError = new StringBuilder();
            sbError.Append("Global里捕捉的异常,");
            sbError.Append("Url:");
            sbError.Append(Request.Url.ToString());
            sbError.Append(",异常内容:");
            sbError.Append(ex.Message);

            NationalUnion.Common.Log.Logger.LogError(ex, "URL:" + Request.Url, string.Empty, sbError.ToString());
            Server.ClearError();
           // Server.Transfer("~/Html/Error.html");
        }

        ///// <summary>
        ///// 从Cookie中读取国美在线的MemberNO,如果读取无效，则返回-1
        ///// </summary>
        //string GomeUserId()
        //{
        //    HttpCookie vMemberNoCookie = Request.Cookies[Common.GOMEMEMBERNO];
        //    if (vMemberNoCookie == null)
        //    {
        //        return "-1";
        //    }

        //    return vMemberNoCookie.Value;
        //}

        ///// <summary>
        ///// 访客IP
        ///// </summary>
        ///// <returns></returns>
        //string IpAddress()
        //{
        //    string result = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //    if (string.IsNullOrEmpty(result))
        //    {
        //        result = Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    if (string.IsNullOrEmpty(result))
        //    {
        //        result = Request.UserHostAddress;
        //    }

        //    return result;
        //}
    }
}