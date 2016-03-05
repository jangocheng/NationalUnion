using System;
using System.Collections.Generic;
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
    public class ShareDetailBll : IShareDetailBll
    {
        [Dependency]
        public IShareDetailRepository ShareDetailRepository { get; set; }
        [Dependency]
        public IShareStatisticsRepository ShareStatisticsRepository { get; set; }

        public List<OrderProductOccurInfo> GetShareDetailList(ref GridPager pager)
        {
            var queryData = ShareDetailRepository.GetShareDetailList();

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public List<OrderProductOccurInfo> GetShareDetailListByQuery(string queryStr, ref GridPager pager)
        {
            var queryData = ShareDetailRepository.GetShareDetailList();

            // *用户名* / 分享平台 / 分享途径 / 下单类型
            string[] queryArr = queryStr.Split(new char[] { '/' });
            string startTime = queryArr[0];
            DateTime start = DateTime.Parse(startTime);
            string endTime = queryArr[1];
            DateTime end = DateTime.Parse(endTime);
            string queryByUser = queryArr[2];
            string queryByPlatform = queryArr[3];
            int platform = Convert.ToInt32(Enum.Parse(typeof(EnumUtility.SharedPlatform), queryByPlatform));
            string queryByClientType = queryArr[4];
            int clientType = Convert.ToInt32(Enum.Parse(typeof(EnumUtility.SharedClientType), queryByClientType));
            string queryByOrderType = queryArr[5];

            if (!string.IsNullOrEmpty(startTime))
            {
                queryData = queryData.Where(d => d.OrderOccurTime >= start);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                queryData = queryData.Where(d => d.OrderOccurTime <= end);
            }
            if (!string.IsNullOrEmpty(queryByUser) && !queryByUser.Equals("输入*用户名*关键字"))
            {
                var user = GetUserByName(queryByUser);
                queryData = queryData.Where(d => d.SharedUserId == user.UserId);
            }
            if (!string.IsNullOrEmpty(queryByPlatform) && !queryByPlatform.Equals("-1"))
            {
                queryData = queryData.Where(d => d.PlatformId == platform);
            }
            if (!string.IsNullOrEmpty(queryByClientType) && !queryByClientType.Equals("-1"))
            {
                queryData = queryData.Where(d => d.SharedClientId == clientType);
            }
            if (!string.IsNullOrEmpty(queryByOrderType) && !queryByOrderType.Equals("-1"))
            {
                queryData = queryData.Where(d => d.ProductSite == queryByOrderType);
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

        public ProductType GetProductType(string typeId)
        {
            return ShareDetailRepository.GetProductType(typeId);
        }

        private List<OrderProductOccurInfo> CreateModelList(ref GridPager pager, ref IQueryable<GetOrderProductOccur> queryData)
        {
            pager.TotalRows = queryData.Count();
            if (pager.TotalRows > 0)
            {
                queryData = pager.Page <= 1
                    ? queryData.Take(pager.Rows)
                    : queryData.Skip((pager.Page - 1)*pager.Rows).Take(pager.Rows);
            }

            List<OrderProductOccurInfo> modelList = queryData.Select(d =>
                new OrderProductOccurInfo
                {
                    OrderId = d.OrderId,
                    ProductSkuId = d.ProductSkuId,
                    CategoryId = d.CategoryId,
                    ProductPrice = d.ProductPrice,
                    ProductNumber = d.ProductNumber,
                    ProductName = d.ProductName,
                    ProductPriceAmount = d.ProductPriceAmount,
                    ProductSite = d.ProductSite,
                    SharedUserId = d.SharedUserId,
                    SharedLevel = d.SharedLevel,
                    Commission = d.Commission,
                    CommissionRatio = d.CommissionRatio,
                    PlatformId = d.PlatformId,
                    SharedClientId = d.SharedClientId,
                    OrderOccurTime = d.OrderOccurTime
                }).ToList();

            return modelList;
        }
    }
}
