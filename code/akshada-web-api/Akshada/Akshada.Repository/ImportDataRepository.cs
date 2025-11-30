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
    public class ImportDataRepository : GenericRepository<ImportDatum>, IImportDataRepository
    {
        public ImportDataRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }
    }
}
