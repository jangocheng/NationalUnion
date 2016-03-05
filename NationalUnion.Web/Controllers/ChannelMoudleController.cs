using System;
using System.Collections.Generic;
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
    public class ChannelMoudleController : BaseController
    {
        [Dependency]
        public IChannelMoudleBll ChannelMoudleBll { get; set; }
        [Dependency]
        public IChannelMoudleOperateBll ChannelMoudleOperateBll { get; set; }
        
        // 异常集合
        ValidationErrors _errors = new ValidationErrors();

        public ActionResult MoudleList()
        {
            //ViewBag.Permis = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult GetMoudleList(string parentId)
        {
            if (parentId == null)
                parentId = "0";
            var list = ChannelMoudleBll.GetMoudleList(parentId);

            var json = list.Select(row =>
                new MoudleInfo
                {
                    MoudleId = row.MoudleId,
                    Name = row.Name,
                    EnglishName = row.EnglishName,
                    ParentId = row.ParentId,
                    Url = row.Url,
                    Iconic = row.Iconic,
                    Sort = row.Sort,
                    Remark = row.Remark,
                    MoudleState = row.MoudleState,
                    CreatePerson = row.CreatePerson,
                    CreateTime = row.CreateTime,
                    IsLast = row.IsLast,
                    state = (ChannelMoudleBll.GetMoudleByParent(row.MoudleId).Count > 0) ? "closed" : "open"
                });

            return Json(json);
        }

        [HttpPost]
        public JsonResult GetMoudleOperateList(string moudleId, GridPager pager)
        {
            pager.Rows = 1000;
            pager.Page = 1;
            var list = ChannelMoudleOperateBll.GetMoudleOperateList(moudleId, ref pager);

            var json = list.Select(row =>
                new MoudleOperateInfo
                {
                    MoudleOperateId = row.MoudleOperateId,
                    Name = row.Name,
                    MoudleId = row.MoudleId,
                    KeyCode = row.KeyCode,
                    IsValid = row.IsValid,
                    Sort = row.Sort
                }).ToArray();

            return Json(json);
        }

        public ActionResult AddMoudle(string moudleId)
        {
            //ViewBag.Permis = GetPermission();
            var entity = new MoudleInfo
            {
                ParentId = moudleId,
                MoudleState = 1,
                Sort = 0
            };

            return View(entity);
        }

        [HttpPost]
        public JsonResult AddMoudle(MoudleInfo model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (ChannelMoudleBll.AddMoudle(model, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + err));
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
        }

        public ActionResult AddMoudleOperate(string moudleId)
        {
            //ViewBag.Permis = GetPermission();
            var entity = new MoudleOperateInfo
            {
                MoudleId = moudleId,
                IsValid = 1
            };

            return View(entity);
        }

        [HttpPost]
        public JsonResult AddMoudleOperate(MoudleOperateInfo model)
        {
            if (model != null && ModelState.IsValid)
            {
                var entity = ChannelMoudleOperateBll.GetMoudleOperateById(model.MoudleOperateId);
                if (entity != null)
                {
                    return Json(JsonHandler.CreateMessage(0, Suggestion.PrimaryRepeat), JsonRequestBehavior.AllowGet);
                }

                entity = new MoudleOperateInfo
                {
                    MoudleOperateId = model.MoudleId + model.KeyCode,
                    Name = model.Name,
                    MoudleId = model.MoudleId,
                    KeyCode = model.KeyCode,
                    IsValid = model.IsValid,
                    Sort = model.Sort
                };

                if (ChannelMoudleOperateBll.AddMoudleOperate(model, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + err), JsonRequestBehavior.AllowGet);
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMoudle(string moudleId)
        {
            //ViewBag.Permis = GetPermission();
            var entity = ChannelMoudleBll.GetMoudleById(moudleId);

            return View(entity);
        }

        [HttpPost]
        public JsonResult EditMoudle(MoudleInfo model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (ChannelMoudleBll.EditMoudle(model, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + err));
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
        }

        [HttpPost]
        public JsonResult DeleteMoudle(string moudleId)
        {
            if (!string.IsNullOrWhiteSpace(moudleId))
            {
                if (ChannelMoudleBll.DeleteMoudle(moudleId, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + err), JsonRequestBehavior.AllowGet);
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteMoudleOperate(string moudleOperateId)
        {
            if (!string.IsNullOrWhiteSpace(moudleOperateId))
            {
                if (ChannelMoudleOperateBll.DeleteMoudleOperate(moudleOperateId, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + err), JsonRequestBehavior.AllowGet);
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
        }
    }
}
