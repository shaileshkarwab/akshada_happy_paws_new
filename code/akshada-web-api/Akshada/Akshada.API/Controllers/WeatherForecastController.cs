using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Google.Apis.Drive.v3.Data;
using Akshada.Services.Interfaces;
using Akshada.DTO.Models;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using Akshada.Services.Services;

namespace Akshada.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISrvRequestService srvRequestService;
        private readonly GoogleMapService _mapService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITemplateService<DTO_WalkingServiceRequest> _templateService;
        private readonly ICompanyInformationService companyInformationService;
        public WeatherForecastController(GoogleMapService mapService,ICompanyInformationService companyInformationService,ILogger<WeatherForecastController> logger, ISrvRequestService srvRequestService, ITemplateService<DTO_WalkingServiceRequest> templateService)
        {
            _logger = logger;
            this.srvRequestService = srvRequestService;
            this._templateService = templateService;
            this.companyInformationService = companyInformationService;
            _mapService = mapService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("check-email")]
        public IActionResult CheckEmail()
        {
            try
            {

                var fromAddress = new MailAddress("no-reply@akshadashappypaws.com", "Shailesh Karwa");
                var toAddress = new MailAddress("shaileshkarwab@gmail.com", "Shailesh Karwa B");
                string fromPassword = "U43xh=d|h";
                string body = @"<h1>Welcome to My Webpage</h1>
    <p>This is a simple paragraph of text within the body of the HTML document.</p>
    <p>You can add more content here, such as images, links, or other elements.</p>";

                var smtp = new SmtpClient
                {
                    Host = "smtp.hostinger.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Testing from akshadapaws.com",
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    
                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(0);
        }

        [HttpGet("walking-service-on-boarding-email")]
        public async Task<IActionResult> WalkingServiceOnBoardingEmail()
        {
            var customerRowId = "27e8be06-bd99-4e69-990f-7a18f25b5f0a";
            var petRowId = "9b433d4e-5ee5-4ccd-945a-2f30c6d2b909";
            var serviceRequestRowId = "49541f74-5e9d-4917-80a2-ed07987ffd89";
            var response = this.srvRequestService.GetDetailsForWalkingService(customerRowId,petRowId,serviceRequestRowId);
            var companyInfo = await this.companyInformationService.GetCompanyInformation();
            var outPut = await this._templateService.GetFormattedTemplate(response, DTO.Enums.EmaiNotificationTemplate.PetWalkingServiceNotification, companyInfo);

            try
            {

                var fromAddress = new MailAddress("no-reply@akshadashappypaws.com", "Shailesh Karwa");
                var toAddress = new MailAddress("shaileshkarwab@gmail.com", "Shailesh Karwa B");
                string fromPassword = "U43xh=d|h";
                string body = outPut;

                var smtp = new SmtpClient
                {
                    Host = "smtp.hostinger.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Testing from akshadapaws.com",
                    Body = body,
                    IsBodyHtml = true
                })
                {

                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(outPut);
        }

        [HttpGet("upi-qr")]
        public IActionResult GenerateUpiQr(string upiId, string name, decimal amount = 0)
        {
            string upiString = $"upi://pay?pa={upiId}&pn={name}&am={amount}&cu=INR";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(upiString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrBitmap = qrCode.GetGraphic(20);

            using MemoryStream ms = new MemoryStream();
            qrBitmap.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), "image/png");    // API returns QR image
        }


        [HttpGet("thumbnail")]
        public async Task<IActionResult> GetThumbnail([FromQuery]double lat, [FromQuery] double lng)
        {
            var image = await _mapService.GenerateThumbnail(lat, lng);

            return Ok(new
            {
                ThumbnailUrl = $"{Request.Scheme}://{Request.Host}/thumbnails/{Path.GetFileName(image)}"
            });
        }
    }
}
