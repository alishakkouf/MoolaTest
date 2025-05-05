using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Models.Attachment;

namespace BackendTask.Domain.Contract.AttachmentContract
{
    public interface IAttachmentProvider
    {
        /// <summary>
        /// Create attachment and save the file
        /// </summary>
        Task<AttachmentDomain> AddAsync(CreateAttachmentDomain newAttachment);
    }
}
