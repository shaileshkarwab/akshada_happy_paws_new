using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface ISystemParameterService
    {
        PagedList<DTO_SystemParameter> GetSystemParameter(DTO_FilterAndPaging filterAndPaging);

        DTO_SystemParameter GetSystemParameterById(string sysParamRowId);

        bool SaveSystemParameter(DTO_SystemParameter systemParameter);

        List<DTO_LookUp> GetSystemParameterDetailsByEnumId(Int32 enumID);

        List<DTO_LookUp> GetSystemParameterDetailsByEnumIds(List<Int32> enumID);

        List<DTO_SystemParameter> GetSystemParameterDataDetailsByEnumId(Int32 enumID);
    }
}
