using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_ImportData
    {
        public string Identifier { get; set; } = null!;

        public string JsonData { get; set; } = null!;

        public string OperationKey { get; set; } = null!;

    }
}
