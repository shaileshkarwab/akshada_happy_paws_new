using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_FilterAndPaging
    {
        public List<Filter>? EqualityFilters { get; set; }
        public List<Filter>? Criteria { get; set; }

        public List<DateFilter>? DateFilters { get; set; }

        public List<RangeFilter>? RangeFilters{ get; set; }

        public List<BooleanFilter>? BooleanFilters { get; set; }

        public PageParameter? PageParameter { get; set; }

        public List<Filter>? NotEqualityFilters { get; set; }
    }

    public class BaseFilter
    {
        public string EntityName { get; set; }

        public string DbColumnName { get; set; }
    }

    public class Filter:BaseFilter
    {
        public string? Value { get; set; }
    }

    public class BooleanFilter : BaseFilter
    {
        public bool? Value { get; set; }
    }


    public class RangeFilter : BaseFilter
    {
        public double? Value { get; set; }

    }

    public class DateFilter : BaseFilter
    {
        public String? FromValue { get; set; }
        public string? ToValue { get; set; }

    }

}
