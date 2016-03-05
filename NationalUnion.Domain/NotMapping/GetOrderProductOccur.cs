using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.NotMapping
{
    public class GetOrderProductOccur
    {
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
        /// 商品类别
        /// </summary>
        [StringLength(50)]
        public string CategoryId { get; set; }

        /// <summary>
        /// 单品价格
        /// </summary>
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
        public decimal ProductPriceAmount { get; set; }

        /// <summary>
        /// 下单类型[团购TG/抢购QG/普通PT]
        /// </summary>
        public string ProductSite { get; set; }

        /// <summary>
        /// 分享用户编号
        /// </summary>
        public long SharedUserId { get; set; }

        /// <summary>
        /// 分享等级
        /// </summary>
        public int SharedLevel { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 佣金比例
        /// </summary>
        public decimal CommissionRatio { get; set; }

        /// <summary>
        /// 分享平台
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 分享途径[PC/WAP]
        /// </summary>
        public int SharedClientId { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderOccurTime { get; set; }
    }
}
