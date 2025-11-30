using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IDashBoardService
    {
        Task<DTO_DashBoardController> GetDashBoardCounts(string date);

        Task<DTO_WebRequestList> GetWebSiteSataService();

        Task<DTO_GoogleFormSubmisonList> GetGoogleFormSubmissionData();

        Task<DTO_Customer> ReteriveGoogleFormSubmissionData(string rowID);
    }
}
