using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Web.HtmlHelpers
{
    public static class ExtendMvcHtml
    {
        /// <summary>
        /// 普通按钮
        /// </summary>
        /// <param name="helper">htmlhelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="icon">控件icon图标class</param>
        /// <param name="text">控件的名称</param>
        /// <param name="hr">分割线</param>
        /// <returns>html</returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text, bool hr)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<a id=\"{0}\" style=\"float: left;\" class=\"l-btn l-btn-plain\">", id);
            sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">", icon);
            sb.AppendFormat("{0}</span></span></a>", text);
            if (hr)
            {
                sb.Append("<div class=\"datagrid-btn-separator\"></div>");
            }

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// 权限按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        /// <param name="permis"></param>
        /// <param name="keycode"></param>
        /// <param name="hr"></param>
        /// <returns></returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string icon, string text,
            List<GetPermissionCode> permis, string keycode, bool hr)
        {
            if (permis.Count(a => a.KeyCode == keycode) > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<a id=\"{0}\" style=\"float: left;\" class=\"l-btn l-btn-plain\">", id);
                sb.AppendFormat("<span class=\"l-btn-left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">", icon);
                sb.AppendFormat("{0}</span></span></a>", text);
                if (hr)
                {
                    sb.Append("<div class=\"datagrid-btn-separator\"></div>");
                }

                return new MvcHtmlString(sb.ToString());
            }

            return new MvcHtmlString("");
        }
    }
}