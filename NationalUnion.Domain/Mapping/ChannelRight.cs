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
    public class ChannelRight : IAggregateRoot
    {
        [Key, StringLength(200)]
        public string RightId { get; set; }

        [StringLength(50)]
        public string MoudleId { get; set; }

        [StringLength(50)]
        public string RoleId { get; set; }

        public int RightFlag { get; set; }

        public virtual ChannelMoudle ChannelMoudle { get; set; }

        public virtual ChannelRole ChannelRole { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
