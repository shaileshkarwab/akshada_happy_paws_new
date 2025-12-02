using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.RawSQL
{
    public static class WalkingServiceRecord
    {
        public const string WalkingServiceSQL = @"WITH RECURSIVE date_series AS (
    SELECT DATE(@fromDate) AS dt 
    UNION ALL
    SELECT dt + INTERVAL 1 DAY
    FROM date_series
    WHERE dt < DATE(@toDate)
)
select 
a.ServiceDate,
a.DayName,
a.WalkingRequestID,
a.RequestDayId,
a.ScheduleRowId,
a.ScheduleId,
a.walking_request_master_id,
a.walking_request_day_master_id,
a.walking_request_schedule_master_id,
ifnull(d.change_from_time, FromTime) as FromTime,
ifnull(d.change_to_time, ToTime) as ToTime,
a.CustomerName,
a.customer_id,
a.CustomerIsActive,
a.CustomerRowId,
a.PetName,
a.PetAndOwnerImage,
a.PetId,
a.Breed,
a.Colour,
a.AreaLocationName,
a.AreaLocationRowId,
a.Address,
a.Mobile,
a.Email,
a.WeekDayName,
a.FromDate,
a.ToDate,
a.DaysPending,
ifnull(d.first_name, UserFirstName) as UserFirstName,
ifnull(d.last_name, UserLastName) as UserLastName,
ifnull(d.row_id, UserRowId) as UserRowId,
a.ServiceSystemRowId,
a.ServiceSystemValue,
a.FrequencySystemRowId,
a.FrequencySystemValue,
b.row_id WalkingServiceRecordId,
VaccinationDueDate,
VaccinationPendingDays,
d.row_id as NewUserAssignToWalkingServiceRowId
from
(
SELECT 
dt ServiceDate,
DAYNAME(dt) as DayName,
a.row_id as WalkingRequestID,
g.row_id as RequestDayId,
h.row_id as ScheduleRowId,
h.id as ScheduleId,

a.id as walking_request_master_id,
g.id as walking_request_day_master_id,
h.id as walking_request_schedule_master_id,

h.from_time as FromTime,
h.to_time as ToTime,
b.customer_name  as CustomerName,
b.id  as customer_id,
b.is_active  as CustomerIsActive,
b.row_id as CustomerRowId,
c.pet_name as PetName,
c.pet_and_owner_image as PetAndOwnerImage,
c.row_id as PetId,
c.id as pet_id,
d.param_value as Breed,
e.param_value as Colour,
f.param_value as AreaLocationName,
f.row_id as AreaLocationRowId,
b.address as Address,
b.mobile as Mobile,
b.email as Email,
g.week_day_name as WeekDayName,
a.from_date as FromDate,
a.to_date as ToDate,
DATEDIFF(a.to_date,dt) as DaysPending,
i.first_name as UserFirstName,
i.last_name as UserLastName,
i.row_id as UserRowId,
j.row_id ServiceSystemRowId,
j.param_value ServiceSystemValue,
k.row_id FrequencySystemRowId,
k.param_value FrequencySystemValue
from date_series ,
walking_service_request a, customer b
left outer join system_parameter f
on f.id = b.area_location_system_id
, customer_pets c, system_parameter d, system_parameter e, walking_service_request_days g, 
walking_service_request_day_schedule h
left outer join (select walking_service_request_day_schedule_id, b.first_name, b.last_name, b.row_id 
from  walking_service_request_day_schedule_assigned_to_user a, user_master b
where a.user_id = b.id
) i on h.id = i.walking_service_request_day_schedule_id
, system_parameter j , system_parameter k  
where 1 = 1
and a.customer_id = b.id
and a.pet_id = c.id
and c.customer_id = a.customer_id
and d.id = c.breed_system_id
and e.id = c.colour_id
and g.walking_service_request_master_id = a.id
and g.is_selected = 1
and DATE(dt)  >=  str_to_date(@fromDate,'%Y-%m-%d')
and DATE(dt) <= str_to_date(@toDate,'%Y-%m-%d')
and DAYNAME(dt) = g.week_day_name
and h.walking_service_request_days_master_id = g.id
and DATE(dt) between a.from_date and a.to_date
and j.id = a.service_system_id
and k.id = a.frequency_system_id
) a
left outer join walking_service_record b 
on b.walking_service_day_schedule_master_id = a.ScheduleId
and date(ServiceDate) = date(b.service_offered_date)
left outer join
(
select 
c.row_id as customer_id, 
d.row_id as pet_id, 
max(due_date) as VaccinationDueDate, datediff(max(due_date),@fromDate) as VaccinationPendingDays  
from vaccination_record a, vaccination_record_detail b, customer c, customer_pets d
where a.id = b.vaccination_record_master_id
and c.id = a.customer_id
and d.id = a.pet_id
and d.customer_id = a.customer_id
group  by
customer_id, pet_id
) c on c.customer_id = a.CustomerRowId
and c.pet_id = a.PetId
left outer join
(
SELECT 
assign_date,
a.row_id, 
b.id, 
b.first_name, 
b.last_name, 
a.customer_id, 
a.pet_id,  
walking_request_master_id,
walking_request_day_master_id,
walking_request_schedule_master_id,
change_from_time,
change_to_time
FROM new_user_assign_to_walking_service a, user_master b
where a.user_id = b.id
) d 
on 1 = 1
and date(d.assign_date) = date(ServiceDate)
and d.customer_id = a.customer_id
and d.pet_id = a.pet_id
and d.walking_request_master_id = a.walking_request_master_id
and d.walking_request_day_master_id = a.walking_request_day_master_id
and d.walking_request_schedule_master_id = a.walking_request_schedule_master_id
order by ServiceDate";

        public const string OtherServiceSQL = @"WITH RECURSIVE date_series AS (
    SELECT DATE(@fromDate) AS ServiceDate 
    UNION ALL
    SELECT ServiceDate  + INTERVAL 1 DAY
    FROM date_series
    WHERE ServiceDate  < DATE(@toDate)
)
select * from date_series a, 
(
select 
i.param_value ServiceName,
i.row_id ServiceRowId,
a.row_id RequestRowId,
a.service_required_on_date ServiceRequiredOnDate,
changed_request_date ChangedRequestDate,
ifnull(b.customer_name, a.customer_name) CustomerName,
ifnull(b.mobile, a.mobile) CustomerMobile,
ifnull(b.email, a.email) CustomerEmail,
ifnull(b.address, a.customer_address) CustomerAddress,
ifnull(b.param_value,d.param_value) AreaLocation,
ifnull(b.row_id,d.row_id) AreaLocationRowId,
ifnull(c.pet_name,a.pet_name) PetName,
ifnull(c.breed,e.param_value) Breed,
ifnull(c.breedRowId,e.row_id) BreedRowId,
ifnull(c.colour,f.param_value) Colour,
g.from_time FromTime,
g.to_time ToTime,
h.first_name UserFirstName,
h.last_name userLastName,
concat(h.first_name, ' ', h.last_name) AssignedTo,
g.row_id AssignedToUserRowId,
j.row_id ServiceOfferedRowId
from other_service_request a
left outer join 
(select a.id, b.param_value, a.customer_name, a.address,mobile,email, b.row_id from customer a, system_parameter b 
where a.area_location_system_id = b.id
) b on a.customer_id = b.id
left outer join 
(select a.pet_name, 
a.id, 
a.customer_id, 
b.param_value breed, 
c.param_value colour,
b.row_id breedRowId, 
c.row_id colourRowId
from customer_pets a, system_parameter b, system_parameter c
where b.id = a.breed_system_id
and c.id = a.colour_id
) c on a.pet_id = c.id
and c.customer_id = a.customer_id
left outer join system_parameter d
on d.id = a.address_location_system_id
left outer join system_parameter e on e.id = a.pet_colour_breed_id
left outer join system_parameter f on f.id = a.pet_colour_system_id
left outer join other_services_offered j on j.other_service_request_master_id = a.id
,
assign_other_service_request_user g, user_master h, system_parameter i 
where date(changed_request_date) between @fromDate and @toDate
and g.other_service_request_master_id = a.id
and h.id = assigned_to_user_id
and i.id = required_service_system_id
) b
where a.ServiceDate  = b.ChangedRequestDate
order by a.ServiceDate";


    }
}
