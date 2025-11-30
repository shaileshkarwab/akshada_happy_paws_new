using Akshada.EFCore.DbModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public class GoogleFormSubmissionRepository : GenericRepository<GoogleFormSubmission>
    {
        public GoogleFormSubmissionRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }
    }
}
