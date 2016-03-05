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
    public class ChannelMoudle : IAggregateRoot
    {
        [Key]
        [StringLength(50)]
        public string MoudleId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string EnglishName { get; set; }

        [StringLength(50)]
        public string ParentId { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        [StringLength(200)]
        public string Iconic { get; set; }

        public int Sort { get; set; }

        [StringLength(4000)]
        public string Remark { get; set; }

        public int MoudleState { get; set; }

        [StringLength(200)]
        public string CreatePerson { get; set; }

        public DateTime CreateTime { get; set; }

        public int IsLast { get; set; }

        [Timestamp]
        public string Version { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
