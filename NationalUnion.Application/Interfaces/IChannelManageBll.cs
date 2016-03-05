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
    /// 渠道管理渠道维护模块接口
    /// </summary>
    public interface IChannelManageBll
    {
        /// <summary>
        /// 判断渠道ID是否存在
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        bool ChannelIdExists(Int64 channelId);

        /// <summary>
        /// 根据渠道ID获取渠道
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        ChannelInfo GetChannelById(Int64 channelId);

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ChannelInfo> GetChannelList(ref GridPager pager);

        /// <summary>
        /// 根据查询条件获取渠道列表
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ChannelInfo> GetChannelListByQuery(string queryStr, ref GridPager pager);

        /// <summary>
        /// 添加渠道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddChannel(ChannelInfo model);

        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditChannel(ChannelInfo model);

        /// <summary>
        /// 删除渠道
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        bool DeleteChannel(Int64 channelId);

        /// <summary>
        /// 激活或停用
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        bool ChangeStatus(Int64 channelId);
    }
}
