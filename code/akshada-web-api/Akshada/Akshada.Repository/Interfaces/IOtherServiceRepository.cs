using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IOtherServiceRepository:IGenericRepository<OtherServicesOffered>
    {
        Task<IQueryable<DailyOtherServiceList>> GetOtherPetServices(DTO_FilterAndPaging filterAndPaging);

        Task<OtherServiceExecutionDetail> ReteriveOtherPetServices(string otherServiceRequestId, string otherServiceAssignedToRowId);
    }
}
