using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class Feriado
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int? RecorrenteAte { get; set; }
    }
}
