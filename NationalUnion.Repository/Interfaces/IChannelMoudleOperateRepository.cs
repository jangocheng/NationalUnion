using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IChannelMoudleOperateRepository
    {
        #region Methods

        /// <summary>
        /// 根据功能模块Id，获取功能模块操作列表
        /// </summary>
        /// <returns></returns>
        //IQueryable<ChannelMoudleOperate> GetMoudleOperateList(string moudleId);
        List<ChannelMoudleOperate> GetMoudleOperateList(string moudleId);

        /// <summary>
        /// 根据功能模块操作Id，获取功能模块操作
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        ChannelMoudleOperate GetMoudleOperateById(string moudleOperateId);

        /// <summary>
        /// 判断功能模块操作是否存在
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        bool MoudleOperateExistsById(string moudleOperateId);

        /// <summary>
        /// 添加功能模块操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddMoudleOperate(ChannelMoudleOperate entity);

        /// <summary>
        /// 修改功能模块操作
        /// </summary>
        /// <param name="enity"></param>
        /// <returns></returns>
        int EditMoudleOperate(ChannelMoudleOperate enity);

        /// <summary>
        /// 删除功能模块操作
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        int DeleteMoudleOperate(string moudleOperateId);

        #endregion
    }
}
