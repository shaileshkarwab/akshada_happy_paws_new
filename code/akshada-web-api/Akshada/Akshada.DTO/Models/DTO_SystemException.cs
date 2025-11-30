using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_SystemException:Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public Exception SystemException { get; set; }
    }
}
