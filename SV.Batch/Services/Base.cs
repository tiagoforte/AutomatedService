using SV.Batch.Commands;
using SV.Batch.Dto.ApiClient.ApiBase;
using SV.Batch.Dto.ApiClient.Core;
using SV.Batch.Interface.ApiClient;
using SV.Batch.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Services
{
    public class Base
    {

        private IScheduler _scheduler { get; set; }
        private IFeriados _feriados { get; set; }
        private IProcessBatch _processBatch { get; set; }

        public Base()
        {
            this._scheduler = new Repository.ApiClient.Scheduler();
            this._feriados = new Repository.ApiClient.Feriados();
        }
        public List<Dto.ApiClient.ApiBase.Scheduler> GetSchedulesForProcess(int hour, int minute)
        {

            var schedules = new List<Dto.ApiClient.ApiBase.Scheduler>();
            var currentTimer = new TimeSpan(hour, minute, 0);
            var currentDate = DateTime.Now;

            var schedulers = _scheduler.GetAll().Result;
            if (schedulers.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.RenovaToken();
                return this.GetSchedulesForProcess(hour, minute);
            }

            var schedulesAtivos = schedulers.Data.Where(x => x.Ativo && !x.EmExecucao && x.Frequencias.Any(y => y.Horas == currentTimer) && (x.TipoFrequencia == (int)Enums.EFrequencia.Diario || (x.TipoFrequencia == (int)Enums.EFrequencia.Semanal && x.Frequencias.Any(y => y.DiaDaSemana == (int)currentDate.DayOfWeek)) || (x.TipoFrequencia == (int)Enums.EFrequencia.Mensal && x.Frequencias.Any(y => y.Dias == currentDate.Day.ToString())))).ToList();
            var feriados = _feriados.GetAll().Result.Data.Where(x => x.Data.Date == currentDate.Date).ToList();

            foreach (var item in schedulesAtivos)
            {
                if (item.ProcessaDiaNaoUtil || (!item.ProcessaDiaNaoUtil && currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday && !feriados.Any()))
                {
                    schedules.Add(item);
                }
            }
            return schedules;
        }

        public bool Proccess(ProgramacaoTarefa programacaoTarefa)
        {
            var log = new Util.Log(ConfigurationManager.AppSettings["LogServico"]);
            RestDto<Log> response = null;
            try
            {
                _processBatch = new Repository.ApiClient.ProcessBatch(string.Empty, programacaoTarefa?.Tarefa.UrlAPI);
                response = _processBatch.Process(programacaoTarefa).Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.RenovaToken();
                    this.Proccess(programacaoTarefa);
                }
            }
            catch (Exception ex)
            {
                log.Escreve_Log($"URL Chamada: { programacaoTarefa.Tarefa.UrlAPI}");
                log.Escreve_Log(ex.Message);
                log.Escreve_Log(ex?.StackTrace);
                log.Escreve_Log(ex?.InnerException?.Message);
            }
            return response.Success;
        }

        private void RenovaToken()
        {
            Foundation.Params.Usuario.SetToken(new Auth().Authenticate());
        }

        public void AtualizaStatusEmExecucao(Guid ProgramacaoId, bool EmExecucao)
        {
            this._scheduler.AtualizaStatusExecucao(new Commands.AtualizaStatusScheduler { ProgramacaoId = ProgramacaoId, EmExecucao = EmExecucao });
        }

    }
}
