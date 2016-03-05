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
    /// 渠道负责人历史明细表
    /// </summary>
    public class ManagerDetail : IAggregateRoot
    {
        [Key]
        public int ManagerDetailId { get; set; }

        /// <summary>
        /// 渠道负责人ID
        /// </summary>
        public Int64 ManagerId { get; set; }

        /// <summary>
        /// 当前渠道ID
        /// </summary>
        public Int64 CurrentChannelId { get; set; }

        /// <summary>
        /// 历史渠道ID
        /// </summary>
        public Int64 OldChannelId { get; set; }

        /// <summary>
        /// 当前渠道等级
        /// </summary>
        public int CurrentRank { get; set; }

        /// <summary>
        /// 历史渠道等级
        /// </summary>
        public int OldRank { get; set; }

        /// <summary>
        /// 渠道等级状态[1离2降3平4升]
        /// </summary>
        public int RankStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        public virtual Manager Manager { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
