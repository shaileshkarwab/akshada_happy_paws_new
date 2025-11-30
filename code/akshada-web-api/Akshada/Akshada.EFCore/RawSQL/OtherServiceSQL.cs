using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.RawSQL
{
    public class OtherServiceSQL
    {
        public const string OhterServiceExecuteSQL = @"select 
a.row_id OtherRequestRowId,
b.row_id AssignedOtherServiceRowId,
ifnull(d.customer_name,a.customer_name) CustomerName,
ifnull(d.address,customer_address) Address,
ifnull(d.email,a.email) Email,
ifnull(d.mobile,a.mobile) Mobile,
ifnull(d.param_value, g.param_value) AreaLocation,
c.row_id UserID,
c.first_name FirstName,
c.last_name LastName,
e.row_id PetRowID,
ifnull(e.pet_name,a.pet_name) PetName,
ifnull(e.breed, h.param_value) PetBreed,
ifnull(e.colour, i.param_value) PetColour,
ifnull(pet_and_owner_image,pet_image) PetOwnerImage,
a.service_request_date ServiceRequestDate,
b.changed_request_date ChangeRequestDate,
b.from_time FromTime,
b.to_time ToTime,
b.assign_date AssignedDate,
b.remarks Remarks,
f.row_id ServiceSystemRowID,
f.param_value ServiceName,
OtherServiceOfferRowId,
OtherServiceOfferDate,
ServiceOfferedUserId,
ServiceOfferedFromTime,
ServiceOfferedToTime,
ServiceOfferedRemarks,
ServiceOfferedUserFirstName,
ServiceOfferedUserLastName
from other_service_request a
left outer join 
(
select a.id, a.area_location_system_id, a.customer_name, a.email, a.mobile, a.address, b.param_value 
from customer a
left outer join system_parameter b on  a.area_location_system_id = b.id
) d
on d.id = a.customer_id
left outer join (select 
a.row_id, a.id, a.pet_name, 
b.param_value breed, 
c.param_value colour,
pet_and_owner_image
from customer_pets a, system_parameter b, system_parameter c
where a.breed_system_id = b.id
and a.colour_id = c.id
) e
on e.id = a.pet_id
left outer join system_parameter g
on g.id = address_location_system_id
left outer join system_parameter h
on h.id = a.pet_colour_breed_id
left outer join system_parameter i
on i.id = a.pet_colour_system_id
-- service offered
left outer join
(
SELECT 
a.row_id OtherServiceOfferRowId,
a.service_offered_date OtherServiceOfferDate,
b.row_id ServiceOfferedUserId,
a.other_service_request_master_id,
a.from_time ServiceOfferedFromTime,
a.to_time ServiceOfferedToTime,
a.remarks ServiceOfferedRemarks,
b.first_name ServiceOfferedUserFirstName,
b.last_name ServiceOfferedUserLastName
FROM other_services_offered a, user_master b
where a.service_offered_user_id = b.id
) g on g.other_service_request_master_id = a.id
, assign_other_service_request_user b,
user_master c, system_parameter f
where a.row_id = @otherServiceRequestId
and a.id = b.other_service_request_master_id
and b.row_id = @otherServiceAssignedToRowId
and c.id = b.assigned_to_user_id
and f.id = required_service_system_id";
    }
}
