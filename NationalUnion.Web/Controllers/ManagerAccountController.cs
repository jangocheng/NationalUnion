using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Repository.Interfaces;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class ManagerAccountController : Controller
    {
        [Dependency]
        public IManagerAccountBll ManagerAccountBll { get; set; }
        [Dependency]
        public IRegionProvinceCityRepository RegionProvinceCityRepository { get; set; }
        [Dependency]
        public IChannelManageRepository ChannelManageRepository { get; set; }

        // 异常集合
        ValidationErrors _errors = new ValidationErrors();

        public ActionResult ManagerList()
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

        [HttpPost]
        public JsonResult GetManagerList(GridPager pager)
        {
            var list = ManagerAccountBll.GetManagerList(ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ManagerAccount
                    {
                        ManagerId = row.ManagerId,
                        ManagerName = row.ManagerName,
                        ManagerEmail = row.ManagerEmail,
                        MobilePhone = row.MobilePhone,
                        ChannelTypeDesc = EnumHelper.GetDescription((ChannelType)(row.ChannelType)),
                        ChannelName = row.ChannelName,
                        ChannelRankDesc = EnumHelper.GetDescription((ChannelRank)(row.ChannelRank)),
                        ParentId = row.ParentId,
                        ParentChannel = row.ParentId == 0 ? "" : ChannelManageRepository.GetChannelById(row.ParentId).ChannelName,
                        City = row.City,
                        Province = RegionProvinceCityRepository.GetProvinceById(row.ProvinceId).Name,
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
            var list = ManagerAccountBll.GetManagerListByQuery(queryStr, ref pager);

            var json = new
            {
                total = pager.TotalRows,
                rows = list.Select(row =>
                    new ManagerAccount
                    {
                        ManagerId = row.ManagerId,
                        ManagerName = row.ManagerName,
                        ManagerEmail = row.ManagerEmail,
                        MobilePhone = row.MobilePhone,
                        ChannelTypeDesc = EnumHelper.GetDescription((ChannelType) (row.ChannelType)),
                        ChannelName = row.ChannelName,
                        ChannelRankDesc = EnumHelper.GetDescription((ChannelRank) (row.ChannelRank)),
                        ParentId = row.ParentId,
                        ParentChannel = row.ParentId == 0 ? "" : ChannelManageRepository.GetChannelById(row.ParentId).ChannelName,
                        City = row.City,
                        Province = RegionProvinceCityRepository.GetProvinceById(row.ProvinceId).Name,
                        CreatedTime = row.CreatedTime,
                        ModifiedTime = row.ModifiedTime,
                        IsActive = row.IsActive,
                        IsActiveDesc = EnumHelper.GetDescription((Status) (row.IsActive))
                    }).ToArray()
            };

            return new ToJsonResult
            {
                Data = json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult AddManager()
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

            var provinceList = RegionProvinceCityRepository.GetProvinceList();
            var provSelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "请选择", Value = "-1", Selected = true}
            };
            provSelectItems.AddRange(from prov in provinceList
                select new SelectListItem
                {
                    Text = prov.Name,
                    Value = prov.PId.ToString(CultureInfo.InvariantCulture),
                    Selected = false
                });

            ViewData["ProvList"] = provSelectItems;

            var cityList = RegionProvinceCityRepository.GetCityListByProvId(0);
            var citySelectItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "请选择", Value = "-1", Selected = true}
            };
            citySelectItems.AddRange(from city in cityList
                select new SelectListItem
                {
                    Text = city.Name,
                    Value = city.CId.ToString(CultureInfo.InvariantCulture),
                    Selected = false
                });

            ViewData["CityList"] = citySelectItems;

            return View();
        }

        [HttpPost]
        public JsonResult AddManager(ManagerAccount model)
        {
            if (ManagerAccountBll.AddManager(model, ref _errors))
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

        public ActionResult EditManager(Int64 managerId)
        {
            ManagerAccount entity = ManagerAccountBll.GetManagerById(managerId);

            var typeSelectItems = new List<SelectListItem>();
            typeSelectItems.AddRange(from object value in Enum.GetValues(typeof (ChannelType))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((ChannelType) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["ChannelType"] = typeSelectItems;

            var statusSelectItems = new List<SelectListItem>();
            statusSelectItems.AddRange(from object value in Enum.GetValues(typeof (Status))
                select new SelectListItem
                {
                    Text = EnumHelper.GetDescription((Status) (value)),
                    Value = value.ToString(),
                    Selected = false
                });

            ViewData["Status"] = statusSelectItems;

            var provinceList = RegionProvinceCityRepository.GetProvinceList();
            var provSelectItems = new List<SelectListItem>();
            provSelectItems.AddRange(from prov in provinceList
                select new SelectListItem
                {
                    Text = prov.Name,
                    Value = prov.PId.ToString(CultureInfo.InvariantCulture),
                    Selected = false
                });

            ViewData["ProvList"] = provSelectItems;

            var cityList = RegionProvinceCityRepository.GetCityListByProvId(entity.ProvinceId);
            var citySelectItems = new List<SelectListItem>();
            citySelectItems.AddRange(from city in cityList
                select new SelectListItem
                {
                    Text = city.Name,
                    Value = city.CId.ToString(CultureInfo.InvariantCulture),
                    Selected = false
                });

            ViewData["CityList"] = citySelectItems;

            entity.ChannelTypeStr = Enum.Parse(typeof(ChannelType), (entity.ChannelType).ToString(CultureInfo.InvariantCulture)).ToString();
            entity.IsActiveStr = Enum.Parse(typeof (Status), (entity.IsActive).ToString(CultureInfo.InvariantCulture)).ToString();
            return View(entity);
        }

        [HttpPost]
        public JsonResult EditManager(ManagerAccount model)
        {
            if (ManagerAccountBll.EditManager(model))
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
        public ActionResult Search(string channelType, string keyword)
        {
            var items = new List<object>();
            var channelList = ChannelManageRepository.GetChannelList();
            if (!channelType.Equals("-1"))
            {
                int type = Convert.ToInt32(Enum.Parse(typeof(ChannelType), channelType));
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
        public JsonResult GetCityList(int pid)
        {
            var cityList = RegionProvinceCityRepository.GetCityListByProvId(pid);
            var citySelectItems = new List<SelectListItem>();
            citySelectItems.AddRange(from city in cityList
                select new SelectListItem
                {
                    Text = city.Name,
                    Value = city.CId.ToString(CultureInfo.InvariantCulture),
                    Selected = false
                });

            return Json(citySelectItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailManager(Int64 managerId)
        {
            ManagerAccount entity = ManagerAccountBll.GetManagerById(managerId);

            var citySelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = entity.City,
                    Value = entity.CityId.ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["CityList"] = citySelectItems;

            var provSelectItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = entity.Province,
                    Value = entity.ProvinceId.ToString(CultureInfo.InvariantCulture),
                    Selected = true
                }
            };

            ViewData["ProvList"] = provSelectItems;

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

            return View(entity);
        }

        [HttpPost]
        public JsonResult DeleteManager(Int64 managerId)
        {
            if (ManagerAccountBll.DeleteManager(managerId))
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
        public JsonResult ChangeAccountStatus(Int64 managerId)
        {
            if (ManagerAccountBll.ChangeAccountStatus(managerId))
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

        //public ActionResult ResetPassword(Int64 managerId)
        //{
        //    ManagerAccount entity = ManagerAccountBll.GetManagerById(managerId);

        //    return View(entity);
        //}

        //[HttpPost]
        //public JsonResult ResetPassword(ManagerAccount model)
        //{
        //    if (ManagerAccountBll.ResetPassword(model))
        //    {
        //        return new ToJsonResult
        //        {
        //            Data = 1,
        //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //        };
        //    }
        //    return new ToJsonResult
        //    {
        //        Data = 0,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}
    }
}
