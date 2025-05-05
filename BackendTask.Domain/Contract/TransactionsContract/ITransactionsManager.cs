using BackendTask.Domain.Models.Transactions;
using BackendTask.Shared;

namespace BackendTask.Domain.Contract.TransactionsContract
{
    public interface ITransactionsManager
    {
        Task<TransactionDomain> GetByIdAsync(long id);
        Task<PagedResultDto<TransactionDomain>> GetAllAsync(PagedAndSortedRequestDto request);
        Task<List<TransactionDomain>> GetUserTransactionHistoryAsync(long id);
        Task AddAsync(CreateTransactionDomain task);
        Task UpdateAsync(TransactionDomain task);
        Task DeleteAsync(long id);
    }
}
