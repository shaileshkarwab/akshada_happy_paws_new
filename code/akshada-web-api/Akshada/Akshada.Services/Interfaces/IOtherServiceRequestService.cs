using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IOtherServiceRequestService
    {
        PagedList<DTO_OtherServiceRequest> GetAll(DTO_FilterAndPaging filterAndPaging);

        bool AddOtherServiceRequest(DTO_OtherServiceRequest otherServiceRequest);

        bool DeleteOtherServiceRequest(string rowID);

        bool AssignUserToOtherServiceRequest(string rowID, DTO_AssignOtherServiceRequestUser assignOtherServiceRequestUser);

        bool DeleteUserToOtherServiceRequest(string rowID);

        DTO_OtherServiceRequest GetOtherServiceRequestById(string rowID);

        DTO_AssignOtherServiceRequestUser OtherServiceRequestAssignedUser(string rowID, string assignedRequestRowId);

        bool UpdateServiceRequestAssignedUser(string rowID, string assignedRequestRowId, DTO_AssignOtherServiceRequestUser updateEntity);
    }
}
