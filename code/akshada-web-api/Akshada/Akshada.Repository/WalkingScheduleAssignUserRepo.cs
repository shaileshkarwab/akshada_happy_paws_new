using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class WalkingScheduleAssignUserRepo : GenericRepository<WalkingServiceRequestDayScheduleAssignedToUser>, IWalkingScheduleAssignUserRepo
    {
        public WalkingScheduleAssignUserRepo(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }
    }
}
