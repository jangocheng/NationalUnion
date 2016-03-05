using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NationalUnion.Web.Commons
{
    public class ToJsonResult : JsonResult
    {
        const string Error = "为了可以GET请求，请设置JsonRequestBehavior AllowGet！";

        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormateStr { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(Error);
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                var serializer = new JavaScriptSerializer();
                string jsonstring = serializer.Serialize(Data);


                //string p = @"\\/Date\((\d+)\+\d+\)\\/";

                const string p = @"\\/Date\(\d+\)\\/";

                var matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);

                var reg = new Regex(p);

                jsonstring = reg.Replace(jsonstring, matchEvaluator);
                response.Write(jsonstring);
            }
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private string ConvertJsonDateToDateString(Match m)
        {

            string result = string.Empty;

            const string p = @"\d";
            var cArray = m.Value.ToCharArray();
            var sb = new StringBuilder();

            var reg = new Regex(p);
            foreach (char arr in cArray)
            {
                if (reg.IsMatch(arr.ToString(CultureInfo.InvariantCulture)))
                {
                    sb.Append(arr);
                }
            }
            // reg.Replace(m.Value;

            var dt = new DateTime(1970, 1, 1);

            dt = dt.AddMilliseconds(long.Parse(sb.ToString()));

            dt = dt.ToLocalTime();

            result = dt.ToString("yyyy-MM-dd HH:mm:ss");

            return result;
        }
    }
}