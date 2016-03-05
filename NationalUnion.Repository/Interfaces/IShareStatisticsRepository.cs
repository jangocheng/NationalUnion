using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IShareStatisticsRepository
    {
        /// <summary>
        /// 分享宝数据统计
        /// </summary>
        /// <returns></returns>
        IQueryable<GetShareStatistics> GetShareStatisticsesList();

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
