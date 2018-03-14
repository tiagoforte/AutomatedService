using SV.Batch.Commands;
using SV.Batch.Dto.ApiClient.ApiBase;
using SV.Batch.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;

namespace SV.Batch
{


    public class GerenciadorTarefas
    {

        private Timer tmrCheckTarefas;

        public void GerenciarTarefas()
        {

            var log = new Util.Log(ConfigurationManager.AppSettings["LogServico"]);
            try
            {
                log.Escreve_Log("Reiniciando a Execução dos Serviços");
            }
            catch (Exception e)
            {
                log.Escreve_Log($"Ocorreram erros ao Reiniciar os Serviços - {e.Message}");
                log.Escreve_Log(e?.InnerException?.Message);
            }

            tmrCheckTarefas = new System.Timers.Timer();
            tmrCheckTarefas.Elapsed += new System.Timers.ElapsedEventHandler(tmrCheckTarefas_Elapsed);
            tmrCheckTarefas.AutoReset = true;
            tmrCheckTarefas.Interval = 60000; //1 minuto            
            tmrCheckTarefas.Start();
        }

        void tmrCheckTarefas_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var log = new Util.Log(ConfigurationManager.AppSettings["LogServico"]);
            var objBase = new Services.Base();
            try
            {
                tmrCheckTarefas.Enabled = false;
                log.Escreve_Log("Verificando processos à serem Executados");

                //lista os processos.
                var schedulers = objBase.GetSchedulesForProcess(e.SignalTime.Hour, e.SignalTime.Minute);
                if (schedulers.Any())
                {
                    log.Escreve_Log($"Quantidade de Processos a serem executados: {schedulers.Count.ToString()}");
                    foreach (var sched in schedulers)
                    {
                        var tarefasLog = new List<TarefaLog>();
                        try
                        {
                            var threadTarefa = new System.Threading.Thread(
                                  delegate ()
                                  {
                                      log.Escreve_Log($"Executando Rotina: {sched.Programacao.Descricao}");
                                      objBase.AtualizaStatusEmExecucao(sched.ProgramacaoId, true);
                                      foreach (var tarefa in sched.Programacao.Tarefas.OrderBy(x => x.Sequencia))
                                      {
                                          try
                                          {
                                              if (tarefasLog.Any(x => x.Id == tarefa.TarefaDependenteId && !x.ISSuccess))
                                              {
                                                  break;
                                              }
                                              var result = objBase.Proccess(tarefa);
                                              tarefasLog.Add(new TarefaLog(tarefa.TarefaId, result));
                                          }
                                          catch (Exception ex)
                                          {
                                              log.Escreve_Log(ex.Message);
                                              log.Escreve_Log(ex?.StackTrace);
                                              log.Escreve_Log(ex?.InnerException?.Message);

                                              //caso der algum erro, restart o timer.
                                              tmrCheckTarefas.Interval = 60000;
                                              tmrCheckTarefas.Start();
                                          }
                                      }
                                      objBase.AtualizaStatusEmExecucao(sched.ProgramacaoId, false);
                                  });
                            threadTarefa.Start();
                        }
                        catch (Exception ex)
                        {
                            log.Escreve_Log(ex.Message);
                            log.Escreve_Log(ex?.StackTrace);
                            log.Escreve_Log(ex?.InnerException?.Message);

                            //caso der algum erro, restart o timer.
                            tmrCheckTarefas.Interval = 60000;
                            tmrCheckTarefas.Start();
                        }
                    }
                }
                else
                {
                    log.Escreve_Log("Não existem processos à serem Executados");
                }
                tmrCheckTarefas.Enabled = true;
            }
            catch (Exception ex)
            {

                log.Escreve_Log("Erro obter serviços para procesamento");
                log.Escreve_Log(ex.Message);
                log.Escreve_Log(ex?.StackTrace);
                log.Escreve_Log(ex?.InnerException?.Message);

                //caso der algum erro, restart o timer.
                tmrCheckTarefas.Interval = 60000;
                tmrCheckTarefas.Start();
            }
        }



    }
}
