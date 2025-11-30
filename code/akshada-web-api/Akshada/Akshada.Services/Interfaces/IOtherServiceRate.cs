using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface IOtherServiceRate
    {
        bool SaveOtherServiceRate(DTO_OtherServiceRate otherServiceRate);

        PagedList<DTO_OtherServiceRateList> GetAllOtherServiceDate(DTO_FilterAndPaging filterAndPaging);

        DTO_OtherServiceRate GetByIdOtherServiceRate(string otherServiceRateRowId);

        bool DeleteByIdOtherServiceRate(string otherServiceRateRowId);

        bool UpdateOtherServiceRate(string otherServiceRowId, DTO_OtherServiceRate updateEntity);
    }
}
