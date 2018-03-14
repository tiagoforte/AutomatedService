using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class TarefaLog
    {
        public TarefaLog(Guid id, bool success)
        {
            this.Id = id;
            this.ISSuccess = success;
        }

        public Guid Id { get; set; }
        public bool ISSuccess { get; set; }
    }
}
