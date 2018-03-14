using SV.Batch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV.Batch.Util;
using SV.Batch.Commands;
using SV.Batch.ValueObjects;
using SV.Batch.Dto.ApiClient.Core;
using SV.Batch.Dto.ApiClient.ApiBase;
using SV.Batch.Interface.ApiClient;
using System.Configuration;

namespace SV.Batch.Repository.ApiClient
{
    public class Scheduler : IScheduler
    {

        private readonly RequestApis _request;
        private string _urlBase;
        private string _urlMethod = ConfigurationManager.AppSettings["URL_API_BASE_SCHEDULER"];
        private string _specificMethod { get; set; }

        public Scheduler()
        {
            this._request = new RequestApis(this._urlBase, MediaType.Json, this._urlMethod, Foundation.Params.Usuario);
        }

        Task<RestListDto<Dto.ApiClient.ApiBase.Scheduler>> IScheduler.GetAll()
        {
            return this._request.Get<Dto.ApiClient.ApiBase.Scheduler, Guid>(string.Empty, new List<Guid>(), this._specificMethod);
        }

        public Task<RestDto<Dto.ApiClient.ApiBase.Log>> AtualizaStatusExecucao(Commands.AtualizaStatusScheduler command)
        {
            this._specificMethod = "/atualizaStatusEmExecucao/";
            return _request.Put<Dto.ApiClient.ApiBase.Log, Commands.AtualizaStatusScheduler>(command, _specificMethod);
        }
    }
}
