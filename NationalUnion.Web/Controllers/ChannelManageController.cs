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
using NationalUnion.Repository.Interfaces;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class ChannelManageController : Controller
    {
        [Dependency]
        public IChannelManageBll ChannelManageBll { get; set; }
        [Dependency]
        public IChannelManageRepository ChannelManageRepository { get; set; }

        public ActionResult ChannelList()
        {
            var typeSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "渠道类型", Value = "-1", Selected = true}
            };
            typeSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelType) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["ChannelType"] = typeSelectItems;

            var rankSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "渠道等级", Value = "-1", Selected = true}
            };
            rankSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelRank))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Rank"] = rankSelectItems;

            var statusSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "渠道状态", Value = "-1", Selected = true}
            };
            statusSelectItems.AddRange(from object value in Enum.GetValues(typeof (Status))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((Status) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Status"] = statusSelectItems;

            return View();
        }

        public JsonResult GetChannelList(GridPager pager)
        {
            var list = ChannelManageBll.GetChannelList(ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ChannelInfo
                    {
                        ChannelId = row.ChannelId,
                        ChannelName = row.ChannelName,
                        RankDesc = EnumHelper.GetDescription((ChannelRank)(row.Rank)),
                        ChannelTypeDesc = EnumHelper.GetDescription((ChannelType)(row.ChannelType)),
                        ParentId = row.ParentId,
                        ParentChannel = row.ParentId == 0 ? "" : ChannelManageBll.GetChannelById(row.ParentId).ChannelName,
                        KeyWord = row.KeyWord,
                        CreatedTime = row.CreatedTime,
                        ModifiedTime = row.ModifiedTime,
                        IsActive = row.IsActive,
                        IsActiveDesc = EnumHelper.GetDescription((Status)(row.IsActive))
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
            var list = ChannelManageBll.GetChannelListByQuery(queryStr, ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ChannelInfo
                    {
                        ChannelId = row.ChannelId,
                        ChannelName = row.ChannelName,
                        RankDesc = EnumHelper.GetDescription((ChannelRank)(row.Rank)),
                        ChannelTypeDesc = EnumHelper.GetDescription((ChannelType)(row.ChannelType)),
                        ParentId = row.ParentId,
                        ParentChannel = row.ParentId == 0 ? "" : ChannelManageBll.GetChannelById(row.ParentId).ChannelName,
                        KeyWord = row.KeyWord,
                        CreatedTime = row.CreatedTime,
                        ModifiedTime = row.ModifiedTime,
                        IsActive = row.IsActive,
                        IsActiveDesc = EnumHelper.GetDescription((Status)(row.IsActive))
                    }).ToArray()
            };

            return new ToJsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult AddChannel()
        {
            var typeSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "请选择", Value = "-1", Selected = true}
            };
            typeSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelType) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["ChannelType"] = typeSelectItems;

            var rankSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "请选择", Value = "-1", Selected = true}
            };
            rankSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelRank))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Rank"] = rankSelectItems;

            var statusSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "请选择", Value = "-1", Selected = true}
            };
            statusSelectItems.AddRange(from object value in Enum.GetValues(typeof (Status))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((Status) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Status"] = statusSelectItems;

            return View();
        }

        [HttpPost]
        public ActionResult Search(string channelType, string keyword)
        {
            var items = new List<object>();
            var channelList = ChannelManageRepository.GetChannelList();
            if (!channelType.Equals("-1"))
            {
                int type = Convert.ToInt32(Enum.Parse(typeof (ChannelType), channelType));
                channelList = channelList.Where(c => c.ChannelType == type);
            }
            var channelNameIdList = channelList.ToList().Where(c => c.ChannelName.Contains(keyword));
            items.AddRange(channelNameIdList.Select(c => new
            {
                text = c.ChannelName + "/" + c.ChannelId,
                value = c.ChannelId.ToString(CultureInfo.InvariantCulture)
            }));

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddChannel(ChannelInfo model)
        {
            if (ChannelManageBll.AddChannel(model))
            {
                return new ToJsonResult
                {
                    Data = 1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new ToJsonResult
            {
                Data = 0,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult EditChannel(int channelId)
        {
            ChannelInfo entity = ChannelManageBll.GetChannelById(channelId);

            var typeSelectItems = new List<SelectListItem>();
            typeSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelType) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["ChannelType"] = typeSelectItems;

            var rankSelectItems = new List<SelectListItem>();
            rankSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelRank))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Rank"] = rankSelectItems;

            var statusSelectItems = new List<SelectListItem>();
            statusSelectItems.AddRange(from object value in Enum.GetValues(typeof (Status))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((Status) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Status"] = statusSelectItems;

            entity.ParentChannel = ChannelManageBll.GetChannelById(entity.ParentId).ChannelName;
            entity.RankStr = Enum.Parse(typeof(ChannelRank), (entity.Rank).ToString(CultureInfo.InvariantCulture)).ToString();
            entity.ChannelTypeStr = Enum.Parse(typeof (ChannelType), (entity.ChannelType).ToString(CultureInfo.InvariantCulture)).ToString();
            entity.IsActiveStr = Enum.Parse(typeof (Status), (entity.IsActive).ToString(CultureInfo.InvariantCulture)).ToString();

            return View(entity);
        }

        [HttpPost]
        public JsonResult EditChannel(ChannelInfo model)
        {
            if (ChannelManageBll.EditChannel(model))
            {
                return new ToJsonResult
                {
                    Data = 1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new ToJsonResult
            {
                Data = 0,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult DetailChannel(int channelId)
        {
            ChannelInfo entity = ChannelManageBll.GetChannelById(channelId);

            var rankSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelRank) (entity.Rank)),
                    Value = (entity.Rank).ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["Rank"] = rankSelectItems;

            var typeSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelType)(entity.ChannelType)),
                    Value = (entity.ChannelType).ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["ChannelType"] = typeSelectItems;

            var statusSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = EnumHelper.GetDescription((Status) (entity.IsActive)),
                    Value = (entity.IsActive).ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["Status"] = statusSelectItems;

            entity.ParentChannel = ChannelManageBll.GetChannelById(entity.ParentId).ChannelName;

            return View(entity);
        }

        [HttpPost]
        public JsonResult DeleteChannel(int channelId)
        {
            if (ChannelManageBll.DeleteChannel(channelId))
            {
                return new ToJsonResult
                {
                    Data = 1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new ToJsonResult
            {
                Data = 0,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult ChangeStatus(int channelId)
        {
            if (ChannelManageBll.ChangeStatus(channelId))
            {
                return new ToJsonResult
                {
                    Data = 1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new ToJsonResult
            {
                Data = 0,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
