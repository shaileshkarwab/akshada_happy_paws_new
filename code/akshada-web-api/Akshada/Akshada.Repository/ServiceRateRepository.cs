using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class ServiceRateRepository : GenericRepository<ServiceRateMaster>, IServiceRateRepository
    {
        public ServiceRateRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration,services)
        {
        }

        public List<DTO_ServiceRateList> GetServiceRateList()
        {
            var serviceRates = (from srm in this.akshadaPawsContext.ServiceRateMasters.Include(s => s.ServiceSystem)
                                select new DTO_ServiceRateList
                                {
                                    RowId = srm.RowId,
                                    EffectiveDate = srm.EffectiveDate,
                                    EntryDate = srm.EntryDate,
                                    IsActive = srm.IsActive,
                                    ServiceName = srm.ServiceSystem.ParamValue
                                }
                                ).OrderBy(c => c.ServiceName).ThenBy(c => c.EffectiveDate).ToList();
            return serviceRates;
        }

        public WalkingServiceRate GetWalkingServiceRate(string serviceId, string locationId, string date)
        {
            var paramServiceId = new MySqlParameter("@service_row_id", serviceId);
            var paramLocationId = new MySqlParameter("@location_row_id",locationId);
            var paramDate = new MySqlParameter("@from_date", DateTimeHelper.GetDate( date).ToString("yyyy-MM-dd"));
        
            var result = akshadaPawsContext.Set<WalkingServiceRate>()
                .FromSqlRaw(Akshada.EFCore.RawSQL.WalkingRateSQL.SQLQuery, paramServiceId, paramLocationId, paramDate)
                .AsNoTracking()
                .FirstOrDefault();
            if(result == null)
            {
                result = new WalkingServiceRate { 
                    RegularRate = 0,
                    SpecialDayRate = 0
                };
            }
            return result;
        }

        public ServiceRateMaster ReteriveServiceRate(string rowID)
        {
            var serviceRateMaster = this.akshadaPawsContext.ServiceRateMasters
                .Include(d=>d.ServiceSystem)
                .Include(d=>d.ServiceRateMasterDetails)
                .Include(d=>d.ServiceRateMasterDetails).ThenInclude(d=>d.LocationSystem)
                .FirstOrDefault(c => c.RowId == rowID);

            return serviceRateMaster;
        }
    }
}
