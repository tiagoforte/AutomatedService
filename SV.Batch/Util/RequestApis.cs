using SV.Batch.Commands;
using SV.Batch.Dto.ApiClient.Core;
using SV.Batch.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Util
{
    public class RequestApis
    {
        private string _urlBase { get; set; }
        private string _urlMethod { get; set; }
        private MediaType _mediaType { get; set; }
        public User _user { get; set; }

        public RequestApis(string urlBase, MediaType mediaType, string urlMethod, User user)
        {
            this._mediaType = mediaType;
            this._urlBase = urlBase;
            this._urlMethod = urlMethod;
            this._user = user;
        }

        public async Task<RestDto<TResult>> Put<TResult, TParameter>(TParameter value, string specificMethod)
        {

            var json = Serialize.SerializeObject<TParameter>(value);
            using (var client = RestClient.GetClient(_urlBase, _mediaType, _user))
            {
                return await client.PutAsyncGeneric<RestDto<TResult>>($"{_urlMethod}{specificMethod}", new StringContent(json, Encoding.UTF8, _mediaType.Value));
            }
        }


        public async Task<RestDto<TResult>> Post<TResult, TParameter>(TParameter value, string specificMethod)
        {

            var json = Serialize.SerializeObject<TParameter>(value);
            using (var client = RestClient.GetClient(_urlBase, _mediaType, _user))
            {
                return await client.PostAsyncGeneric<RestDto<TResult>>($"{_urlMethod}{specificMethod}", new StringContent(json, Encoding.UTF8, _mediaType.Value));
            }
        }

        public async Task<RestListDto<TResult>> Get<TResult, TParameter>(string parameterQueryString, List<TParameter> valuesQueryString, string specificMethod)
        {

            var queryString = QueryStringFormat.BuilderQueryString<TParameter>(parameterQueryString, valuesQueryString);
            using (var client = RestClient.GetClient(_urlBase, _mediaType, _user))
            {
                return await client.GetAsyncGeneric<RestListDto<TResult>>($"{_urlMethod}{specificMethod}/?{queryString}");
            }
        }


    }
}
