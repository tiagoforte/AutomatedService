using SV.Batch.Dto.ApiClient.ApiBase;
using SV.Batch.Dto.ApiClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Interface.ApiClient
{
    public interface IFeriados
    {
        Task<RestListDto<Feriado>> GetAll();
    }
}
