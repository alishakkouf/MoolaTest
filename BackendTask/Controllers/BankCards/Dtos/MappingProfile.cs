using AutoMapper;
using BackendTask.Domain.Models.Attachment;

namespace BackendTask.API.Controllers.BankCards.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAttachmentDto, CreateAttachmentDomain>();
        }
    }
}
