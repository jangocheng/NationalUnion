using NationalUnion.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace NationalUnion.Domain.Mapping
{
    /// <summary>
    /// 会员级别配置信息
    /// </summary>
    public class UserLevel : IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int LevelID { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 级别编码
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 级别名
        /// </summary>
        [StringLength(50)]
        public string LevelName { get; set; }

        /// <summary>
        /// 级别描述
        /// </summary>
        [StringLength(500)]
        public string LevelDescription { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// UV数量
        /// </summary>
        public int UVQuantity { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } 

        [NotMapped]
        public Guid Id { get; set; }
    }
}
