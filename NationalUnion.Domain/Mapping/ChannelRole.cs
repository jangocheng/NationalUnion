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
    public class ChannelRole : IAggregateRoot
    {
        [Key]
        [StringLength(50)]
        public string RoleId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        [StringLength(200)]
        public string CreatePerson { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
