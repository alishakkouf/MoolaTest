using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Domain.Models.Departments
{
    public class CreateDepartmentDomain
    {
        public string Name { get; set; }

        public int CompanyId { get; set; }
    }
}
