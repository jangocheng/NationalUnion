using NationalUnion.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.Mapping
{
    public class DataSummary : IAggregateRoot
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 用户ID(分享者的MemberNO,如果有多级，则为第三级)
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 分享的平台ID
        /// </summary>
        public int PlatformID { get; set; }

        /// <summary>
        /// 分享途径，WAP/PC
        /// </summary>
        public int SharedClientID { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime SummaryDate { get; set; }

        /// <summary>
        /// 分享者的渠道ID
        /// </summary>
        public long ChannelID { get; set; }

        /// <summary>
        /// 分享者所属的渠道负责人ID
        /// </summary>
        public long ManagerID { get; set; }

        /// <summary>
        /// PV的数量
        /// </summary>
        public int PV { get; set; }

        /// <summary>
        /// UV的数量
        /// </summary>
        public int UV { get; set; }

        /// <summary>
        /// 订单成交量
        /// </summary>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductsQuantity { get; set; }

        /// <summary>
        /// 订单成交金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        public decimal EstimateCommission { get; set; }

        /// <summary>
        /// 可结算佣金
        /// </summary>
        public decimal AvaliableCommission { get; set; }

        /// <summary>
        /// 妥投的订单金额
        /// </summary>
        public decimal AvaliableOrderAmount { get; set; }

        /// <summary>
        /// 妥投的订单数量
        /// </summary>
        public int AvaliableOrderQuantity { get; set; }

        /// <summary>
        /// 分享量
        /// </summary>
        public int SharedQunatity { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
