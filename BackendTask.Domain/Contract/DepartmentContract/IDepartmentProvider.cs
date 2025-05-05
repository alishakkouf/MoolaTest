using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Models.Departments;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.DepartmentContract
{
    public interface IDepartmentProvider
    {
        Task<DepartmentDomain> GetByIdAsync(long id);
        Task<PagedResultDto<DepartmentDomain>> GetPagedAsync(PagedAndSortedRequestDto request);
        Task<DepartmentDomain> AddAsync(CreateDepartmentDomain task);
        Task UpdateAsync(DepartmentDomain task);
        Task DeleteAsync(long id);
    }
}
