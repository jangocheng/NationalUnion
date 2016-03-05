using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IManagerStatisticsRepository
    {
        #region Methods

        /// <summary>
        /// 按天数据统计
        /// </summary>
        /// <returns></returns>
        List<GetStatistics> GetDataStatisticsByDay(Int64 managerId);

        /// <summary>
        /// 按周数据统计
        /// </summary>
        /// <returns></returns>
        List<GetStatistics> GetDataStatisticsByWeek(Int64 managerId);

        /// <summary>
        /// 按月数据统计
        /// </summary>
        /// <returns></returns>
        List<GetStatistics> GetDataStatisticsByMonth(Int64 managerId);

        #endregion
    }
}
