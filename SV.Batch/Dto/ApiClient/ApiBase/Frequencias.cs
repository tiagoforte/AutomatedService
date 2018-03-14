using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class Frequencias
    {
        public string Dias { get; set; }
        public TimeSpan Horas { get; set; }
        public int DiaDaSemana { get; set; }
    }
}
