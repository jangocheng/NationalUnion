using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IChannelMoudleRepository
    {
        #region Methods

        /// <summary>
        /// 获取功能模块列表
        /// </summary>
        /// <returns></returns>
        //IQueryable<ChannelMoudle> GetMoudleList();
        List<ChannelMoudle> GetMoudleList();

        /// <summary>
        /// 根据上级Id，获取功能模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        //IQueryable<ChannelMoudle> GetMoudleByParent(string parentId);
        List<ChannelMoudle> GetMoudleByParent(string parentId);

        /// <summary>
        /// 根据功能模块Id，获取功能模块
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        ChannelMoudle GetMoudleById(string moudleId);

        /// <summary>
        /// 判断功能模块是否存在
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        bool MoudleExistsById(string moudleId);

        /// <summary>
        /// 添加功能模块
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddMoudle(ChannelMoudle entity);

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int EditMoudle(ChannelMoudle entity);

        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        int DeleteMoudle(string moudleId);

        #endregion
    }
}
