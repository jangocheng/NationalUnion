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
    /// 城市对照表
    /// </summary>
    public class City : IAggregateRoot
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int CId { get; set; }

        /// <summary>
        /// 所属省份编号
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public virtual Province Province { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
