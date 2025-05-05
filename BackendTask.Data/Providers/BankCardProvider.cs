using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackendTask.Data.Models;
using BackendTask.Domain.Contract.BankCardContract;
using BackendTask.Domain.Models.BankCards;
using BackendTask.Shared.Exceptions;
using BackendTask.Shared;
using Microsoft.EntityFrameworkCore;

namespace BackendTask.Data.Providers
{
    internal class BankCardProvider : GenericRepository<BankCardDomain, BankCard, CreateBankCardDomain, long>, IBankCardProvider
    {
        public BankCardProvider(BackendTaskDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<BankCardDomain> GetAsync(long id)
        {
            var entity = await BaseQuery.Include(x=>x.Attachment)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)) ??
                throw new EntityNotFoundException(typeof(BankCard).Name, id.ToString());

            return _mapper.Map<BankCardDomain>(entity);
        }

        protected override IQueryable<BankCard> ApplyKeywordFilter(IQueryable<BankCard> query, string keyword)
        {
            return query.Where(c => c.AccountNumber.Contains(keyword) ||
                                    c.IssuingBank.Contains(keyword) ||
                                    c.CardNumber.Contains(keyword));
        }
    }
}
