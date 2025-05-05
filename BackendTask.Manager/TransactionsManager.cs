using BackendTask.Domain.Contract;
using BackendTask.Shared;
using BackendTask.Domain.Models.Transactions;
using BackendTask.Domain.Contract.TransactionsContract;

namespace BackendTask.Manager
{
    public class TransactionsManager(ITransactoinProvider provider)
        : ITransactionsManager
    {
        private readonly ITransactoinProvider _provider = provider;

        public async Task AddAsync(CreateTransactionDomain task)
        {
            await _provider.AddAsync(task);
        }

        public async Task DeleteAsync(long id)
        {
            await _provider.DeleteAsync(id);
        }

        public async Task<PagedResultDto<TransactionDomain>> GetAllAsync(PagedAndSortedRequestDto request)
        {
            return await _provider.GetPagedAsync(request);
        }

        public async Task<List<TransactionDomain>> GetUserTransactionHistoryAsync(long id)
        {
            return await _provider.GetUserTransactionHistoryAsync(id);
        }

        public async Task<TransactionDomain> GetByIdAsync(long id)
        {
            return await _provider.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TransactionDomain task)
        {
            await _provider.UpdateAsync(task);
        }
    }
}

