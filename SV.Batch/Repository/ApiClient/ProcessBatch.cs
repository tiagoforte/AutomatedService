using SV.Batch.Interface.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV.Batch.Dto.ApiClient.ApiBase;
using SV.Batch.Dto.ApiClient.Core;
using SV.Batch.Util;
using SV.Batch.ValueObjects;

namespace SV.Batch.Repository.ApiClient
{
    public class ProcessBatch : IProcessBatch
    {
        private readonly RequestApis _request;
        private string _specificMethod { get; set; }

        public ProcessBatch(string urlBase, string urlMethod)
        {
            this._request = new RequestApis(urlBase, MediaType.Json, urlMethod, Foundation.Params.Usuario);
        }

        public Task<RestDto<Dto.ApiClient.ApiBase.Log>> Process(ProgramacaoTarefa programacaoTarefa)
        {            
            return this._request.Post<Dto.ApiClient.ApiBase.Log, ProgramacaoTarefa>(programacaoTarefa, this._specificMethod);
        }

    }
}
