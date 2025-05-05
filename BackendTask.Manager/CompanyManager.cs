using BackendTask.Domain.Contract;
using BackendTask.Domain.Contract.CompanyContract;
using BackendTask.Domain.Models.Company;
using BackendTask.Domain.Models.TasksDomain;
using BackendTask.Shared;

namespace BackendTask.Manager
{
    public class CompanyManager(ICompanyProvider provider/*IRepository<CompanyDomain, Company, CreateCompanyDomain, long> repository*/)
        : ICompanyManager
    {
        //private readonly IRepository<CompanyDomain, Company, CreateCompanyDomain, long> _repository = repository;
        private readonly ICompanyProvider _provider = provider;
        public async Task AddAsync(CreateCompanyDomain task)
        {
            await _provider.AddAsync(task);
        }

        public async Task DeleteAsync(long id)
        {
            await _provider.DeleteAsync(id);
        }

        public async Task<PagedResultDto<CompanyDomain>> GetAllAsync(PagedAndSortedRequestDto request)
        {
           return await _provider.GetPagedAsync(request);
        }

        public async Task<CompanyDomain> GetByIdAsync(long id)
        {
            return await _provider.GetByIdAsync(id);
        }

        public async Task UpdateAsync(CompanyDomain task)
        {
            await _provider.UpdateAsync(task);
        }
    }
}
