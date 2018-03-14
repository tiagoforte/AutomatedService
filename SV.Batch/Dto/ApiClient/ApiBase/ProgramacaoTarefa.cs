using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class ProgramacaoTarefa
    {

        public string UrlApi { get; set; }
        public Tarefa Tarefa { get; set; }
        public Guid TarefaId { get; set; }

        public Guid? TarefaDependenteId { get; set; }
        public int Sequencia { get; set; }

        public Guid ProgramacaoId { get; set; }
    }
}
