using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages.Scope;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;
using NationalUnion.Web.Commons;

namespace NationalUnion.Web.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IChannelMenuBll ChannelMenuBll { get; set; }
        [Dependency]
        public IChannelMoudleBll ChannelMoudleBll { get; set; }
        [Dependency]
        public IManagerAccountBll ManagerAccountBll { get; set; }
        [Dependency]
        public IRegionProvinceCityRepository RegionProvinceCityRepository { get; set; }
        [Dependency]
        public IChannelManageRepository ChannelManageRepository { get; set; }

        // 异常集合
        ValidationErrors _errors = new ValidationErrors();

        public ActionResult Index()
        {
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)System.Web.HttpContext.Current.Session["Account"];
            //    ViewData["Manager"] = account.ManagerName;

            //    return View();
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var manager = ManagerAccountBll.GetManagerById(memberNo);
                ViewData["Manager"] = manager.ManagerName;

                return View();
            }

            return View();
        }

        public ActionResult PerfectInfo()
        {
            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var manager = ManagerAccountBll.GetManagerById(memberNo);
                ViewData["Manager"] = manager.ManagerName;

                return View();
            }

            return View();
        }

        public JsonResult GetTree(string id)
        {
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)Session["Account"];

            //    List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByParentId(id);
            //    //List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByPersonName(account.ManagerName, id);
            //    //List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByPersonId(account.ManagerId, id);

            //    var jsonData = menus.Select(m => new
            //    {
            //        id = m.MoudleId,
            //        text = m.Name,
            //        value = m.Url,
            //        showcheck = false,
            //        complete = false,
            //        isexpand = false,
            //        checkstate = 0,
            //        hasChildren = !Convert.ToBoolean(m.IsLast),
            //        icon = m.Iconic
            //    }).ToArray();

            //    return Json(jsonData, JsonRequestBehavior.AllowGet);
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var manager = ManagerAccountBll.GetManagerById(memberNo);

                List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByParentId(id);
                //List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByPersonId(manager.ManagerId, id);

                var jsonData = menus.Select(m => new
                {
                    id = m.MoudleId,
                    text = m.Name,
                    value = m.Url,
                    showcheck = false,
                    complete = false,
                    isexpand = false,
                    checkstate = 0,
                    hasChildren = !Convert.ToBoolean(m.IsLast),
                    icon = m.Iconic
                }).ToArray();

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTreeByEasyUi(string id)
        {
            ////CultureInfo info = Thread.CurrentThread.CurrentCulture;
            ////string infoName = info.Name;
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)Session["Account"];

            //    List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByParentId(id);
            //    //List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByPersonId(account.ManagerId, id);

            //    var jsonData = from r in menus
            //        select new MoudleNavigationInfo()
            //        {
            //            id = r.MoudleId,
            //            //text = infoName.IndexOf("zh", System.StringComparison.Ordinal) > -1 || infoName == "" ? r.Name : r.EnglishName,
            //            text = r.Name,
            //            //attributes = (infoName.IndexOf("zh", System.StringComparison.Ordinal) > -1 || infoName == "" ? "zh-CN" : "en-US") + "/" + r.Url,
            //            attributes = r.Url,
            //            iconCls = r.Iconic,
            //            state = (ChannelMoudleBll.GetMoudleByParent(r.MoudleId).Count > 0) ? "closed" : "open"
            //        };

            //    return Json(jsonData);
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var manager = ManagerAccountBll.GetManagerById(memberNo);

                List<ChannelMoudle> menus = null;
                var administratorArr = ConfigurationManager.AppSettings["Administrator"].Split(new char[] {'|'});
                if (administratorArr.Contains(memberNo.ToString(CultureInfo.InvariantCulture)))
                {
                    menus = ChannelMenuBll.GetMenuByParentId(id);
                }
                else
                {
                    menus = ChannelMenuBll.GetTempMenuByParentId(id);
                }

                //List<ChannelMoudle> menus = ChannelMenuBll.GetMenuByPersonId(manager.ManagerId, id);

                var jsonData = from r in menus
                    select new MoudleNavigationInfo()
                    {
                        id = r.MoudleId,
                        text = r.Name,
                        attributes = r.Url,
                        iconCls = r.Iconic,
                        state = (ChannelMoudleBll.GetMoudleByParent(r.MoudleId).Count > 0) ? "closed" : "open"
                    };

                return Json(jsonData);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MemberNoExists(Int64 memberNo)
        {
            JsonMessage msg = null;
            var manager = ManagerAccountBll.Login(memberNo);
            if (manager == null)
            {
                msg = new JsonMessage(false, Suggestion.LoginToProfile);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            msg = new JsonMessage(true, "");
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountLogin()
        {
            return View();
        }

        //[HttpPost]
        //public JsonResult AccountLogin(string user, string password)
        //{
        //    JsonMessage msg = null;
        //    //if (Session["Code"] == null)
        //    //{
        //    //    msg = new JsonMessage(false, "重新刷新验证码");
        //    //    return Json(msg, JsonRequestBehavior.AllowGet);
        //    //}
        //    //if (Session["Code"].ToString().ToLower() != code.ToLower())
        //    //{
        //    //    msg = new JsonMessage(false, "验证码错误");
        //    //    return Json(msg, JsonRequestBehavior.AllowGet);
        //    //}

        //    //var manager = ManagerAccountBll.Login(user, ValueConvert.Md5(password));
        //    var manager = ManagerAccountBll.Login(user, password);
        //    if (manager == null)
        //    {
        //        msg = new JsonMessage(false, Suggestion.LoginError);
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }
        //    else if (!Convert.ToBoolean(manager.IsActive))
        //    {
        //        msg = new JsonMessage(false, Suggestion.AccountDisabled);
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var model = new ManagerAccount
        //        {
        //            ManagerId = manager.ManagerId,
        //            ManagerName = manager.ManagerName,
        //            ManagerEmail = manager.ManagerEmail,
        //            MobilePhone = manager.MobilePhone
        //        };

        //        // 创建身份验证票证，即转换为"已登录状态"
        //        FormsAuthentication.SetAuthCookie(manager.ManagerName, false);
        //        Session["Account"] = model;

        //        msg = new JsonMessage(true, "");
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public JsonResult AccountLogin(Int64 memberNo)
        {
            JsonMessage msg = null;
            var manager = ManagerAccountBll.Login(memberNo);
            if (manager == null)
            {
                msg = new JsonMessage(false, Suggestion.LoginToProfile);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (manager.IsActive == (int) Status.Pending)
                {
                    msg = new JsonMessage(false, Suggestion.AccountPending);
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else if (manager.IsActive == (int) Status.Disable)
                {
                    msg = new JsonMessage(false, Suggestion.AccountDisabled);
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = new ManagerAccount
                {
                    ManagerId = manager.ManagerId,
                    ManagerName = manager.ManagerName,
                    ManagerEmail = manager.ManagerEmail,
                    MobilePhone = manager.MobilePhone
                };

                // 创建身份验证票证，即转换为"已登录状态"
                FormsAuthentication.SetAuthCookie(manager.ManagerName, false);
                Session["Account"] = model;

                msg = new JsonMessage(true, "");
                return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult AccountPerfect()
        {
            var model = new ManagerAccount();
            if (Request.Cookies["memberNo"] != null)
            {
                //var statusSelectItems = new List<SelectListItem>
                //{
                //    new SelectListItem {Text = "请选择", Value = "-1", Selected = true}
                //};
                //statusSelectItems.AddRange(from object value in Enum.GetValues(typeof (Status))
                //    select new SelectListItem
                //    {
                //        Text = EnumHelper.GetDescription((Status) (value)),
                //        Value = value.ToString(),
                //        Selected = false
                //    });
                var shareChannelSelectItems = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = EnumHelper.GetDescription(ChannelShare.ZhuGuan),
                        Value = (ChannelShare.ZhuGuan).ToString(),
                        Selected = true
                    }
                };

                ViewData["ShareChannel"] = shareChannelSelectItems;

                var statusSelectItems = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = EnumHelper.GetDescription(Status.Pending),
                        Value = (Status.Pending).ToString(),
                        Selected = true
                    }
                };

                ViewData["Status"] = statusSelectItems;

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

                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);

                var loginNameCookie = Request.Cookies["loginName"];
                if (loginNameCookie != null)
                {
                    var loginName = HttpUtility.UrlDecode(loginNameCookie.Value);
                    model.ManagerEmail = loginName;
                }

                model.ManagerId = memberNo;
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public JsonResult AccountPerfect(ManagerAccount model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (ManagerAccountBll.AddManager(model, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.AddChannelUserSuccess), JsonRequestBehavior.AllowGet);
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.AddChannelUserFailed + err), JsonRequestBehavior.AllowGet);
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.AddChannelUserFailed), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AccountLoginOut()
        {
            // 取消会话
            Session.Abandon();
            // 删除Forms验证票证
            FormsAuthentication.SignOut();

            var msg = new JsonMessage(true, "");
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountInfo()
        {
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)System.Web.HttpContext.Current.Session["Account"];
            //    var managerId = account.ManagerId;

            //    ManagerAccount entity = ManagerAccountBll.GetManagerById(managerId);

            //    return View(entity);
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var entity = ManagerAccountBll.GetManagerById(memberNo);

                return View(entity);
            }

            return View();
        }

        public ActionResult AccountEdit(Int64 managerId)
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

            //var channelList = ChannelManageRepository.GetChannelList();
            //var channelNameIdList = channelList.ToList().Select(c => new
            //{
            //    channel = c.ChannelName + "/" + c.ChannelId
            //}).ToArray();

            //ViewData["ChannelList"] = channelNameIdList;

            entity.ChannelTypeStr = Enum.Parse(typeof(ChannelType), (entity.ChannelType).ToString(CultureInfo.InvariantCulture)).ToString();
            entity.IsActiveStr = Enum.Parse(typeof(Status), (entity.IsActive).ToString(CultureInfo.InvariantCulture)).ToString();
            return View(entity);
        }

        //[HttpPost]
        //public ActionResult GetChannelList(string query)
        //{
        //    var channelList = ChannelManageRepository.GetChannelList();
        //    var channelNameIdList = channelList.ToList().Where(c=>c.ChannelName.Contains(query)).Select(c => new
        //    {
        //        ChannelNameId = c.ChannelName + "/" + c.ChannelId
        //    });

        //    return Content(string.Join("\n", channelNameIdList));
        //}

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

        [HttpPost]
        public JsonResult AccountEdit(ManagerAccount model)
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

        //public ActionResult AccountPwd(Int64 managerId)
        //{
        //    ManagerAccount entity = ManagerAccountBll.GetManagerById(managerId);

        //    return View(entity);
        //}

        //[HttpPost]
        //public JsonResult AccountPwd(ManagerAccount model)
        //{
        //    JsonMessage msg = null;
        //    ManagerAccount entity = ManagerAccountBll.GetManagerById(model.ManagerId);
        //    if (entity == null)
        //    {
        //        msg = new JsonMessage(false, "");
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }
        //    var oldPwd = entity.Password;
        //    var inputPwd = (model.OldPwd).Encrypt();
        //    if (!inputPwd.Equals(oldPwd))
        //    {
        //        msg = new JsonMessage(false, Suggestion.ModifyPwdError);
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }

        //    if (ManagerAccountBll.ResetPassword(model))
        //    {
        //        msg = new JsonMessage(true, "");
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }

        //    msg = new JsonMessage(false, "");
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetStatisticsByDay()
        {
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)Session["Account"];
            //    var json = ManagerAccountBll.GetDataStatisticsByDay(account.ManagerId);

            //    return new ToJsonResult
            //    {
            //        Data = json,
            //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //    };
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var json = ManagerAccountBll.GetDataStatisticsByDay(memberNo);

                return new ToJsonResult
                {
                    Data = json,
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
        public JsonResult GetStatisticsByWeek()
        {
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)Session["Account"];
            //    var json = ManagerAccountBll.GetDataStatisticsByWeek(account.ManagerId);

            //    return new ToJsonResult
            //    {
            //        Data = json,
            //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //    };
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var json = ManagerAccountBll.GetDataStatisticsByWeek(memberNo);

                return new ToJsonResult
                {
                    Data = json,
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
        public JsonResult GetStatisticsByMonth()
        {
            //if (Session["Account"] != null)
            //{
            //    var account = (ManagerAccount)Session["Account"];
            //    var json = ManagerAccountBll.GetDataStatisticsByMonth(account.ManagerId);

            //    return new ToJsonResult
            //    {
            //        Data = json,
            //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //    };
            //}

            if (Request.Cookies["memberNo"] != null)
            {
                var memberNoCookie = Request.Cookies["memberNo"].Value;
                var memberNo = Int64.Parse(memberNoCookie);
                var json = ManagerAccountBll.GetDataStatisticsByMonth(memberNo);

                return new ToJsonResult
                {
                    Data = json,
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
