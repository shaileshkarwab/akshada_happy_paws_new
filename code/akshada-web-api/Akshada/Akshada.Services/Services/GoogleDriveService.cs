using Akshada.DTO.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class GoogleDriveService
    {
        private DriveService _driveService;
        private readonly GoogleServiceAccountOptions _options;
        public GoogleDriveService(CredentialFactory credentialFactory, IOptions<GoogleServiceAccountOptions> options)
        {
            var credential = credentialFactory.CreateGoogleDriveCredential();

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "DriveDownloader"
            });
            this._options = options.Value;
        }



        public async Task<string> DownloadFileAsync(string fileId, string saveFolder)
        {
            try
            {
                var fileRequest = _driveService.Files.Get(fileId);
                var file = await fileRequest.ExecuteAsync();

                var fileName = file.Name ?? $"{fileId}.bin";
                var filePath = Path.Combine(saveFolder, fileName);

                if (!Directory.Exists(saveFolder))
                    Directory.CreateDirectory(saveFolder);

                using var output = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await fileRequest.DownloadAsync(output);

                return fileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = file.FileName
                };

                if (!string.IsNullOrEmpty(this._options.UploadFolderId))
                    fileMetadata.Parents = new List<string> { this._options.UploadFolderId };

                using var stream = file.OpenReadStream();
                var request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);

                request.Fields = "id, name, webViewLink";
                var progress = await request.UploadAsync();
                if (progress.Status == UploadStatus.Completed)
                {
                    var uploaded = request.ResponseBody;
                    var permission = new Google.Apis.Drive.v3.Data.Permission
                    {
                        Type = "anyone",
                        Role = "reader"
                    };
                    await _driveService.Permissions.Create(permission, uploaded.Id).ExecuteAsync();
                    //return (true, uploaded.Id, uploaded.Name, uploaded.WebViewLink, null);
                    return uploaded.Name;
                }
                return string.Empty;

            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            return null;
        }
    }
}
