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
    public class WebsiteServiceRepository : GenericRepository<WebsiteService>, IWebsiteServiceRepository
    {
        public WebsiteServiceRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }

        public IQueryable<WebsiteService> GetActiveWebRequest()
        {
            var response = (from a in this.akshadaPawsContext.WebsiteServices
                            join b in this.akshadaPawsContext.WebsiteServiceProcesses on a.Id equals b.WebsiteServiceMasterId into bs
                            from b in bs.DefaultIfEmpty()
                            where b == null
                            select a
                            ).AsQueryable();
            return response;
        }
    }
}
