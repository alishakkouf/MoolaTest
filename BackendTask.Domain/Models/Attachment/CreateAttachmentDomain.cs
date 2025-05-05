using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BackendTask.Domain.Models.Attachment
{
    public class CreateAttachmentDomain
    {
        public string Name { get; set; }

        public byte Type { get; set; }

        public IFormFile File { get; set; }

        public string RelativePath { get; set; }
    }
}
