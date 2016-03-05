using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ManagerStatisticsRepository : IManagerStatisticsRepository
    {
        public List<GetStatistics> GetDataStatisticsByDay(Int64 managerId)
        {
            using (var db = new NationalUnionContext())
            {
                var today = DateTime.Parse(DateTime.Now.ToShortDateString());
                var lday = today.AddDays(-1);
                List<GetStatistics> list =
                    db.DataSummary.Where(ds => ds.UserID == managerId && ds.SummaryDate == lday)
                        .GroupBy(ds => new { ds.UserID })
                        .Select(g => new GetStatistics
                        {
                            PV = g.Sum(ds => ds.PV),
                            UV = g.Sum(ds => ds.UV),
                            OrderQuantity = g.Sum(ds => ds.OrderQuantity),
                            OrderAmount = g.Sum(ds => ds.OrderAmount),
                            EstimateCommission = g.Sum(ds => ds.EstimateCommission),
                            AvaliableCommission = g.Sum(ds => ds.AvaliableCommission),
                            UserId = g.Key.UserID
                        }).ToList();

                return list;
            }
        }

        public List<GetStatistics> GetDataStatisticsByWeek(Int64 managerId)
        {
            using (var db = new NationalUnionContext())
            {
                var today = DateTime.Parse(DateTime.Now.ToShortDateString());
                var lday = today.AddDays(-1);
                var wday = today.AddDays(-8);
                List<GetStatistics> list =
                    db.DataSummary.Where(ds => ds.UserID == managerId && ds.SummaryDate >= wday && ds.SummaryDate <= lday)
                        .GroupBy(ds => new { ds.UserID })
                        .Select(g => new GetStatistics
                        {
                            PV = g.Sum(ds => ds.PV),
                            UV = g.Sum(ds => ds.UV),
                            OrderQuantity = g.Sum(ds => ds.OrderQuantity),
                            OrderAmount = g.Sum(ds => ds.OrderAmount),
                            EstimateCommission = g.Sum(ds => ds.EstimateCommission),
                            AvaliableCommission = g.Sum(ds => ds.AvaliableCommission),
                            UserId = g.Key.UserID
                        }).ToList();

                return list;
            }
        }

        public List<GetStatistics> GetDataStatisticsByMonth(Int64 managerId)
        {
            using (var db = new NationalUnionContext())
            {
                var today = DateTime.Parse(DateTime.Now.ToShortDateString());
                var lday = today.AddDays(-1);
                var mday = today.AddDays(-31);
                List<GetStatistics> list =
                    db.DataSummary.Where(ds => ds.UserID == managerId && ds.SummaryDate >= mday && ds.SummaryDate <= lday)
                        .GroupBy(ds => new { ds.UserID })
                        .Select(g => new GetStatistics
                        {
                            PV = g.Sum(ds => ds.PV),
                            UV = g.Sum(ds => ds.UV),
                            OrderQuantity = g.Sum(ds => ds.OrderQuantity),
                            OrderAmount = g.Sum(ds => ds.OrderAmount),
                            EstimateCommission = g.Sum(ds => ds.EstimateCommission),
                            AvaliableCommission = g.Sum(ds => ds.AvaliableCommission),
                            UserId = g.Key.UserID
                        }).ToList();

                return list;
            }
        }
    }
}
