using Akshada.EFCore.DbModels;
using Akshada.Repository;
using Akshada.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class OtherServiceRequestRepository : GenericRepository<OtherServiceRequest>, IOtherServiceRequestRepository
    {
        public OtherServiceRequestRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }
    }
}
