using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_ReciveFormSubmission
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string JsonContent { get; set; }

        public string? RowId { get; set; }
    }

    public class RequestData
    {
        public string Service { get; set; }
        public string Your_name { get; set; }
        public string Phone_number { get; set; }
        public string Booking_date { get; set; }
        public string Booking_time { get; set; }
        public string Email_ID { get; set; }
        public string Message { get; set; }

        [JsonProperty("FormSubmit Team")]
        public string FormSubmitTeam { get; set; }

        public string? RowId { get; set; }
    }

    public class DTO_WebRequestList
    {
        public int TotalRecords { get; set; }
        public List<RequestData> RequestData { get; set; }
    }

    public class DTO_GoogleFormSubmisonList
    {
        public int TotalRecords { get; set; }
        public List<DTO_GoogleFormSubmission> RequestData { get; set; }
    }
}
