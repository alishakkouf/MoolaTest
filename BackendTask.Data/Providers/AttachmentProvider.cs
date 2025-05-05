using AutoMapper;
using BackendTask.Data.Models;
using BackendTask.Domain.Contract.AttachmentContract;
using BackendTask.Domain.Contract.BankCardContract;
using BackendTask.Domain.Models.Attachment;
using BackendTask.Domain.Models.BankCards;

namespace BackendTask.Data.Providers
{
    internal class AttachmentProvider : GenericRepository<AttachmentDomain, Attachment, CreateAttachmentDomain, long>, IAttachmentProvider
    {
        public AttachmentProvider(BackendTaskDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
