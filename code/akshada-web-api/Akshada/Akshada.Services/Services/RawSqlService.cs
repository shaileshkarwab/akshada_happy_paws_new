using Akshada.DTO.Helpers;
using Akshada.EFCore.DbModels;
using Akshada.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class RawSqlService : IRawSqlService
    {
        private readonly AkshadaPawsContext akshadaPawsContext;
        public RawSqlService(AkshadaPawsContext akshadaPawsContext) { 
            this.akshadaPawsContext = akshadaPawsContext;
        }
        public List<DashServiceLocationCount> GetDashboardServiceAndLocationCount(string date)
        {
            var paramDate = new MySqlParameter("@date", DateTimeHelper.GetDate(date).ToString("yyyy-MM-dd"));
            var result = akshadaPawsContext.Set<DashServiceLocationCount>()
                .FromSqlRaw(Akshada.EFCore.RawSQL.DashboardSQL.ServiceAndLocationWiseCount, paramDate)
                .AsNoTracking()
                .AsQueryable();
            return result.ToList();
        }
    }
}
