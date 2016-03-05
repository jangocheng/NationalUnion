using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Application.Commons;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Application.Interfaces
{
    /// <summary>
    /// 渠道管理功能模块接口
    /// </summary>
    public interface IChannelMoudleBll
    {
        /// <summary>
        /// 获取功能模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<MoudleInfo> GetMoudleList(string parentId);

        /// <summary>
        /// 根据上级Id，获取功能模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<ChannelMoudle> GetMoudleByParent(string parentId);

        /// <summary>
        /// 根据功能模块Id，获取功能模块
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        MoudleInfo GetMoudleById(string moudleId);

        /// <summary>
        /// 判断功能模块是否存在
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        bool MoudleExistsById(string moudleId);

        /// <summary>
        /// 添加功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool AddMoudle(MoudleInfo model, ref ValidationErrors errors);

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool EditMoudle(MoudleInfo model, ref ValidationErrors errors);

        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool DeleteMoudle(string moudleId, ref ValidationErrors errors);
    }
}
