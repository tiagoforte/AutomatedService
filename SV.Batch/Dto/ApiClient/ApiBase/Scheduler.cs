using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class Scheduler
    {
        public Guid Id { get; set; }
        public Programacao Programacao { get; set; }
        public Guid ProgramacaoId { get; set; }
        public int TipoFrequencia { get; set; }
        public bool ProcessaDiaNaoUtil { get; set; }
        public bool Ativo { get; set; }
        public List<Frequencias> Frequencias { get; set; } = new List<Frequencias>();
        public bool EmExecucao { get; set; }
    }
}
