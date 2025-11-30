using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_OtherServicesOfferedNew
    {

        public string? RowId { get; set; } = null!;

        public string ServiceOfferedDate { get; set; }


        public string FromTime { get; set; }

        public string ToTime { get; set; }

        public string? Remarks { get; set; }

        public List<DTO_OtherServicesOfferedImage> OtherServicesOfferedImages { get; set; } = new List<DTO_OtherServicesOfferedImage>();

        public DTO_LookUp ServiceOfferedUser { get; set; } = null!;

    }

    public class DTO_OtherServicesOfferedImage
    {

        public string? RowId { get; set; } = null!;
        public string? ImageName { get; set; } = null!;

        public string? UploadImageName { get; set; } = null!;

        public DTO_LookUp? ImageTypeSystem { get; set; } = null!;
    }
}
