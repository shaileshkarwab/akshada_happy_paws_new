using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface IRolesService
    {
        public List<DTO_LookUp> GetAllRoles();
    }
}
