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
    public class OrderProductOccur : IAggregateRoot
    {
        [Key]
        public int OrderProductOccurId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 商品唯一标识
        /// </summary>
        [StringLength(50)]
        public string ProductSkuId { get; set; }

        /// <summary>
        /// 单品价格
        /// </summary>
        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNumber { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(1000)]
        public string ProductName { get; set; }

        /// <summary>
        /// 商品总额
        /// </summary>
        [Column(TypeName = "money")]
        public decimal ProductPriceAmount { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        [StringLength(50)]
        public string CategoryId { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Commission { get; set; }

        /// <summary>
        /// 佣金比例
        /// </summary>
        public decimal CommissionRatio { get; set; }

        /// <summary>
        /// 来源[PC/WAP]
        /// </summary>
        [StringLength(50)]
        public string WebSite { get; set; }

        /// <summary>
        /// 商品类型[团购/抢购/普通]
        /// </summary>
        [StringLength(50)]
        public string ProductSite { get; set; }

        /// <summary>
        /// 订单结算状态
        /// </summary>
        [StringLength(50)]
        public string OrderSettlement { get; set; }

        /// <summary>
        /// 下单IP
        /// </summary>
        [StringLength(50)]
        public string OrderIp { get; set; }

        /// <summary>
        /// 订单是否有效
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 是否推送
        /// </summary>
        public int PushStatus { get; set; }

        /// <summary>
        /// Feedback & wId
        /// </summary>
        public UrlParameters UrlParameters { get; set; }

        public long MemberNo { get; set; }

        public int OmnitureFlag { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderOccurTime { get; set; }

        /// <summary>
        /// 记录生成时间
        /// </summary>
        public DateTime RecordCreatedTime { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
