using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
#if DEBUG
                    Service_SVBatch service = new Service_SVBatch();
                    service.StartDebug(new string[2]);
                    System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
                }
                else
                {

                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                new Service_SVBatch()
                    };
                    ServiceBase.Run(ServicesToRun);
                }
            }
            catch (Exception ex)
            {
                var log = new Util.Log(ConfigurationManager.AppSettings["LogServico"]);
                log.Escreve_Log(ex.Message);
                log.Escreve_Log(ex?.StackTrace);                
                log.Escreve_Log(ex?.InnerException?.Message);
            }
        }
    }
}
