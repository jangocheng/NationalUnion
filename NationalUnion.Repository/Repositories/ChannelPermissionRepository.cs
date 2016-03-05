using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using NationalUnion.Common.SqlHelper;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ChannelPermissionRepository : IChannelPermissionRepository
    {
        /// <summary>
        /// 获取模块的当前用户操作权限[换用存储过程]
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public List<ChannelRightOperate> GetCurrentPermission(Int64 managerId, string action)
        {
            using (var db = new NationalUnionContext())
            {
                var operate = (from rm in db.ChannelRoleManagers
                    where rm.ManagerId == managerId
                    from right in db.ChannelRights
                    where right.RoleId == rm.RoleId
                    from menu in db.ChannelMoudles
                    where menu.MoudleId == right.MoudleId
                    where menu.Url == action
                    from ro in db.ChannelRightOperates
                    where ro.RightId == right.RightId
                    where ro.IsValid == 1
                    select ro).Distinct().ToList();

                return operate;
            }
        }

        /// <summary>
        /// 获取模块的当前用户操作权限
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public List<GetPermissionCode> GetPermission(Int64 managerId, string action)
        {
            // 换用存储过程
            //var permis = GetCurrentPermission(managerId, action);

            //定义存储过程入参
            var sqlParas = new SqlParameter[1];
            sqlParas[0] = new SqlParameter("@ManagerId", DbType.Int64) {Direction = ParameterDirection.Input};
            sqlParas[1] = new SqlParameter("@Url", DbType.String) {Direction = ParameterDirection.Input};

            IList<ChannelRightOperate> permis = ProcedureHelper.ExcuteDataSet<ChannelRightOperate>("getChannelRightOperateByManagerId", sqlParas);

            List<GetPermissionCode> rights = (from perm in permis
                select new GetPermissionCode
                {
                    KeyCode = perm.RightId,
                    IsValid = perm.IsValid
                }).ToList();

            return rights;
        }

        public int UpdatePermission(ChannelRightOperate entity)
        {
            var rightOperate = new ChannelRightOperate
            {
                RightOperateId = entity.RightOperateId,
                RightId = entity.RightId,
                KeyCode = entity.KeyCode,
                IsValid = entity.IsValid
            };

            // 判断rightOperate是否存在，如果存在就更新rightOperate，否则就添加一条
            using (var db = new NationalUnionContext())
            {
                ChannelRightOperate rightOpt = db.ChannelRightOperates.FirstOrDefault(ro => ro.RightOperateId == rightOperate.RightOperateId);
                if (rightOpt != null)
                {
                    rightOpt.IsValid = rightOperate.IsValid;
                }

                db.ChannelRightOperates.Add(rightOperate);

                if (db.SaveChanges() > 0)
                {
                    // 更新角色--模块的有效标志RightFlag
                    var right = (from rt in db.ChannelRights
                        where rt.RightId == rightOperate.RightId
                        select rt).First();

                    // 根据right.MoudleId right.RoleId，计算上级模块的RightFlag
                    var count = db.ChannelRightOperates.Count(ro => ro.RightId == (right.RoleId + right.MoudleId) && ro.IsValid == 1);
                    var rgt = db.ChannelRights.FirstOrDefault(r => r.RoleId == right.RoleId && r.MoudleId == right.MoudleId);
                    if (rgt != null)
                    {
                        if (count > 0)
                        {
                            rgt.RightFlag = 1;
                        }

                        rgt.RightFlag = 0;
                    }

                    string parentId = right.MoudleId;
                    while (parentId != null && parentId != "0")
                    {
                        var moudles = db.ChannelMoudles.Select(cm => cm.ParentId == parentId);
                        if (moudles.Any())
                        {
                            var objcount = (from cm in db.ChannelMoudles
                                where cm.ParentId == parentId
                                from cr in db.ChannelRights
                                where cr.RoleId == right.RoleId
                                where cr.MoudleId == cm.MoudleId
                                where cr.RightFlag == 1
                                select cr).Count();

                            var crgt = db.ChannelRights.FirstOrDefault(r => r.MoudleId == parentId && r.RoleId == right.RoleId);
                            if (crgt != null)
                            {
                                if (objcount > 0)
                                {
                                    crgt.RightFlag = 1;
                                }

                                crgt.RightFlag = 0;
                            }
                        }
                    }
                    return 1;
                }
                db.SaveChanges();
            }

            return 0;
        }

        public List<GetPermissionByRoleAndMoudle> GetPermissionByRoleAndMoudle(string roleId, string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                var result = (from cmo in db.ChannelMoudleOperates
                    from cro in db.ChannelRightOperates
                    from r in db.ChannelRights
                    where r.RoleId == roleId
                    where r.MoudleId == moudleId
                    where r.RightId == cro.RightId
                    where r.MoudleId == cmo.MoudleId
                    where cmo.IsValid == cro.IsValid
                    where cmo.IsValid == 1
                    select new GetPermissionByRoleAndMoudle
                    {
                        Name = cmo.Name,
                        KeyCode = cmo.KeyCode,
                        RoleId = r.RoleId,
                        MoudleId = r.MoudleId,
                        RightId = roleId + moudleId,
                        IsValid = cmo.IsValid,
                        Sort = cmo.Sort
                    }).ToList();

                return result;
            }
        }
    }
}
