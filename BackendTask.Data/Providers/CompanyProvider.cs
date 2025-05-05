using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackendTask.Data.Models;
using BackendTask.Domain.Contract.CompanyContract;
using BackendTask.Domain.Models.Company;
using BackendTask.Domain.Models.TasksDomain;

namespace BackendTask.Data.Providers
{
    internal class CompanyProvider : GenericRepository<CompanyDomain, Company, CreateCompanyDomain, long>, ICompanyProvider
    {
        public CompanyProvider(BackendTaskDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        protected override IQueryable<Company> ApplyKeywordFilter(IQueryable<Company> query, string keyword)
        {
            // Implement company-specific keyword filtering
            return query.Where(c => c.Name.Contains(keyword));
        }
    }
}
