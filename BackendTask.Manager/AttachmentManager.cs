using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Contract.AttachmentContract;
using BackendTask.Domain.Models.Attachment;
using BackendTask.Shared.Enums;
using BackendTask.Shared;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace BackendTask.Manager
{
    public class AttachmentManager : IAttachmentManager
    {
        private readonly IAttachmentProvider _attachmentProvider;
        private readonly IStorageManager _storageManager;
        private readonly IStringLocalizer _localizer;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public AttachmentManager(IAttachmentProvider attachmentProvider,
                                IStorageManager storageManager,
                                IHostEnvironment hostEnvironment,
                                IConfiguration configuration,
                                IStringLocalizerFactory factory)
        {
            _attachmentProvider = attachmentProvider;
            _storageManager = storageManager;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<AttachmentDomain> CreateAndSaveAsync(CreateAttachmentDomain command)
        {
            string fileName = string.IsNullOrEmpty(command.Name) ?
                                Path.GetFileName(command.File.FileName) : command.Name;

            command.Name = fileName;
            command.Type = (byte)AttachmentType.Image;

            command.RelativePath = await _storageManager.SaveAttachmentAsync(command.File, fileName, AttachmentType.Image);

            var attachment = await _attachmentProvider.AddAsync(command);

            attachment.Url = GetFullUrl(attachment.RelativePath);

            return attachment;
        }

        private string GetFullUrl(string relativePath)
        {
            var url = string.Empty;

            string basePath = _hostEnvironment.ContentRootPath;
            string folderName = "Attachments";

            var _serverBaseUrl = _configuration["App:ServerRootAddress"];

            if (_hostEnvironment.IsDevelopment())
                url = Path.GetFullPath(Path.Combine(basePath, folderName, relativePath));
            else
                url = $"{_serverBaseUrl}{relativePath.Replace("\\", "/")}";
                        
            return url;
        }
    }
}
