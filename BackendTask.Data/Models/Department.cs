using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Data.Models.Identity;
using BackendTask.Shared;

namespace BackendTask.Data.Models
{
    public class Department : AuditedEntity, IEntity<long>
    {
        public long Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }
        public long CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        public virtual ICollection<UserAccount> Users { get; set; }
    }
}
