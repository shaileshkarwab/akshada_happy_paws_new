using Akshada.API.AuthFilter;
using Akshada.API.Extensions;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class VaccinationRecordController : BaseController
    {
        private readonly IVaccinationRecordService vaccinationRecordService;
        public VaccinationRecordController(IVaccinationRecordService vaccinationRecordService) {
            this.vaccinationRecordService = vaccinationRecordService;
        }

        [HttpPost]
        public IActionResult SaveVaccinationRecord([FromBody]DTO_VaccinationRecord vaccinationRecord) {
            var response = this.vaccinationRecordService.SaveVaccinationRecord(vaccinationRecord);
            return SuccessResponse(response);
        }


        [HttpPost("get-all")]
        public IActionResult GetAllVaccinationRecords([FromBody] DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.vaccinationRecordService.GetAllVaccinationRecords(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpGet("get-by-id/recordID/{rowId}")]
        public IActionResult GetVaccinationRecordById([FromRoute]string rowId)
        {
            var response = this.vaccinationRecordService.GetVaccinationRecordById(rowId);

            // check if the vaccination proof image exists
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploaddata", "client-pet-images",response.VaccinationProofImage);
            if(!System.IO.File.Exists(filePath))
            {
                response.VaccinationProofImage = string.Empty;
                response.IsFileDeleted = true;
            }
            return SuccessResponse(response);
        }

        [HttpDelete("{rowId}")]
        public IActionResult DeleteVaccinationRecordById([FromRoute] string rowId)
        {
            var response = this.vaccinationRecordService.DeleteVaccinationRecordById(rowId);

            // check if the vaccination proof image exists
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploaddata", "client-pet-images", response.VaccinationProofImage);
            return SuccessResponse(response);
        }


        [HttpPut("vaccinationRowId/{vaccinationRowId}")]
        public IActionResult UpdateVaccinationRecord([FromRoute]string vaccinationRowId,  [FromBody] DTO_VaccinationRecord vaccinationRecord)
        {
            var response = this.vaccinationRecordService.UpdateVaccinationRecord(vaccinationRowId,vaccinationRecord);
            return SuccessResponse(response);
        }
    }
}
