using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class ManagerDetailController : Controller
    {
        [Dependency]
        public IManagerDetailBll ManagerDetailBll { get; set; }
        [Dependency]
        public IChannelManageBll ChannelManageBll { get; set; }

        public ActionResult ManagerDetailList()
        {
            var currentRankSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "当前渠道等级", Value = "-1", Selected = true}
            };
            currentRankSelectItems.AddRange(from object value in Enum.GetValues(typeof(ChannelRank))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank)(value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["CurrentRank"] = currentRankSelectItems;

            var oldRankSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "历史渠道等级", Value = "-1", Selected = true}
            };
            oldRankSelectItems.AddRange(from object value in Enum.GetValues(typeof(ChannelRank))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["OldRank"] = oldRankSelectItems;

            var rankChangeStatusSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "渠道等级状态", Value = "-1", Selected = true}
            };
            rankChangeStatusSelectItems.AddRange(from object value in Enum.GetValues(typeof(ChannelRankChangeStatus))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRankChangeStatus)(value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["ChannelRankChange"] = rankChangeStatusSelectItems;

            return View();
        }

        public JsonResult GetManagerDetailList(GridPager pager)
        {
            var list = ManagerDetailBll.GetManagerDetailList(ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ManagerDetailInfo
                    {
                        ManagerDetailId = row.ManagerDetailId,
                        ManagerId = row.ManagerId,
                        ManagerName = row.ManagerName,
                        CurrentChannelId = row.CurrentChannelId,
                        CurrentChannel = ChannelManageBll.GetChannelById(row.CurrentChannelId).ChannelName,
                        OldChannelId = row.OldChannelId,
                        OldChannel = ChannelManageBll.GetChannelById(row.OldChannelId).ChannelName,
                        CurrentRankDesc = EnumHelper.GetDescription((ChannelRank) (row.CurrentRank)),
                        OldRankDesc = EnumHelper.GetDescription((ChannelRank) (row.OldRank)),
                        RankStatusDesc = EnumHelper.GetDescription((ChannelRankChangeStatus) (row.RankStatus)),
                        CreatedTime = row.CreatedTime
                    }).ToArray()
            };

            return new ToJsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetListByQuery(string queryStr, GridPager pager)
        {
            var list = ManagerDetailBll.GetManagerDetailListByQuery(queryStr, ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ManagerDetailInfo
                    {
                        ManagerDetailId = row.ManagerDetailId,
                        ManagerId = row.ManagerId,
                        ManagerName = row.ManagerName,
                        CurrentChannelId = row.CurrentChannelId,
                        OldChannelId = row.OldChannelId,
                        CurrentRankDesc = EnumHelper.GetDescription((ChannelRank) (row.CurrentRank)),
                        OldRankDesc = EnumHelper.GetDescription((ChannelRank) (row.OldRank)),
                        RankStatusDesc = EnumHelper.GetDescription((ChannelRankChangeStatus) (row.RankStatus)),
                        CreatedTime = row.CreatedTime
                    }).ToArray()
            };

            return new ToJsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetManagerDetail(int managerDetailId)
        {
            ManagerDetailInfo entity = ManagerDetailBll.GetManagerDetailById(managerDetailId);
            var currentRankSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (entity.CurrentRank)),
                    Value = (entity.CurrentRank).ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["CurrentRank"] = currentRankSelectItems;

            var oldRankSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (entity.OldRank)),
                    Value = (entity.OldRank).ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["OldRank"] = oldRankSelectItems;

            var rankStatusSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRankChangeStatus) (entity.RankStatus)),
                    Value = (entity.RankStatus).ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["RankStatus"] = rankStatusSelectItems;

            return View(entity);
        }

        [HttpPost]
        public JsonResult DeleteManagerDetail(int managerDetailId)
        {
            if (ManagerDetailBll.DeleteManagerDetail(managerDetailId))
            {
                return new JsonResult
                {
                    Data = 1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = 0,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
