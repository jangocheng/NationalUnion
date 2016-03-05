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
    /// 总收益统计
    /// </summary>
    public class Revenue : IAggregateRoot
    {
        [Key]
        public int RevenueId { get; set; }

        /// <summary>
        /// 负责人编号
        /// </summary>
        public Int64 ManagerId { get; set; }

        /// <summary>
        /// 个人提成
        /// </summary>
        public decimal PersonalCommission { get; set; }

        /// <summary>
        /// 团队提成
        /// </summary>
        public decimal TeamCommission { get; set; }

        /// <summary>
        /// 管理提成
        /// </summary>
        public decimal ManageCommission { get; set; }

        /// <summary>
        /// 总提成
        /// </summary>
        public decimal TotalCommission { get; set; }

        /// <summary>
        /// KPI得分
        /// </summary>
        public decimal KpiScore { get; set; }

        /// <summary>
        /// 总收益
        /// </summary>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// 收益月份
        /// </summary>
        public DateTime RevenueMonth { get; set; }

        public virtual Manager Manager { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
