using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IPetOtherSrvService
    {
        Task<PagedList<DailyOtherServiceList>> GetOtherPetServices(DTO_FilterAndPaging filterAndPaging);

        Task<OtherServiceExecutionDetail> ReteriveOtherPetServices(string otherServiceRequestId, string otherServiceAssignedToRowId);

        Task<bool> SaveOtherPetServiceExecuted(string otherServiceRequestId, DTO_OtherServicesOfferedNew saveEntity);

        Task<bool> UpdateOtherPetServiceExecuted(string otherServiceRequestId, string serviceOfferedRowId, DTO_OtherServicesOfferedNew saveEntity);

        Task<bool> DeleteOtherPetServiceExecuted(string requestRowId);
    }
}
