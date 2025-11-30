using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.RawSQL
{
    public class DashboardSQL
    {
        public const string ServiceAndLocationWiseCount = @"select 
c.param_value as  ServiceName , 
ifnull(e.param_value,'NA') as  AreaLocation,  
count(*)  as TotalCount  from walking_service_request a, 
walking_service_request_days b, 
system_parameter c,
customer d
left outer join system_parameter e on e.id = d.area_location_system_id
where @date between date(from_date) and date(to_date)
and a.id = b.walking_service_request_master_id
and c.id = a.service_system_id
and b.is_selected = 1
and d.id = a.customer_id
and DAYNAME(@date) = week_day_name
group by c.param_value, e.param_value";
    }
}
