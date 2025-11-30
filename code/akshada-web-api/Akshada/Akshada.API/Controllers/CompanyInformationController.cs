using Akshada.API.AuthFilter;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class CompanyInformationController : BaseController
    {
        private ICompanyInformationService companyInformationService;
        public CompanyInformationController(ICompanyInformationService companyInformationService) { 
            this.companyInformationService = companyInformationService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCompanyInformation([FromBody] DTO_CompanyInformation companyInformation)
        {
            var response = await this.companyInformationService.SaveCompanyInformation(companyInformation);
            return await Task.Run(() =>
            {
                return SuccessResponse(response);
            });
        }

        [HttpPut("company/{companyRowId}")]
        public async Task<IActionResult> UpdateCompanyInformation([FromRoute] string companyRowId, [FromBody] DTO_CompanyInformation companyInformation)
        {
            var response = await this.companyInformationService.UpdateCompanyInformation(companyRowId, companyInformation);
            return await Task.Run(() =>
            {
                return SuccessResponse(response);
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetCompanyInformation()
        {
            var response = await this.companyInformationService.GetCompanyInformation();
            return await Task.Run(() =>
            {
                return SuccessResponse(response);
            });
        }
    }
}
