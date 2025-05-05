using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract
{
    public interface IRepository<TDomain, TEntity, TCreateDomain, TKey>
    {
        Task<TDomain> GetByIdAsync(TKey id);
        Task<PagedResultDto<TDomain>> GetPagedAsync(PagedAndSortedRequestDto filter);
        Task<TDomain> AddAsync(TCreateDomain entity);
        Task UpdateAsync(TDomain entity);
        Task DeleteAsync(TKey id);
    }
}
