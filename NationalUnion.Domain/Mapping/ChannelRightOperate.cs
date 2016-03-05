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
    public class ChannelRightOperate : IAggregateRoot
    {
        [Key, StringLength(200)]
        public string RightOperateId { get; set; }

        [StringLength(200)]
        public string RightId { get; set; }

        [StringLength(200)]
        public string KeyCode { get; set; }

        public int IsValid { get; set; }

        public virtual ChannelRight ChannelRight { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
