using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Contract.BankCardContract;
using BackendTask.Domain.Contract;
using BackendTask.Domain.Models.BankCards;
using BackendTask.Shared;
using BackendTask.Domain.Contract.UserContract;
using BackendTask.Shared.Exceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace BackendTask.Manager
{
    public class BankCardManager(IAccountManager accountManager,
                                 IHostEnvironment hostEnvironment,
                                 IConfiguration configuration,
                                 IBankCardProvider provider)
        : IBankCardManager
    {
        private readonly IAccountManager _accountManager = accountManager;
        private readonly IBankCardProvider _provider = provider;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IConfiguration _configuration = configuration;

        public async Task AddAsync(CreateBankCardDomain input)
        {
            if (!await _accountManager.CheckExistedUserAsync(input.UserId))
                throw new BusinessException("The user is not existed!");

            await _provider.AddAsync(input);
        }

        public async Task DeleteAsync(long id)
        {
            await _provider.DeleteAsync(id);
        }

        public async Task<PagedResultDto<BankCardDomain>> GetAllAsync(PagedAndSortedRequestDto request)
        {
            return await _provider.GetPagedAsync(request);
        }

        public async Task<BankCardDomain> GetAsync(long id)
        {
            var data = await _provider.GetAsync(id);

            if (!string.IsNullOrEmpty(data.Url))
            {
                string basePath = _hostEnvironment.ContentRootPath;
                string folderName = "Attachments";

                var _serverBaseUrl = _configuration["App:ServerRootAddress"];

                if (_hostEnvironment.IsDevelopment())
                    data.Url = Path.GetFullPath(Path.Combine(basePath, folderName, data.Url));
                else
                    data.Url = $"{_serverBaseUrl}{data.Url.Replace("\\", "/")}";

            }


            return data;
        }

        public async Task UpdateAsync(BankCardDomain input)
        {
            await _provider.UpdateAsync(input);
        }
    }
}
