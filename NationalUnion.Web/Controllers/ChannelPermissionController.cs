using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Web.Controllers
{
    public class ChannelPermissionController : BaseController
    {
        [Dependency]
        public IChannelPermissionBll ChannelPermissionBll { get; set; }
        [Dependency]
        public IChannelRoleBll ChannelRoleBll { get; set; }
        [Dependency]
        public IChannelMoudleBll ChannelMoudleBll { get; set; }

        public ActionResult GetList()
        {
            //ViewBag.Permis = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult GetRoleList(GridPager pager)
        {
            List<RoleInfo> roles = ChannelRoleBll.GetRoleList(ref pager, "");

            var json = new
            {
                total = pager.TotalRows,
                rows = (from r in roles
                    select new RoleInfo
                    {
                        RoleId = r.RoleId,
                        Name = r.Name,
                        Description = r.Description,
                        CreateTime = r.CreateTime,
                        CreatePerson = r.CreatePerson
                    }).ToArray()
            };

            return Json(json);
        }

        [HttpPost]
        public JsonResult GetMoudleList(string moudleId)
        {
            if (moudleId == null)
                moudleId = "0";
            List<MoudleInfo> moudles = ChannelMoudleBll.GetMoudleList(moudleId);

            var json = from r in moudles
                       select new MoudleInfo
                       {
                           MoudleId = r.MoudleId,
                           Name = r.Name,
                           EnglishName = r.EnglishName,
                           ParentId = r.ParentId,
                           Url = r.Url,
                           Iconic = r.Iconic,
                           Sort = r.Sort,
                           Remark = r.Remark,
                           MoudleState = r.MoudleState,
                           IsLast = r.IsLast,
                           CreatePerson = r.CreatePerson,
                           CreateTime = r.CreateTime,
                           state = (ChannelMoudleBll.GetMoudleByParent(r.MoudleId).Count > 0) ? "closed" : "open"
                       };

            //var json = new
            //{
            //    rows = (from r in moudles
            //        select new MoudleInfo
            //        {
            //            MoudleId = r.MoudleId,
            //            Name = r.Name,
            //            EnglishName = r.EnglishName,
            //            ParentId = r.ParentId,
            //            Url = r.Url,
            //            Iconic = r.Iconic,
            //            Sort = r.Sort,
            //            Remark = r.Remark,
            //            MoudleState = r.MoudleState,
            //            IsLast = r.IsLast,
            //            CreatePerson = r.CreatePerson,
            //            CreateTime = r.CreateTime,
            //            state = (ChannelMoudleBll.GetMoudleByParent(r.MoudleId).Count > 0) ? "closed" : "open"
            //        }).ToArray()
            //};

            return Json(json);
        }

        [HttpPost]
        public JsonResult GetPermissionByRoleAndMoudle(GridPager pager, string roleId, string moudleId)
        {
            pager.Rows = 1000;
            var permis = ChannelPermissionBll.GetPermissionByRoleAndMoudle(roleId, moudleId);

            var json = new
            {
                total = pager.TotalRows,
                rows = (from r in permis
                    select new PermissionInfo()
                    {
                        RightOperateId = r.RightId + r.KeyCode,
                        Name = r.Name,
                        KeyCode = r.KeyCode,
                        RightId = r.RightId,
                        IsValid = r.IsValid
                    }).ToArray()
            };

            return Json(json);
        }

        [HttpPost]
        public int UpdatePermission(ChannelRightOperate model)
        {
            return ChannelPermissionBll.UpdatePermission(model);
        }
    }
}
