using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackendTask.Data.Models.Identity;
using BackendTask.Domain.Models.Attachment;
using BackendTask.Domain.Models.BankCards;
using BackendTask.Domain.Models.Company;
using BackendTask.Domain.Models.Departments;
using BackendTask.Domain.Models.TasksDomain;
using BackendTask.Domain.Models.Transactions;
using BackendTask.Domain.Models.UserAccount;

namespace BackendTask.Data.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDomain>().ReverseMap();
            CreateMap<CreateCompanyDomain, Company>();

            CreateMap<Department, DepartmentDomain>().ReverseMap();
            CreateMap<CreateDepartmentDomain, Department>();

            CreateMap<Transaction, TransactionDomain>().ReverseMap();
            CreateMap<CreateTransactionDomain, Transaction>();

            CreateMap<BankCard, BankCardDomain>()
                .ForMember(x=>x.Url, o=> o.MapFrom(x=> x.Attachment != null ?
                                                       x.Attachment.RelativePath :
                                                       string.Empty)).ReverseMap();
            CreateMap<CreateBankCardDomain, BankCard>();

            CreateMap<UserAccount, UserAccountDomain>();

            CreateMap<Attachment, AttachmentDomain>().ReverseMap();
            CreateMap<CreateAttachmentDomain, Attachment>();
        }
    }
}
