using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Dto.ApiClient.ApiBase
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string UrlAPI { get; set; }
        public string Funcao { get; set; }
        public bool Ativo { get; set; }
    }
}
