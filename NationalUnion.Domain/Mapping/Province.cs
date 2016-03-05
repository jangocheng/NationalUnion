﻿using System;
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
    /// 省份对照表
    /// </summary>
    public class Province : IAggregateRoot
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int PId { get; set; }

        /// <summary>
        /// 所属大区编号
        /// </summary>
        public int RId { get; set; }

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

        public virtual Region Region { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
