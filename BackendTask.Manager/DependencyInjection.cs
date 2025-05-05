using BackendTask.Domain.Contract.AttachmentContract;
using BackendTask.Domain.Contract.BankCardContract;
using BackendTask.Domain.Contract.CompanyContract;
using BackendTask.Domain.Contract.DepartmentContract;
using BackendTask.Domain.Contract.TransactionsContract;
using BackendTask.Domain.Contract.UserContract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTask.Manager
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureManagerModule(this IServiceCollection services,
                                                                     IConfiguration configuration)
        {
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ICompanyManager, CompanyManager>();
            services.AddScoped<IBankCardManager, BankCardManager>();
            services.AddScoped<ITransactionsManager, TransactionsManager>();
            services.AddScoped<IDepartmentManager, DepartmentManager>();
            services.AddScoped<IAttachmentManager, AttachmentManager>();
            services.AddScoped<IStorageManager, WebStorageManager>();

            return services;
        }
    }
}
