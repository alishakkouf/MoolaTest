using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Domain.Contract.AttachmentContract;
using BackendTask.Shared.Enums;
using BackendTask.Shared.Exceptions;
using BackendTask.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;

namespace BackendTask.Manager
{
    public class WebStorageManager : IStorageManager
    {
        private const string WebRootFolder = "wwwroot";
        private static readonly string ImagesFolder = Path.Combine(Constants.UploadsFolderName, Constants.ImagesFolderName);
        private static readonly string[] ImageFileExtensions = { "jpeg", "jpg", "png" };

        private readonly IHostEnvironment _hostEnvironment;
        private readonly Uri _baseUri;
        private readonly IStringLocalizer _localizer;

        public WebStorageManager(
            IHostEnvironment hostEnvironment,
            IConfiguration configuration,
            IStringLocalizerFactory factory)
        {
            _hostEnvironment = hostEnvironment;
            _baseUri = new Uri(configuration[Constants.AppServerRootAddressKey] ?? "/");
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<string> SaveAttachmentAsync(IFormFile file, string fileName, AttachmentType type)
        {
            CheckFile(file, false, type);

            var relativePath = string.Empty;

            string _UploadFolder = Path.Combine(_hostEnvironment.ContentRootPath, WebRootFolder, ImagesFolder);

            if (!Directory.Exists(_UploadFolder))
            {
                Directory.CreateDirectory(_UploadFolder);
            }

            var extension = Path.GetExtension(file.FileName);

            if (_hostEnvironment.IsDevelopment())
            {
                var pathToSave = Path.Combine(_hostEnvironment.ContentRootPath, WebRootFolder, ImagesFolder, fileName);

                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                relativePath = Path.Combine(WebRootFolder, ImagesFolder, fileName);
            }
            else
            {
                var pathToSave = Path.Combine(_hostEnvironment.ContentRootPath, WebRootFolder, ImagesFolder, fileName);

                using (var stream = new FileStream(pathToSave, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                relativePath = Path.Combine(ImagesFolder, fileName);
            }

            return relativePath;
        }

        #region Private Methods

        private void CheckFile(IFormFile file, bool isProfileImage, AttachmentType type = AttachmentType.Image)
        {
            var fileAccepted = false;

            switch (type)
            {
                case AttachmentType.Image:
                    fileAccepted = ImageFileExtensions.Any(x => file.ContentType.Contains(x));
                    break;
            }

            if (!fileAccepted)
                throw new BusinessException(_localizer["FileTypeIsNotAcceptable"]);
        }

        public Task<string> GetUrlAsync(string relativePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
