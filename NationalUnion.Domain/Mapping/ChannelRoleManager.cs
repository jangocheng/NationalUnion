using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Domain.Mapping
{
    public class ChannelRoleManager : IAggregateRoot
    {
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 ManagerId { get; set; }

        [Key, Column(Order = 2), StringLength(50)]
        public string RoleId { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual ChannelRole ChannelRole { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
