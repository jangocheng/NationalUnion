using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;

namespace NationalUnion.Domain.Mapping
{
    /// <summary>
    /// 团队业绩统计
    /// </summary>
    public class TeamPerformance : IAggregateRoot
    {
        public int TeamPerformanceId { get; set; }

        /// <summary>
        /// 负责人编号
        /// </summary>
        public Int64 ManagerId { get; set; }

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
        /// 业绩月份
        /// </summary>
        public DateTime PerformanceMonth { get; set; }

        public virtual Manager Manager { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
