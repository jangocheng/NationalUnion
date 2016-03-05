using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.NotMapping
{
    public class GetShareStatistics
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime SummaryDate { get; set; }

        /// <summary>
        /// 渠道Id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 分享平台
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 分享途径 PC/WAP
        /// </summary>
        public int SharedClientId { get; set; }

        /// <summary>
        /// 分享量
        /// </summary>
        public int SharedQunatity { get; set; }

        /// <summary>
        /// PV
        /// </summary>
        public int PV { get; set; }

        /// <summary>
        /// UV
        /// </summary>
        public int UV { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductQuantity { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        public decimal EstimateCommission { get; set; }
    }
}
