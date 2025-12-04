using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_EmailTemplateVariable
    {
        public DTO_Customer Customer { get; set; }
        public DTO_CustomerPet CustomerPet { get; set; }

        public DTO_CompanyInformation CompanyInformation { get; set; }
    }
}
