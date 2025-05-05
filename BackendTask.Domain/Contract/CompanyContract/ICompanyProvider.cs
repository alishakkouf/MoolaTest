using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Models.Company;
using BackendTask.Domain.Models.TasksDomain;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.CompanyContract
{
    public interface ICompanyProvider
    {
        Task<CompanyDomain> GetByIdAsync(long id);
        Task<PagedResultDto<CompanyDomain>> GetPagedAsync(PagedAndSortedRequestDto request);
        Task<CompanyDomain> AddAsync(CreateCompanyDomain task);
        Task UpdateAsync(CompanyDomain task);
        Task DeleteAsync(long id);
    }
}
