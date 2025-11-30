using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IRawSqlService
    {
        List<DashServiceLocationCount> GetDashboardServiceAndLocationCount(string date);
    }
}
