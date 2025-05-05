using BackendTask.Shared;
using BackendTask.Shared.Enums;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace BackendTask.API.Controllers.BankCards.Dtos
{
    public class CreateAttachmentDto : IValidatableObject
    {
        public string? Name { get; set; }

        /// <summary>
        /// Type of attachment: 1- Image
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Accepted Types: Jpeg, Png, Jpg with max file size 3 mb.
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public IFormFile File { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var factory = validationContext.GetService<IStringLocalizerFactory>();
            var localizer = factory.Create(typeof(CommonResource));
            var results = new List<ValidationResult>();

            if (!Enum.IsDefined(typeof(AttachmentType), Type))
                results.Add(new ValidationResult(localizer["InvalidEnumValue", nameof(Type)],
                    new[] { nameof(Type) }));

            const int maxFileSizeInBytes = 3 * 1024 * 1024; // 3 MB
            if (File?.Length > maxFileSizeInBytes)
            {
                results.Add(new ValidationResult(
                    localizer["FileSizeExceedsMaxFileSize", "3 MB"],
                    new[] { nameof(File) }));
            }

            return results;
        }
    }
}
