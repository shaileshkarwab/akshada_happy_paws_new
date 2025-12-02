using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface ISrvRequestService
    {
        List<DTO_WalkingServiceRequest> GetCustomerServiceRequests(string customerRowId, string petRowId);

        DTO_WalkingServiceRequest GetDetailsForWalkingService(string customerRowId, string petRowId, string serviceRequestRowId);

        bool UpdateCustomerPetWalkingServiceRequest(string customerRowId, string petRowId, string serviceRequestId, DTO_WalkingServiceRequest updateEntity);

        PagedList<DTO_WalkingServiceRequestQuery> GetPetServiceDetails(DTO_FilterAndPaging filterAndPagings);

        PagedList<DTO_WalingServiceList> GetWalkingServiceRequests(DTO_FilterAndPaging filterAndPagings);

        bool AssignUserToPetWalkingServiceSchedule(string customerRowId, string petRowId, string serviceRequestId, List<DTO_WalkingServiceRequestDayScheduleAssignedToUser> walkingServiceRequestDayScheduleAssignedToUsers);

        bool ChangeUserAndAssignNewTime(string customerRowId, string petRowId, string serviceRequestId, DTO_ChangeTimeOrUserForRequest changeTimeOrUserForRequest);

        bool UpdateChangeUserAndAssignNewTime(string customerRowId, string petRowId, string serviceRequestId, string assignedRequestId, DTO_ChangeTimeOrUserForRequest changeTimeOrUserForRequest);

        bool DeleteChangeUserAndAssignNewTime(string assignedRequestId);

        bool AddCustomerPetWalkingServiceRequest(string customerRowId, string petRowId, DTO_WalkingServiceRequest saveEntity);

        bool DeleteCustomerPetWalkingServiceRequest(string serviceRowId);

        bool UpdateWalkingServiceRequest(string walkingRequestRowId, DTO_WalkingRecord walkingRecord);

        List<DTO_WalkingServiceRequestServices> GetActiveWalkingServiceSlotsForCustomerIdPetId(string customerID, string petId, string selectedDate);

        bool AddWalkingServiceRequest(DTO_WalkingRecord walkingRecord);

        PagedList<DTO_GoogleFormRawData> GetGoogleGormRequests(DTO_FilterAndPaging filterAndPagings);

        bool DeleteGoogleGormRequests(string requestRowId);

        PagedList<DTO_ReciveFormSubmission> GetGoogleServiceRequests(DTO_FilterAndPaging filterAndPagings);

        bool DeleteGoogleServiceRequests(string rowId);
    }
}
