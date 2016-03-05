using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IShareDetailRepository
    {
        /// <summary>
        /// 获取分享宝分享明细列表
        /// </summary>
        /// <returns></returns>
        IQueryable<GetOrderProductOccur> GetShareDetailList();

        /// <summary>
        /// 根据商品类别Id，获取商品类别信息
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        ProductType GetProductType(string typeId);
    }
}
