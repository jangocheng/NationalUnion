using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Common.Utility;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Application.Implements
{
    public class ShareStatisticsBll : IShareStatisticsBll
    {
        [Dependency]
        public IShareStatisticsRepository ShareStatisticsRepository { get; set; }

        public List<ShareStatisticsInfo> GetShareStatisticsesList(ref GridPager pager)
        {
            var queryData = ShareStatisticsRepository.GetShareStatisticsesList();

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public List<ShareStatisticsInfo> GetShareStatisticsesListByQuery(string queryStr, ref GridPager pager)
        {
            var queryData = ShareStatisticsRepository.GetShareStatisticsesList();

            // *用户名* / 分享平台 / 分享途径
            string[] queryArr = queryStr.Split(new char[] { '/' });
            string startTime = queryArr[0];
            DateTime start = DateTime.Parse(startTime);
            string endTime = queryArr[1];
            DateTime end = DateTime.Parse(endTime);
            string queryByUser = queryArr[2];
            string queryByPlatform = queryArr[3];
            int platform = Convert.ToInt32(Enum.Parse(typeof (EnumUtility.SharedPlatform), queryByPlatform));
            string queryByClientType = queryArr[4];
            int clientType = Convert.ToInt32(Enum.Parse(typeof (EnumUtility.SharedClientType), queryByClientType));

            if (!string.IsNullOrEmpty(startTime))
            {
                queryData = queryData.Where(d => d.SummaryDate >= start);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                queryData = queryData.Where(d => d.SummaryDate <= end);
            }
            if (!string.IsNullOrEmpty(queryByUser) && !queryByUser.Equals("输入*用户名*关键字"))
            {
                var user = GetUserByName(queryByUser);
                queryData = queryData.Where(d => d.UserId == user.UserId);
            }
            if (!string.IsNullOrEmpty(queryByPlatform) && !queryByPlatform.Equals("-1"))
            {
                queryData = queryData.Where(d => d.PlatformId == platform);
            }
            if (!string.IsNullOrEmpty(queryByClientType) && !queryByClientType.Equals("-1"))
            {
                queryData = queryData.Where(d => d.SharedClientId == clientType);
            }

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public User GetUserById(long userId)
        {
            return ShareStatisticsRepository.GetUserById(userId);
        }

        public User GetUserByName(string userName)
        {
            return ShareStatisticsRepository.GetUserByName(userName);
        }

        private List<ShareStatisticsInfo> CreateModelList(ref GridPager pager, ref IQueryable<GetShareStatistics> queryData)
        {
            pager.TotalRows = queryData.Count();
            if (pager.TotalRows > 0)
            {
                queryData = pager.Page <= 1
                    ? queryData.Take(pager.Rows)
                    : queryData.Skip((pager.Page - 1)*pager.Rows).Take(pager.Rows);
            }

            List<ShareStatisticsInfo> modelList = queryData.Select(d =>
                new ShareStatisticsInfo
                {
                    UserId = d.UserId,
                    SummaryDate = d.SummaryDate,
                    ChannelId = d.ChannelId,
                    PlatformId = d.PlatformId,
                    SharedClientId = d.SharedClientId,
                    SharedQunatity = d.SharedQunatity,
                    PV = d.PV,
                    UV = d.UV,
                    OrderQuantity = d.OrderQuantity,
                    ProductQuantity = d.ProductQuantity,
                    OrderAmount = d.OrderAmount,
                    EstimateCommission = d.EstimateCommission
                }).ToList();

            return modelList;
        }
    }
}
