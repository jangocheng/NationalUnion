using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;

namespace NationalUnion.Domain.Mapping
{
    /// <summary>
    /// 推广收益统计
    /// </summary>
    public class PromotionRevenue : IAggregateRoot
    {
        [Key]
        public int PromoRevenueId { get; set; }

        /// <summary>
        /// 负责人编号
        /// </summary>
        public Int64 ManagerId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public string ProductQuantity { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 订单收益
        /// </summary>
        public decimal OrderRevenue { get; set; }

        /// <summary>
        /// 产生时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public virtual Manager Manager { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
