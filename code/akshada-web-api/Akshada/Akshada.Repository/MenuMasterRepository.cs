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
    public class MenuMasterRepository: GenericRepository<MenuMaster>,IMenuMasterRepository
    {
        public MenuMasterRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration,services)
        {
        }

        public List<MenuMaster> LoggedInUserDetailMenu()
        {
            var menuResponse = akshadaPawsContext.MenuMasters.OrderBy(c=>c.SeqNo).ToList();
            return menuResponse;
        }
    }
}
