using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IShareChannelRepository : IRepository<ShareChannel>
    {
        #region Methods

        /// <summary>
        /// 判断指定分享宝渠道是否存在
        /// </summary>
        /// <param name="shareChannelId"></param>
        /// <returns></returns>
        bool ShareChannelIdExists(Int64 shareChannelId);

        /// <summary>
        /// 根据指定分享宝渠道ID，获取渠道实体
        /// </summary>
        /// <param name="shareChannelId"></param>
        /// <returns></returns>
        ShareChannel GetShareChannelById(Int64 shareChannelId);

        /// <summary>
        /// 获取分享宝渠道实体列表
        /// </summary>
        /// <returns></returns>
        IQueryable<ShareChannel> GetShareChannelList();

        /// <summary>
        /// 添加分享宝渠道实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddShareChannel(ShareChannel entity);

        /// <summary>
        /// 添加分享宝渠道实体
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddShareChannelByfk(string modelName, ShareChannel entity);

        /// <summary>
        /// 修改分享宝渠道实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int EditShareChannel(ShareChannel entity);

        /// <summary>
        /// 根据分享宝渠道ID，删除渠道实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteShareChannel(ShareChannel entity);

        #endregion
    }
}
