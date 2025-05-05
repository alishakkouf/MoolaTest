using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Models.BankCards;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.BankCardContract
{
    public interface IBankCardProvider
    {
        Task<BankCardDomain> GetAsync(long id);
        Task<PagedResultDto<BankCardDomain>> GetPagedAsync(PagedAndSortedRequestDto request);
        Task<BankCardDomain> AddAsync(CreateBankCardDomain task);
        Task UpdateAsync(BankCardDomain task);
        Task DeleteAsync(long id);
    }
}
