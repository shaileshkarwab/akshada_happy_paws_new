using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.DbModels
{
    public class OtherServiceExecutionDetail
    {
        public string OtherRequestRowId { get; set; }
        public string AssignedOtherServiceRowId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AreaLocation { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PetRowID { get; set; }
        public string PetName { get; set; }
        public string PetBreed { get; set; }
        public string PetColour { get; set; }
        public string PetOwnerImage { get; set; }
        public DateTime ChangeRequestDate { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Remarks { get; set; }
        public string ServiceSystemRowID { get; set; }
        public string ServiceName { get; set; }

        public DateTime ServiceRequestDate { get; set; }

        public string? OtherServiceOfferRowId { get; set; }
        public DateTime? OtherServiceOfferDate { get; set; }
        public string? ServiceOfferedUserId { get; set; }
        //public string? other_service_request_master_id { get; set; }
        public DateTime? ServiceOfferedFromTime { get; set; }
        public DateTime? ServiceOfferedToTime { get; set; }
        public string? ServiceOfferedRemarks { get; set; }
        public string? ServiceOfferedUserFirstName { get; set; }
        public string? ServiceOfferedUserLastName { get; set; }

        public List<OtherServiceExecutionDetailImage> OtherServiceExecutionDetailImages { get; set; }

    }

    public class OtherServiceExecutionDetailImage
    {
        public string ParamRowID { get; set; }
        public string ParamValue { get; set; }
        public string OtherServiceOfferedImageRowID { get; set; }
        public string ImageName { get; set; }

        public string UploadedImageName { get; set; }
    }
}
