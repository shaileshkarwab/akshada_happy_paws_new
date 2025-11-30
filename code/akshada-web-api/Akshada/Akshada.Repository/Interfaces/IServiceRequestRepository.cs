using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IServiceRequestRepository:IGenericRepository<WalkingServiceRequest>
    {
        List<WalkingServiceRequest> GetCustomerServiceRequests(string customerRowId, string petRowId);

        WalkingServiceRequest GetDetailsForWalkingService(string customerRowId, string petRowId, string serviceRequestRowId);

        bool UpdateCustomerPetWalkingServiceRequest(string customerRowId, string petRowId, string serviceRequestId, DTO_WalkingServiceRequest updateEntity);

        IQueryable<WalkingServiceRequestQuery> GetPetServiceDetails(DTO_FilterAndPaging filterAndPagings);

        IQueryable<WalkingServiceRequest> GetWalkingServiceRequests();
    }
}
