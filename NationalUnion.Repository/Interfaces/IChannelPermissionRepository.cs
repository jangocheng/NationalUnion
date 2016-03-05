using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IChannelPermissionRepository
    {
        #region Methods

        /// <summary>
        /// 根据用户Id，获取当前用户的权限
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        List<ChannelRightOperate> GetCurrentPermission(Int64 managerId, string controller);

        /// <summary>
        /// 根据用户Id，获取权限
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        List<GetPermissionCode> GetPermission(Int64 managerId, string controller);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdatePermission(ChannelRightOperate entity);

        /// <summary>
        /// 根据角色Id和模块Id，获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        List<GetPermissionByRoleAndMoudle> GetPermissionByRoleAndMoudle(string roleId, string moudleId);

        #endregion
    }
}
