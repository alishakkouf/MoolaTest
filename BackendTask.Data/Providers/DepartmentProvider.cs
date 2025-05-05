using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackendTask.Data.Models;
using BackendTask.Domain.Contract.CompanyContract;
using BackendTask.Domain.Contract.DepartmentContract;
using BackendTask.Domain.Models.Company;
using BackendTask.Domain.Models.Departments;
using BackendTask.Domain.Models.TasksDomain;

namespace BackendTask.Data.Providers
{
    internal class DepartmentProvider : GenericRepository<DepartmentDomain, Department, CreateDepartmentDomain, long>, IDepartmentProvider
    {
        public DepartmentProvider(BackendTaskDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        protected override IQueryable<Department> ApplyKeywordFilter(IQueryable<Department> query, string keyword)
        {
            // Implement company-specific keyword filtering
            return query.Where(c => c.Name.Contains(keyword));
        }
    }
}
