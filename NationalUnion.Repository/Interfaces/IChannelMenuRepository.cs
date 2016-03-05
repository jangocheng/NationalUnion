using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IChannelMenuRepository
    {
        #region Methods

        /// <summary>
        /// 根据菜单上级Id，获取菜单
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        List<ChannelMoudle> GetMenuByParentId(string moudleId);

        /// <summary>
        /// 根据配置文件菜单项，获取临时菜单
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        List<ChannelMoudle> GetTempMenuByParentId(string moudleId);

        /// <summary>
        /// 根据用户Id所对应权限，获取菜单
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        List<ChannelMoudle> GetMenuByPersonId(Int64 personId, string moudleId);

        /// <summary>
        /// 根据用户Name所对应权限，获取菜单
        /// </summary>
        /// <param name="personName"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        List<ChannelMoudle> GetMenuByPersonName(string personName, string moudleId);

        #endregion
    }
}
