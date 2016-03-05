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
    /// 渠道
    /// </summary>
    public class Channel : IAggregateRoot
    {
        [Key]
        public Int64 ChannelId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        [StringLength(100, MinimumLength = 2)]
        public string ChannelName { get; set; }

        /// <summary>
        /// 渠道等级
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public int ChannelType { get; set; }

        /// <summary>
        /// 归属渠道ID
        /// </summary>
        public Int64 ParentId { get; set; }

        /// <summary>
        /// 渠道关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 渠道状态
        /// </summary>
        public int IsActive { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
