using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared;

namespace BackendTask.Data.Models
{
    public class Company : AuditedEntity, IEntity<long>
    {
        public long Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
