using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Contract;
using BackendTask.Shared.Exceptions;
using BackendTask.Shared;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BackendTask.Data.Models;
using BackendTask.Shared.Extensions;

namespace BackendTask.Data.Providers
{
    public class GenericRepository<TDomain, TEntity, TCreateDomain, TKey> 
                     : IRepository<TDomain, TEntity, TCreateDomain, TKey>
                    where TEntity : class, IEntity<TKey>, IAuditedEntity
                    where TDomain : class
    {
        protected readonly BackendTaskDbContext _dbContext;
        protected readonly IMapper _mapper;

        public GenericRepository(BackendTaskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        protected virtual IQueryable<TEntity> BaseQuery => _dbContext.Set<TEntity>().Where(x=>x.IsDeleted != true);

        public async Task<TDomain> GetByIdAsync(TKey id)
        {
            var entity = await BaseQuery
                .FirstOrDefaultAsync(e => e.Id.Equals(id)) ??
                throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());

            return _mapper.Map<TDomain>(entity);
        }

        public async Task<PagedResultDto<TDomain>> GetPagedAsync(PagedAndSortedRequestDto filter)
        {
            var query = BaseQuery;

            // Apply filtering
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                query = ApplyKeywordFilter(query, filter.Keyword);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortingField))
            {                
                query = query.OrderBy(filter.SortingField, filter.SortingDir);
            }

            if (filter.Min.HasValue)
                query = query.Where(x => x.CreatedAt > filter.Min);

            if (filter.Max.HasValue)
                query = query.Where(x => x.CreatedAt < filter.Max);

                // Apply pagination
            var pagedResult = await ApplyPagingAsync<TDomain, TEntity, PagedAndSortedRequestDto>
                (query, filter, filter.IsPaginated());
            
            return new PagedResultDto<TDomain>(pagedResult);
        }

        public async Task<TDomain> AddAsync(TCreateDomain domain)
        {
            var entity = _mapper.Map<TEntity>(domain);

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task UpdateAsync(TDomain domain)
        {            
            // Get the ID from domain model
            var id = typeof(TDomain).GetProperty("Id").GetValue(domain);

            var existingEntity = await BaseQuery
                .FirstOrDefaultAsync(e => e.Id.Equals(id)) ?? 
                throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());

            _mapper.Map(domain, existingEntity);

            _dbContext.Set<TEntity>().Update(existingEntity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await BaseQuery
                .FirstOrDefaultAsync(e => e.Id.Equals(id)) ??
                throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());

            await SoftDeleteAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        protected async Task SoftDeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResultDto<TDomain>> ApplyPagingAsync<TDomain, TSource, TPagedRequest>(IQueryable<TSource> source, TPagedRequest request, bool isPaginated)
           where TDomain : class
           where TSource : class
           where TPagedRequest : PagedAndSortedRequestDto
        {
            var itemsCount = await source.CountAsync();


            if (isPaginated)
            {
                var pageNumber = request?.PageIndex ?? 1;
                pageNumber = pageNumber <= 0 ? 1 : pageNumber;

                var pageSize = request?.PageSize ?? Constants.DefaultPageSize;
                pageSize = pageSize <= 0 ? Constants.DefaultPageSize : pageSize;

                // apply pagination
                source = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                var resultList = await source.ToListAsync();

                var result = new PagedResultDto<TDomain>()
                {
                    TotalCount = itemsCount,
                    PagesSize = pageSize,
                    Items = _mapper.Map<List<TDomain>>(resultList),
                };

                return result;
            }
            else
            {
                var allData = await source.ToListAsync();

                var result = new PagedResultDto<TDomain>()
                {
                    TotalCount = itemsCount,
                    PagesSize = 1000,
                    Items = _mapper.Map<List<TDomain>>(allData),
                };

                return result;
            }

        }

        //To be overriden later
        protected virtual IQueryable<TEntity> ApplyKeywordFilter(IQueryable<TEntity> query, string keyword)
        {
            return query;
        }
    }
}
