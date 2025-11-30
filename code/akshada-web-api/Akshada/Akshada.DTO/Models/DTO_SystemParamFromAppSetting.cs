using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Akshada.DTO.Models
{
    public class DTO_SystemParamFromAppSetting
    {
        public Int32 enum_id { get; set; }
        public List<DTO_SystemData> data { get; set; }
    }

    public class DTO_SystemData
    {
        public string param_value { get; set; }
        public string param_abbrivation { get; set; }
    }
}
