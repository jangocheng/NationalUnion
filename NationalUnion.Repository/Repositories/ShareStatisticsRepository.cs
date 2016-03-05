using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;
using NationalUnion.Infrastructure;

namespace NationalUnion.Repository.Repositories
{
    public class ShareStatisticsRepository : IShareStatisticsRepository
    {
        public IQueryable<GetShareStatistics> GetShareStatisticsesList()
        {
            var db = new NationalUnionContext();
            IQueryable<GetShareStatistics> list = db.DataSummary.Select(ds =>
                new GetShareStatistics
                {
                    UserId = ds.UserID,
                    SummaryDate = ds.SummaryDate,
                    ChannelId = ds.ChannelID,
                    PlatformId = ds.PlatformID,
                    SharedClientId = ds.SharedClientID,
                    SharedQunatity = ds.SharedQunatity,
                    PV = ds.PV,
                    UV = ds.UV,
                    OrderQuantity = ds.OrderQuantity,
                    ProductQuantity = ds.ProductsQuantity,
                    OrderAmount = ds.OrderAmount,
                    EstimateCommission = ds.EstimateCommission
                });

            return list;
        }

        public User GetUserById(long userId)
        {
            using (var db = new NationalUnionContext())
            {
                return db.Users.FirstOrDefault(u => u.UserId == userId);
            }
        }

        public User GetUserByName(string userName)
        {
            using (var db = new NationalUnionContext())
            {
                return db.Users.FirstOrDefault(u => u.RealName.Contains(userName));
            }
        }
    }
}
