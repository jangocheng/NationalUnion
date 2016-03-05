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
    /// 辅助业绩统计
    /// </summary>
    public class AssistPerformance : IAggregateRoot
    {
        [Key]
        public int AssistPerformanceId { get; set; }

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
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 注册量
        /// </summary>
        public int RegisterCount { get; set; }

        /// <summary>
        /// 新客量
        /// </summary>
        public int NewMemberCount { get; set; }

        /// <summary>
        /// 发展会员数
        /// </summary>
        public int DevelopMemberCount { get; set; }

        /// <summary>
        /// 业绩月份
        /// </summary>
        public DateTime PerformanceMonth { get; set; }

        public virtual Manager Manager { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
