using SV.Batch.Interface.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV.Batch.Dto.ApiClient.ApiBase;
using SV.Batch.Dto.ApiClient.Core;
using SV.Batch.Util;
using SV.Batch.Commands;
using SV.Batch.ValueObjects;
using System.Configuration;

namespace SV.Batch.Repository.ApiClient
{
    public class Feriados : IFeriados
    {

        private readonly RequestApis _request;
        private string _urlBase;
        private string _urlMethod = ConfigurationManager.AppSettings["URL_API_BASE_FERIADO"];
        private string _specificMethod { get; set; }

        public Feriados()
        {
            this._request = new RequestApis(this._urlBase, MediaType.Json, this._urlMethod, Foundation.Params.Usuario);
        }

        public Task<RestListDto<Feriado>> GetAll()
        {
            return this._request.Get<Dto.ApiClient.ApiBase.Feriado, Guid>(string.Empty, new List<Guid>(), this._specificMethod);
        }

    }
}
