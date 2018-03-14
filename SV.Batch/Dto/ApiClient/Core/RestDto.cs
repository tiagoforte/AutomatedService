using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.Core
{
    public class RestDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
