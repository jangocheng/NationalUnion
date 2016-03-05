using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace NationalUnion.Web.HtmlHelpers
{
    public static class ExSelectListItem
    {
        public static List<SelectListItem> ToSelectListItem(this Enum valueEnum)
        {
            var list = (Enum.GetValues(valueEnum.GetType())).Cast<int>().Select(value =>
                new SelectListItem
                {
                    Text = Enum.GetName(valueEnum.GetType(), value),
                    Value = value.ToString(CultureInfo.InvariantCulture)
                }).ToList();

            return list;
        }

        public static List<SelectListItem> ToSelectListItem(this Enum valueEnum, string selectName)
        {
            var list = (Enum.GetValues(valueEnum.GetType())).Cast<int>().Select(value =>
                new SelectListItem
                {
                    Text = Enum.GetName(valueEnum.GetType(), value),
                    Value = Enum.GetName(valueEnum.GetType(), value),
                    Selected = Enum.GetName(valueEnum.GetType(), value) == selectName ? true : false
                }).ToList();

            return list;
        }
        
        // Controller Usage
        //TempData["Status"] = (Status.Enable).ToSelectListItem("Enable");

        //var statusSelectItems = new List<SelectListItem>();
        //statusSelectItems.Add(new SelectListItem {Text = "请选择", Value = "-1", Selected = true});
        //foreach (var value in Enum.GetValues(typeof (Status)))
        //{
        //    var item = new SelectListItem();
        //    item.Text = EnumHelper.GetDescription((Status) (value));
        //    item.Value = value.ToString();
        //    item.Selected = false;

        //    statusSelectItems.Add(item);
        //}
    }
}