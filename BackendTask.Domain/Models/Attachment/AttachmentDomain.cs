using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared.Enums;

namespace BackendTask.Domain.Models.Attachment
{
    public class AttachmentDomain
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public AttachmentType? Type { get; set; }

        public long? RefId { get; set; }

        public string Url { get; set; }

        public string RelativePath { get; set; }

        public byte[] FileBytes { get; set; }

        public string ContentType { get; set; }
    }
}
