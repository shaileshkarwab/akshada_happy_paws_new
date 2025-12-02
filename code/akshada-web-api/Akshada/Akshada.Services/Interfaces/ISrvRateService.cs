using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface ISrvRateService
    {
        PagedList<DTO_ServiceRateList> GetServiceRateList(DTO_FilterAndPaging filterAndPaging);

        DTO_ServiceRateMaster ReteriveServiceRate(string rowID);

        bool SaveServiceRate(DTO_ServiceRateMaster saveEntity);

        bool DeleteServiceRate(string rowID);

        dynamic GetWalkingServiceRate(string serviceId, string locationId, string date);

        bool UpdateServiceRate(string rowId, DTO_ServiceRateMaster saveEntity);
    }
}
