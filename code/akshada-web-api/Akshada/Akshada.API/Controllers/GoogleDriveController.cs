using Akshada.API.AuthFilter;
using Akshada.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(BasicAuthorization))]
    public class GoogleDriveController : BaseController
    {
        private readonly GoogleDriveService _driveService;
        private readonly string _saveFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploaddata", "client-pet-images");


        public GoogleDriveController(GoogleDriveService _driveService)
        {
            this._driveService = _driveService;
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            if (!Directory.Exists(_saveFolder))
                Directory.CreateDirectory(_saveFolder);

            try
            {
                string savedPath = await _driveService.DownloadFileAsync(fileId, _saveFolder);
                return Ok(new { message = "File downloaded successfully", path = savedPath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("google-authorize")]
        public IActionResult Googleauthorize()
        {
            return Ok(true);
        }

        [HttpPost("upload-file-to-google-drive")]
        public async Task<IActionResult> UploadToDrive(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                string savedPath = await _driveService.UploadFileAsync(file);
                return Ok(new { message = "File uploaded successfully", path = savedPath });
            }
            catch(Exception ex)
            {
                throw new DTO.Models.DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
