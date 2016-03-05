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
    public class ChannelRoleController : BaseController
    {
        [Dependency]
        public IChannelRoleBll ChannelRoleBll { get; set; }

        ValidationErrors _errors = new ValidationErrors();

        public ActionResult RoleList()
        {
            //ViewBag.Permis = GetPermission();
            return View();
        }

        public JsonResult GetRoleList(GridPager pager, string queryStr)
        {
            List<RoleInfo> list = ChannelRoleBll.GetRoleList(ref pager, queryStr);

            var json = new
            {
                total = pager.TotalRows,
                rows = (from r in list
                    select new RoleInfo
                    {
                        RoleId = r.RoleId,
                        Name = r.Name,
                        Description = r.Description,
                        CreateTime = r.CreateTime,
                        CreatePerson = r.CreatePerson,
                        ManagerName = r.ManagerName
                    }).ToArray()
            };

            return Json(json);
        }

        public ActionResult AddRole()
        {
            //ViewBag.Permis = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult AddRole(RoleInfo model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (ChannelRoleBll.AddRole(model, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + err));
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
        }

        public ActionResult EditRole(string roleId)
        {
            //ViewBag.Permis = GetPermission();
            RoleInfo entity = ChannelRoleBll.GetRoleById(roleId);

            return View(entity);
        }

        [HttpPost]
        public JsonResult EditRole(RoleInfo model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (ChannelRoleBll.EditRole(model, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + err));
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
        }

        public ActionResult DetailRole(string roleId)
        {
            //ViewBag.Permis = GetPermission();
            RoleInfo entity = ChannelRoleBll.GetRoleById(roleId);

            return View(entity);
        }

        [HttpPost]
        public JsonResult DeleteRole(string roleId)
        {
            if (!string.IsNullOrWhiteSpace(roleId))
            {
                if (ChannelRoleBll.DeleteRole(roleId, ref _errors))
                {
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }

                string err = _errors.Error;
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + err));
            }

            return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
        }
    }
}
