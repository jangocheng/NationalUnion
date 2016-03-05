using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Application.Commons;
using NationalUnion.Application.ViewModels;

namespace NationalUnion.Application.Interfaces
{
    /// <summary>
    /// 渠道管理功能模块操作接口
    /// </summary>
    public interface IChannelMoudleOperateBll
    {
        /// <summary>
        /// 获取模块操作列表
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<MoudleOperateInfo> GetMoudleOperateList(string moudleId, ref GridPager pager);

        /// <summary>
        /// 根据模块操作Id，获取模块操作
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        MoudleOperateInfo GetMoudleOperateById(string moudleOperateId);

        /// <summary>
        /// 判断模块操作是否存在
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        bool MoudleOperateExistsById(string moudleOperateId);

        /// <summary>
        /// 添加模块操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool AddMoudleOperate(MoudleOperateInfo model, ref ValidationErrors errors);

        /// <summary>
        /// 修改模块操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool EditMoudleOperate(MoudleOperateInfo model, ref ValidationErrors errors);

        /// <summary>
        /// 删除模块操作
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool DeleteMoudleOperate(string moudleOperateId, ref ValidationErrors errors);
    }
}
