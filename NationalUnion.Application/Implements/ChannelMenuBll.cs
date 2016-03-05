using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Interfaces;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Application.Implements
{
    public class ChannelMenuBll : IChannelMenuBll
    {
        [Dependency]
        public IChannelMenuRepository ChannelMenuRepository { get; set; }

        /// <summary>
        /// 根据菜单上级Id，获取菜单
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public List<ChannelMoudle> GetMenuByParentId(string moudleId)
        {
            return ChannelMenuRepository.GetMenuByParentId(moudleId);
        }

        /// <summary>
        /// 根据配置文件菜单项，获取临时菜单
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public List<ChannelMoudle> GetTempMenuByParentId(string moudleId)
        {
            return ChannelMenuRepository.GetTempMenuByParentId(moudleId);
        }

        /// <summary>
        /// 根据用户Id所对应权限，获取菜单
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public List<ChannelMoudle> GetMenuByPersonId(Int64 personId, string moudleId)
        {
            return ChannelMenuRepository.GetMenuByPersonId(personId, moudleId);
        }

        /// <summary>
        /// 根据用户Name所对应权限，获取菜单
        /// </summary>
        /// <param name="personName"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public List<ChannelMoudle> GetMenuByPersonName(string personName, string moudleId)
        {
            return ChannelMenuRepository.GetMenuByPersonName(personName, moudleId);
        }
    }
}
