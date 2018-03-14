using SV.Batch.Commands;
using SV.Batch.Dto.ApiClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Interface.ApiClient
{
    public interface IOAuth
    {
        Task<RestDto<string>> GetKey(Login login);
        Task<RestDto<Jwt>> Auth(User userAuth);
    }
}
