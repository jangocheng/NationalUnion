using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Application.Commons;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Application.Interfaces
{
    public interface IShareStatisticsBll
    {
        /// <summary>
        /// 获取分享宝数据统计列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ShareStatisticsInfo> GetShareStatisticsesList(ref GridPager pager);

        /// <summary>
        /// 根据查询条件，获取分享宝数据统计列表
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ShareStatisticsInfo> GetShareStatisticsesListByQuery(string queryStr, ref GridPager pager);

        /// <summary>
        /// 根据用户Id，获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserById(Int64 userId);

        /// <summary>
        /// 根据用户名，获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User GetUserByName(string userName);
    }
}
