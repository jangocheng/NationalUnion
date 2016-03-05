using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Common.Utility;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class ShareStatisticsController : Controller
    {
        [Dependency]
        public IShareStatisticsBll ShareStatisticsBll { get; set; }
        [Dependency]
        public IChannelManageBll ChannelManageBll { get; set; }

        public ActionResult StatisticsList()
        {
            var sharedPlatformSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "分享平台", Value = "-1", Selected = true}
            };
            sharedPlatformSelectItems.AddRange(from object value in Enum.GetValues(typeof(EnumUtility.SharedPlatform))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((EnumUtility.SharedPlatform)(value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["SharedPlatform"] = sharedPlatformSelectItems;

            var sharedClientTypeSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "分享途径", Value = "-1", Selected = true}
            };
            sharedClientTypeSelectItems.AddRange(from object value in Enum.GetValues(typeof(EnumUtility.SharedClientType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((EnumUtility.SharedClientType)(value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["SharedClientType"] = sharedClientTypeSelectItems;

            return View();
        }

        [HttpPost]
        public JsonResult GetStatisticsList(GridPager pager)
        {
            var list = ShareStatisticsBll.GetShareStatisticsesList(ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ShareStatisticsInfo
                    {
                        UserId = row.UserId,
                        UserName = ShareStatisticsBll.GetUserById(row.UserId) == null ? "" : ShareStatisticsBll.GetUserById(row.UserId).RealName,
                        SummaryDate = row.SummaryDate,
                        ChannelId = row.ChannelId,
                        ChannelName = ChannelManageBll.GetChannelById(row.ChannelId).ChannelName,
                        PlatformId = row.PlatformId,
                        PlatformName = EnumHelper.GetDescription((EnumUtility.SharedPlatform) (row.PlatformId)),
                        SharedClientId = row.SharedClientId,
                        SharedClientName = EnumHelper.GetDescription((EnumUtility.SharedClientType) (row.SharedClientId)),
                        SharedQunatity = row.SharedQunatity,
                        PV = row.PV,
                        UV = row.UV,
                        OrderQuantity = row.OrderQuantity,
                        ProductQuantity = row.ProductQuantity,
                        OrderAmount = row.OrderAmount,
                        EstimateCommission = row.EstimateCommission
                    }).ToArray()
            };

            return new ToJsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult GetListByQuery(string queryStr, GridPager pager)
        {
            var list = ShareStatisticsBll.GetShareStatisticsesListByQuery(queryStr, ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ShareStatisticsInfo
                    {
                        UserId = row.UserId,
                        UserName = ShareStatisticsBll.GetUserById(row.UserId) == null ? "" : ShareStatisticsBll.GetUserById(row.UserId).RealName,
                        SummaryDate = row.SummaryDate,
                        ChannelId = row.ChannelId,
                        ChannelName = ChannelManageBll.GetChannelById(row.ChannelId).ChannelName,
                        PlatformId = row.PlatformId,
                        PlatformName = EnumHelper.GetDescription((EnumUtility.SharedPlatform) (row.PlatformId)),
                        SharedClientId = row.SharedClientId,
                        SharedClientName = EnumHelper.GetDescription((EnumUtility.SharedClientType) (row.SharedClientId)),
                        SharedQunatity = row.SharedQunatity,
                        PV = row.PV,
                        UV = row.UV,
                        OrderQuantity = row.OrderQuantity,
                        ProductQuantity = row.ProductQuantity,
                        OrderAmount = row.OrderAmount,
                        EstimateCommission = row.EstimateCommission
                    }).ToArray()
            };

            return new ToJsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
