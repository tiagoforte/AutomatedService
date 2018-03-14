using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Commands
{
    public class AtualizaStatusScheduler
    {
        public Guid ProgramacaoId { get; set; }
        public bool EmExecucao { get; set; }
    }
}
