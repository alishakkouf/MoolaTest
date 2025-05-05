using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared.Enums;

namespace BackendTask.Domain.Models.TasksDomain
{
    public class UpdateCompanyDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
