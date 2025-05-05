using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared.Enums;

namespace BackendTask.Domain.Models.Company
{
    public class CompanyDomain
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
