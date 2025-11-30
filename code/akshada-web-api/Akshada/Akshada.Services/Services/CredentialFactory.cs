using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class CredentialFactory
    {
        private readonly GoogleServiceAccountOptions _options;

        public CredentialFactory(IOptions<GoogleServiceAccountOptions> options)
        {
            _options = options.Value;
        }

        public GoogleCredential CreateGoogleDriveCredential()
        {

            var privateKey = _options.Private_Key
            .Replace("\\n", "\n")
            .Trim();


            // Build ServiceAccountCredential from options
            var initializer = new ServiceAccountCredential.Initializer(_options.Client_Email)
            {
                ProjectId = _options.Project_Id,
                KeyId = _options.Private_Key_Id
            }.FromPrivateKey(privateKey);

            var serviceAccount = new ServiceAccountCredential(initializer);

            // Convert to GoogleCredential
            return serviceAccount
                .ToGoogleCredential()
                .CreateScoped(Google.Apis.Drive.v3.DriveService.ScopeConstants.Drive);
        }
    }
}
