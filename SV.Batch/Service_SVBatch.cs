using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch
{

    public partial class Service_SVBatch : ServiceBase
    {



        public Service_SVBatch()
        {


            InitializeComponent();
        }

#if DEBUG
        public void StartDebug(string[] args)
        {
            OnStart(args);
        }
#endif

        protected override void OnStart(string[] args)
        {
            try
            {
                GerenciadorTarefas gerenciadorTarefas = new GerenciadorTarefas();
                gerenciadorTarefas.GerenciarTarefas();
            }
            catch (Exception ex)
            {
                var log = new Util.Log(ConfigurationManager.AppSettings["LogServico"]);
                log.Escreve_Log(ex.Message);
                log.Escreve_Log(ex?.InnerException?.Message);
                log.Escreve_Log(ex?.StackTrace);
            }
        }

        protected override void OnStop()
        {

        }

    }
}
