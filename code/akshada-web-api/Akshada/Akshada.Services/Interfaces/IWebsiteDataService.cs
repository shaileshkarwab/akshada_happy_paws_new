using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IWebsiteDataService
    {
        Task<bool> CaptureServiecRequest(DTO_ReciveFormSubmission reciveFormSubmission);

        Task<RequestData> ReteriveWebServiceData(string recordID);

        Task<bool> SaveWebServiceData(string recordID, DTO_WebsiteServiceProcess websiteServiceProcess);

        Task<bool> SaveGoogleFormSubmissionRawData(string jsonData);


    }
}
