using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Application.Interfaces
{
    /// <summary>
    /// 渠道管理权限控制接口
    /// </summary>
    public interface IChannelPermissionBll
    {
        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        List<GetPermissionCode> GetPermission(Int64 managerId, string controller);

        /// <summary>
        /// 根据角色和模块，获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        List<GetPermissionByRoleAndMoudle> GetPermissionByRoleAndMoudle(string roleId, string moudleId);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdatePermission(ChannelRightOperate entity);
    }
}
