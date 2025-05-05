using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Contract.DepartmentContract;
using BackendTask.Domain.Contract;
using BackendTask.Shared;
using BackendTask.Domain.Models.Departments;

namespace BackendTask.Manager
{
    public class DepartmentManager(IDepartmentProvider provider)
        : IDepartmentManager
    {
        private readonly IDepartmentProvider _provider = provider;

        public async Task AddAsync(CreateDepartmentDomain task)
        {
            await _provider.AddAsync(task);
        }

        public async Task DeleteAsync(long id)
        {
            await _provider.DeleteAsync(id);
        }

        public async Task<PagedResultDto<DepartmentDomain>> GetAllAsync(PagedAndSortedRequestDto request)
        {
            return await _provider.GetPagedAsync(request);
        }

        public async Task<DepartmentDomain> GetByIdAsync(long id)
        {
            return await _provider.GetByIdAsync(id);
        }

        public async Task UpdateAsync(DepartmentDomain task)
        {
            await _provider.UpdateAsync(task);
        }
    }
}

