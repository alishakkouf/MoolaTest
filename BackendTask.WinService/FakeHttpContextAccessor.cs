using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BackendTask.WinService
{
    public class FakeHttpContextAccessor : IHttpContextAccessor
    {
        public HttpContext HttpContext
        {
            get => new DefaultHttpContext(); // Or mock with custom values
            set => throw new NotSupportedException();
        }
    }
}
