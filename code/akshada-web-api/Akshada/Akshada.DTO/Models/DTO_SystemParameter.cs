using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_SystemParameter
    {
        public string? RowId { get; set; }

        public int? EnumId { get; set; }

        public string? ParamValue { get; set; } = null!;

        public string? ParamAbbrivation { get; set; } = null!;

        public string? Identifier1 { get; set; }

        public string? Identifier2 { get; set; }

        public string? EnumDesc { get; set; } = null!;

        public bool? Status { get; set; }
    }
}
