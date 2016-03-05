using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Implements;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Common.Utility;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class ShareDetailController : Controller
    {
        [Dependency]
        public IShareDetailBll ShareDetailBll { get; set; }

        public ActionResult ShareDetailList()
        {
            var sharedPlatformSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "分享平台", Value = "-1", Selected = true}
            };
            sharedPlatformSelectItems.AddRange(from object value in Enum.GetValues(typeof (EnumUtility.SharedPlatform))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((EnumUtility.SharedPlatform) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["SharedPlatform"] = sharedPlatformSelectItems;

            var sharedClientTypeSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "分享途径", Value = "-1", Selected = true}
            };
            sharedClientTypeSelectItems.AddRange(
                from object value in Enum.GetValues(typeof (EnumUtility.SharedClientType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((EnumUtility.SharedClientType) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["SharedClientType"] = sharedClientTypeSelectItems;

            var orderTypeSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "下单类型", Value = "-1", Selected = true}
            };
            orderTypeSelectItems.AddRange(
                from object value in Enum.GetValues(typeof(EnumUtility.OrderType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((EnumUtility.OrderType)(value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["OrderType"] = orderTypeSelectItems;

            return View();
        }

        [HttpPost]
        public JsonResult GetShareDetailList(GridPager pager)
        {
            var list = ShareDetailBll.GetShareDetailList(ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new OrderProductOccurInfo
                    {
                        OrderId = row.OrderId,
                        ProductSkuId = row.ProductSkuId,
                        CategoryId = row.CategoryId,
                        CategoryName = ShareDetailBll.GetProductType(row.CategoryId) == null
                            ? "" : ShareDetailBll.GetProductType(row.CategoryId).ProTypeName,
                        CategoryParentName = ShareDetailBll.GetProductType(row.CategoryId) == null
                            ? "" : ShareDetailBll.GetProductType(ShareDetailBll.GetProductType(row.CategoryId).Pid).ProTypeName,
                        CategoryFinalName = ShareDetailBll.GetProductType(row.CategoryId) == null
                                ? "" : ShareDetailBll.GetProductType(ShareDetailBll.GetProductType(row.CategoryId).FinalPid).ProTypeName,
                        ProductPrice = row.ProductPrice,
                        ProductNumber = row.ProductNumber,
                        ProductName = row.ProductName,
                        ProductPriceAmount = row.ProductPriceAmount,
                        ProductSite = EnumHelper.GetDescription((EnumUtility.OrderType)(Enum.Parse(typeof(EnumUtility.OrderType), row.ProductSite))),
                        IsCoupon = "",
                        SharedUserId = row.SharedUserId,
                        SharedUserName = ShareDetailBll.GetUserById(row.SharedUserId) == null
                                ? "" : ShareDetailBll.GetUserById(row.SharedUserId).RealName,
                        ShareLevel = EnumHelper.GetDescription((EnumUtility.ShareLevel)(row.SharedLevel)),
                        Commission = row.Commission,
                        CommissionRatio = row.CommissionRatio,
                        PlatformId = row.PlatformId,
                        PlatformName = EnumHelper.GetDescription((EnumUtility.SharedPlatform) (row.PlatformId)),
                        SharedClientId = row.SharedClientId,
                        SharedClient = EnumHelper.GetDescription((EnumUtility.SharedClientType) (row.SharedClientId)),
                        OrderOccurTime = row.OrderOccurTime
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
            var list = ShareDetailBll.GetShareDetailListByQuery(queryStr, ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new OrderProductOccurInfo
                    {
                        OrderId = row.OrderId,
                        ProductSkuId = row.ProductSkuId,
                        CategoryId = row.CategoryId,
                        CategoryName = ShareDetailBll.GetProductType(row.CategoryId) == null
                            ? "" : ShareDetailBll.GetProductType(row.CategoryId).ProTypeName,
                        CategoryParentName = ShareDetailBll.GetProductType(row.CategoryId) == null
                            ? "" : ShareDetailBll.GetProductType(ShareDetailBll.GetProductType(row.CategoryId).Pid).ProTypeName,
                        CategoryFinalName = ShareDetailBll.GetProductType(row.CategoryId) == null
                                ? "" : ShareDetailBll.GetProductType(ShareDetailBll.GetProductType(row.CategoryId).FinalPid).ProTypeName,
                        ProductPrice = row.ProductPrice,
                        ProductNumber = row.ProductNumber,
                        ProductName = row.ProductName,
                        ProductPriceAmount = row.ProductPriceAmount,
                        ProductSite = EnumHelper.GetDescription((EnumUtility.OrderType)(Enum.Parse(typeof(EnumUtility.OrderType), row.ProductSite))),
                        IsCoupon = "",
                        SharedUserId = row.SharedUserId,
                        SharedUserName = ShareDetailBll.GetUserById(row.SharedUserId) == null ? "" : ShareDetailBll.GetUserById(row.SharedUserId).RealName,
                        ShareLevel = EnumHelper.GetDescription((EnumUtility.ShareLevel)(row.SharedLevel)),
                        Commission = row.Commission,
                        CommissionRatio = row.CommissionRatio,
                        PlatformId = row.PlatformId,
                        PlatformName = EnumHelper.GetDescription((EnumUtility.SharedPlatform)(row.PlatformId)),
                        SharedClientId = row.SharedClientId,
                        SharedClient = EnumHelper.GetDescription((EnumUtility.SharedClientType)(row.SharedClientId)),
                        OrderOccurTime = row.OrderOccurTime
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
