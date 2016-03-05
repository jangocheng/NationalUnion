using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Appender;
using log4net.Layout;


namespace NationalUnion.Common.Log
{
    public static class Logger
    {
        static ILog log;

        static Logger() 
        {
            string strPath = HttpContext.Current.Server.MapPath("//"); 
            strPath = System.IO.Path.Combine(strPath, "log");
            strPath = System.IO.Path.Combine(strPath, "log4net.config");
            var log4File = new System.IO.FileInfo(strPath);
            log4net.Config.XmlConfigurator.Configure(log4File); 
            log = LogManager.GetLogger("AdoNet");
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="argUserID"></param>
        /// <param name="argIP"></param>
        /// <param name="argDescription"></param>
        /// <param name="argMethod"></param>
        public static void LogInfo(string argUserID = "", string argIP = "", string argDescription = "", string argMethod = "")
        {
            log.Info(new { UserID = argUserID, IP = argIP, Description = argDescription, Method = argMethod });
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="argObj"></param>
        public static void LogInfo(object argObj)
        {
            log.Info(new { argObj });
        }

        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="argEx">异常信息</param>
        /// <param name="argUserID">用户ID</param>
        /// <param name="argIP">访问者IP</param>
        /// <param name="argDescription">描述</param>
        public static void LogError(System.Exception argEx, string argUserID = "", string argIP = "", string argDescription = "")
        {
            log.Error(new { UserID = argUserID, IP = argIP, Description = argDescription }, argEx);
        }

        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="argEx"></param>
        /// <param name="argObj"></param>
        public static void LogError(System.Exception argEx, object argObj)
        {
            log.Error(argObj, argEx);
        }
    }
}