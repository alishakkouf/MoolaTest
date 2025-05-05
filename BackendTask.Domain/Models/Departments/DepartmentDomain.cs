using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Domain.Models.Departments
{
    public class DepartmentDomain
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }
    }
}
