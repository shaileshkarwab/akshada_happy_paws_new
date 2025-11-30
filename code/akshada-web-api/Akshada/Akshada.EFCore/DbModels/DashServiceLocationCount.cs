using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.DbModels
{
    public class DashServiceLocationCount
    {
        public string ServiceName { get; set; }
        public string AreaLocation { get; set; }
        public int TotalCount { get; set; }
    }
}
