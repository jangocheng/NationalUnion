using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IChannelManageRepository : IRepository<Channel>
    {
        #region Methods

        /// <summary>
        /// 判断指定渠道是否存在
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        bool ChannelIdExists(Int64 channelId);

        /// <summary>
        /// 根据指定渠道Id，获取渠道实体
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Channel GetChannelById(Int64 channelId);

        /// <summary>
        /// 根据渠道Name，获取渠道实体列表
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        List<Channel> GetChannelListByName(string channelName);

        /// <summary>
        /// 根据渠道Name，获取渠道实体
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        Channel GetChannelByName(string channelName);

        /// <summary>
        /// 获取渠道实体列表
        /// </summary>
        /// <returns></returns>
        IQueryable<Channel> GetChannelList();

        /// <summary>
        /// 添加渠道实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddChannel(Channel entity);

        /// <summary>
        /// 修改渠道实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int EditChannel(Channel entity);

        /// <summary>
        /// 根据渠道Id，删除渠道实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteChannel(Channel entity);

        #endregion
    }
}
