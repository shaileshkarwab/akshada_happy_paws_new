using Akshada.API.AuthFilter;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using ExcelDataReader;
using FluentMigrator.Runner.Processors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class FileUploadController : BaseController
    {
        private readonly IImportDataService importDataService;
        public FileUploadController(IImportDataService importDataService)
        {
            this.importDataService = importDataService;
        }

        [HttpPost("upload-file/{uploadFolderName}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromRoute] string uploadFolderName, [FromQuery] string? operationKey)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                var newFileName = AddFileToFolder(uploadFolderName, file, operationKey!);
                if (!string.IsNullOrEmpty(newFileName))
                {
                    return SuccessResponse(new { UploadedFileName = newFileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        void PerformActionForOperationKey(string operationKey, string fullPath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    System.Data.DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    if (dataSet.Tables.Count > 0)
                    {
                        System.Data.DataTable dataTable = dataSet.Tables[0];
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var dict = dataTable.Columns
                                .Cast<DataColumn>()
                                .ToDictionary(col => col.ColumnName, col => row[col]);

                            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
                            this.importDataService.SaveImportData(new DTO.Models.DTO_ImportData
                            {
                                Identifier = "SBK",
                                JsonData = json,
                                OperationKey = operationKey,
                            });
                            Console.WriteLine(json);
                        }
                    }
                }
            }
        }

        [HttpDelete("delete-file/folder-name/{folderName}/file-name/{fileName}")]
        public IActionResult DeleteFile([FromRoute] string folderName, [FromRoute] string fileName)
        {
            var filePath = Path.Combine("uploaddata", folderName, fileName);
            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            try
            {
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                    var response = true;
                    return SuccessResponse(response);
                }
                throw new Exception("The requested file doesnot exists");
            }
            catch (Exception ex)
            {
                throw new DTO.Models.DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }

        }

        string AddFileToFolder(string uploadFolderName, IFormFile file, string operationKey = "")
        {
            var folderName = Path.Combine("uploaddata", uploadFolderName);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                if (!System.IO.Directory.Exists(pathToSave))
                {
                    System.IO.Directory.CreateDirectory(pathToSave);
                }

                var fileInfo = new System.IO.FileInfo(file.FileName);
                var fileName = string.Format("{0}{1}", System.Guid.NewGuid().ToString(), fileInfo.Extension);
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                if (!string.IsNullOrEmpty(operationKey))
                {
                    PerformActionForOperationKey(operationKey, fullPath);
                }
                return fileName;
            }
            return string.Empty;
        }

        [HttpPost("upload-file-for-service/{uploadFolderName}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFileForService([FromRoute] string uploadFolderName)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var monthYear = string.Format("{0}_{1}", System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(System.DateTime.Now.Month), System.DateTime.Now.Year);
                var newFileName = AddFileToFolder(string.Format("{0}\\{1}", uploadFolderName, monthYear), file, string.Empty);
                if (!string.IsNullOrEmpty(newFileName))
                {
                    return SuccessResponse(new { UploadedFileName = newFileName, UploadedFileNamePath = $"{DTO_Configuration.DataUploadPath}{uploadFolderName}/{monthYear}/{newFileName}" });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
