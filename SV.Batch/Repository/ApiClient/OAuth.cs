using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SV.Batch.Interface.ApiClient;
using SV.Batch.Util;
using SV.Batch.Commands;
using SV.Batch.ValueObjects;
using SV.Batch.Dto.ApiClient.Core;
using System.Configuration;

namespace SV.Batch.Repository.ApiClient
{
    public class OAuth : IOAuth
    {
        private readonly RequestApis _request;
        private string _urlBase;
        private string _urlMethod = ConfigurationManager.AppSettings["URL_API_OAUTH"];
        private string _specificMethod { get; set; }

        public OAuth()
        {
            this._request = new RequestApis(this._urlBase, MediaType.Json, this._urlMethod, new User());
        }

        public Task<RestDto<string>> GetKey(Login login)
        {
            this._specificMethod = "key/";
            return _request.Post<string, Login>(login, _specificMethod);
        }

        public Task<RestDto<Jwt>> Auth(User userAuth)
        {
            this._specificMethod = "oauth/";
            return _request.Post<Jwt, User>(userAuth, _specificMethod);
        }

    }
}
