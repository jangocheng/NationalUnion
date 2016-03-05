using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ShareDetailRepository : IShareDetailRepository
    {
        public IQueryable<GetOrderProductOccur> GetShareDetailList()
        {
            var db = new NationalUnionContext();
            IQueryable<GetOrderProductOccur> list = db.OrderProductOccurs.Select(p =>
                new GetOrderProductOccur
                {
                    OrderId = p.OrderId,
                    ProductSkuId = p.ProductSkuId,
                    CategoryId = p.CategoryId,
                    ProductPrice = p.ProductPrice,
                    ProductNumber = p.ProductNumber,
                    ProductName = p.ProductName,
                    ProductPriceAmount = p.ProductPriceAmount,
                    ProductSite = p.ProductSite,
                    SharedUserId = p.UrlParameters.SharedUserId,
                    SharedLevel = p.UrlParameters.SharedLevel,
                    Commission = p.Commission,
                    CommissionRatio = p.CommissionRatio,
                    PlatformId = p.UrlParameters.PlatformId,
                    SharedClientId = p.UrlParameters.SharedClientId,
                    OrderOccurTime = p.OrderOccurTime
                });

            return list;
        }

        public ProductType GetProductType(string typeId)
        {
            using (var db = new NationalUnionContext())
            {
                return db.ProductType.FirstOrDefault(p => p.PtoTypeId == typeId);
            }
        }
    }
}
