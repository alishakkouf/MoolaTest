using BackendTask.Domain.Models.Departments;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.DepartmentContract
{
    public interface IDepartmentManager
    {
        Task<DepartmentDomain> GetByIdAsync(long id);
        Task<PagedResultDto<DepartmentDomain>> GetAllAsync(PagedAndSortedRequestDto request);
        Task AddAsync(CreateDepartmentDomain task);
        Task UpdateAsync(DepartmentDomain task);
        Task DeleteAsync(long id);
    }
}
