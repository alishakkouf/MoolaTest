using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackendTask.Data.Models;
using BackendTask.Domain.Contract.TransactionsContract;
using BackendTask.Domain.Models.Transactions;
using BackendTask.Shared;
using BackendTask.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackendTask.Data.Providers
{
    public class TransactoinProvider : GenericRepository<TransactionDomain, Transaction, CreateTransactionDomain, long>, ITransactoinProvider
    {
        public TransactoinProvider(BackendTaskDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<List<TransactionDomain>> GetUserTransactionHistoryAsync(long userId)
        {
            var bankCard = await _dbContext.BankCards.FirstOrDefaultAsync(x=>x.UserId == userId) ??
                throw new EntityNotFoundException(nameof(BankCard), userId.ToString());

            var query = await BaseQuery.AsNoTracking().OrderBy(x=>x.CreatedAt).Where(x=>x.BankCardId == bankCard.Id).ToListAsync();

            return _mapper.Map<List<TransactionDomain>>(query);
        }

        protected override IQueryable<Transaction> ApplyKeywordFilter(IQueryable<Transaction> query, string keyword)
        {
            return query;
        }
    }
}
