using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Models.BankCards;
using BackendTask.Domain.Models.TasksDomain;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.BankCardContract
{
    public interface IBankCardManager
    {
        Task<BankCardDomain> GetAsync(long id);
        Task<PagedResultDto<BankCardDomain>> GetAllAsync(PagedAndSortedRequestDto request);
        Task AddAsync(CreateBankCardDomain task);
        Task UpdateAsync(BankCardDomain task);
        Task DeleteAsync(long id);
    }
}
