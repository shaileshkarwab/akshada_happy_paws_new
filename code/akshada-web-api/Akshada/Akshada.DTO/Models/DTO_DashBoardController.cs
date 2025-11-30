using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_DashBoardController
    {
        public string Message = "Oky";

        public List<DTO_DashBoardCount> DashBoardCounts { get; set; }

        public List<DTO_DashBoardCount> AreaWiseCustomers { get; set; }

        public List<DTO_DashBoardCount> BreedWiseCount { get; set; }

        public List<DTO_DashBoardCount> ServiceCountForDate { get; set; }
        public List<DTO_DashBoardCount> LocationCountForDate { get; set; }  
    }

    public class DTO_DashBoardCount
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }

        public string BoxIcon {get;set;}
    }
}
