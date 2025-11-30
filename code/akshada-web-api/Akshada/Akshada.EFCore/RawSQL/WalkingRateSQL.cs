using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.RawSQL
{
    public class WalkingRateSQL
    {
        public const string SQLQuery = @"select 
c.regular_rate as RegularRate,
c.special_day_rate as SpecialDayRate
from service_rate_master a, system_parameter b, service_rate_master_detail c, system_parameter d,
(
select 
b.row_id service_row_id,
d.row_id location_row_id,
max(datediff(effective_date, @from_date)) min_date_diff
from service_rate_master a, system_parameter b, service_rate_master_detail c, system_parameter d
where a.service_system_id = b.id
and c.service_master_id = a.id
and d.id = c.location_system_id
and a.is_active = 1
and effective_date <= @from_date 
group by
b.row_id,
d.row_id
) e
where a.service_system_id = b.id
and c.service_master_id = a.id
and d.id = c.location_system_id
and a.is_active = 1
and b.row_id = @service_row_id
and d.row_id = @location_row_id
and effective_date <= @from_date 
and e.service_row_id = b.row_id
and e.location_row_id = d.row_id
and datediff(effective_date, @from_date) = min_date_diff";
    }
}
