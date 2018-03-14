using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class Programacao
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public List<ProgramacaoTarefa> Tarefas { get; set; }
    }
}
