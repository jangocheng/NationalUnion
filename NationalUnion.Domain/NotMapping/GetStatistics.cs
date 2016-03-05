using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.NotMapping
{
    public class GetStatistics
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
        /// PV
        /// </summary>
        public int PV { get; set; }

        /// <summary>
        /// UV
        /// </summary>
        public int UV { get; set; }

        /// <summary>
        /// 订单量
        /// </summary>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        public decimal EstimateCommission { get; set; }

        /// <summary>
        /// 可用佣金
        /// </summary>
        public decimal AvaliableCommission { get; set; }
    }
}
