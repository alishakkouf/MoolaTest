using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared;
using BackendTask.Shared.Enums;

namespace BackendTask.Data.Models
{
    public class Attachment : AuditedEntity, IEntity<long>
    {
        public long Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        /// <summary>
        /// Attachment Types:
        /// 1- Image
        /// </summary>
        public AttachmentType Type { get; set; } = AttachmentType.Image;

        /// <summary>
        /// Path of the attachment file relative to configured dir
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string RelativePath { get; set; }
    }
}
