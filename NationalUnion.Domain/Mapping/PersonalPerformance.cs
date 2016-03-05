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
    /// 个人业绩统计
    /// </summary>
    public class PersonalPerformance : IAggregateRoot
    {
        [Key]
        public int PersonalPerformanceId { get; set; }

        /// <summary>
        /// 负责人编号
        /// </summary>
        public Int64 ManagerId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 订单分类编号
        /// </summary>
        public int OrderCategoryId { get; set; }

        /// <summary>
        /// 订单分类
        /// </summary>
        public string OrderCategory { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public string ProductQuantity { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        public decimal ExpectedKickback { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string OrderSource { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public virtual Manager Manager { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
