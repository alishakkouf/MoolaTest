using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Models.Transactions;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.TransactionsContract
{
    public interface ITransactoinProvider
    {
        Task<TransactionDomain> GetByIdAsync(long id);
        Task<PagedResultDto<TransactionDomain>> GetPagedAsync(PagedAndSortedRequestDto request);
        Task<List<TransactionDomain>> GetUserTransactionHistoryAsync(long id);
        Task<TransactionDomain> AddAsync(CreateTransactionDomain task);
        Task UpdateAsync(TransactionDomain task);
        Task DeleteAsync(long id);
    }
}
