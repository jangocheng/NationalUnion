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
    public class ChannelMoudleOperate : IAggregateRoot
    {
        [Key]
        [StringLength(50)]
        public string MoudleOperateId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string MoudleId { get; set; }

        [StringLength(200)]
        public string KeyCode { get; set; }

        public int IsValid { get; set; }

        public int Sort { get; set; }

        public virtual ChannelMoudle ChannelMoudle { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
