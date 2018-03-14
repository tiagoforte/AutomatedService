using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.Core
{
    public class RestListDto<T>
    {
        public List<T> Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
