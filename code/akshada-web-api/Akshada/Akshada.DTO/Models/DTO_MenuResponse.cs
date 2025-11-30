using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_MenuResponse
    {
        public string MenuText { get; set; } = null!;
        public string? Controller { get; set; }

        public string? Page { get; set; }

        public string FaIcon { get; set; } = null!;

        public List<DTO_MenuResponse> ChildMenus { get; set; }
    }
}
