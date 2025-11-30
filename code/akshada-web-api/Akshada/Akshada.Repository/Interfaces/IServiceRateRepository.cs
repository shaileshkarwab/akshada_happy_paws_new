using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IServiceRateRepository:IGenericRepository<ServiceRateMaster>
    {
        List<DTO_ServiceRateList> GetServiceRateList();

        ServiceRateMaster ReteriveServiceRate(string rowID);

        WalkingServiceRate GetWalkingServiceRate(string serviceId, string locationId, string date);
    }
}
