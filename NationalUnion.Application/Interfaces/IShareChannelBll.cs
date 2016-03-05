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
    /// 分享宝渠道管理渠道维护模块接口
    /// </summary>
    public interface IShareChannelBll
    {
        /// <summary>
        /// 判断渠道ID是否存在
        /// </summary>
        /// <param name="shareChannelId"></param>
        /// <returns></returns>
        bool ShareChannelIdExists(Int64 shareChannelId);

        /// <summary>
        /// 根据渠道ID获取渠道
        /// </summary>
        /// <param name="shareChannelId"></param>
        /// <returns></returns>
        ShareChannelInfo GetShareChannelById(Int64 shareChannelId);

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ShareChannelInfo> GetShareChannelList(ref GridPager pager);

        /// <summary>
        /// 根据查询条件获取渠道列表
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ShareChannelInfo> GetShareChannelListByQuery(string queryStr, ref GridPager pager);

        /// <summary>
        /// 添加渠道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddShareChannel(ShareChannelInfo model);

        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditShareChannel(ShareChannelInfo model);

        /// <summary>
        /// 删除渠道
        /// </summary>
        /// <param name="shareChannelId"></param>
        /// <returns></returns>
        bool DeleteShareChannel(Int64 shareChannelId);

        /// <summary>
        /// 激活或停用
        /// </summary>
        /// <param name="shareChannelId"></param>
        /// <returns></returns>
        bool ShareChangeStatus(Int64 shareChannelId);
    }
}
