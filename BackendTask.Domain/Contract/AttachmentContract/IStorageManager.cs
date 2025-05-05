using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace BackendTask.Domain.Contract.AttachmentContract
{
    public interface IStorageManager
    {
        /// <summary>
        /// Save attachment file depending on rootPath (wwwroot), and attachments folder
        /// </summary>
        Task<string> SaveAttachmentAsync(IFormFile file, string fileName, AttachmentType type);

        /// <summary>
        /// Get url.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<string> GetUrlAsync(string relativePath);
    }
}
