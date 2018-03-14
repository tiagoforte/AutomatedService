using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Util
{
    public class Log
    {
        private string _nomeArquivoLog = null;

        public Log(string nomeArquivoLog)
        {
            this._nomeArquivoLog = nomeArquivoLog;
        }

        public void Escreve_Log(string strErro)
        {
            StreamWriter strm = null;
            try
            {
                MoveLogHistorico();

                var strArquivo = Path.Combine(ConfigurationManager.AppSettings["CAMINHO_LOG"], _nomeArquivoLog);
                strm = new StreamWriter(strArquivo, true);
                strm.WriteLine(DateTime.Now.ToString() + " - " + strErro);              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex?.InnerException?.Message);
            }
            finally
            {
                if (strm != null)
                {
                    strm.Flush();
                    strm.Close();
                }
            }
        }


        public void MoveLogHistorico()
        {
            try
            {

                string agora = DateTime.Now.ToString("ddMMyyyyHHmmssmmm");

                //verficar se existe o arquivo de log para realizar a validação de tamanho.
                if (System.IO.File.Exists(ConfigurationManager.AppSettings["CAMINHO_LOG"] + @"\" + _nomeArquivoLog) == true)
                {

                    FileInfo file = new FileInfo(ConfigurationManager.AppSettings["CAMINHO_LOG"] + @"\" + _nomeArquivoLog);

                    //Verifica se o arquivo é maior que o tamanho max. configurado
                    if (file.Length >= long.Parse(ConfigurationManager.AppSettings["TamanhoMaxArquivoLog"]))
                    {
                        //Cria pasta de Historico caso não Exista
                        if (System.IO.Directory.Exists(ConfigurationManager.AppSettings["CAMINHO_LOG"] + @"\Historico\") == false)
                        {
                            System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["CAMINHO_LOG"] + @"\Historico\");
                        }

                        System.IO.File.Move(ConfigurationManager.AppSettings["CAMINHO_LOG"] + @"\" + _nomeArquivoLog, ConfigurationManager.AppSettings["CAMINHO_LOG"] + @"\Historico\" + _nomeArquivoLog.Replace(".txt", "") + "_" + agora + ".txt");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
