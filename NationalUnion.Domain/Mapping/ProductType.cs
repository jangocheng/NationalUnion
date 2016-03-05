using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalUnion.Domain.Mapping
{
    public class ProductType : IAggregateRoot
    {
        [Key]
        [StringLength(20)]
        public string PtoTypeId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(200)]
        public string ProTypeName { get; set; }
        /// <summary>
        /// 父类ID
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 顶级父类ID
        /// </summary>
        public string FinalPid { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        [StringLength(20)]
        public int? SortId { get; set; }

        /// <summary>
        /// 分类地址
        /// </summary>
        [StringLength(2000)]
        public string CateUrl { get; set; }

        /// <summary>
        /// 是否启用[0为禁用1为启用]
        /// </summary>
        public int IsUsed { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
