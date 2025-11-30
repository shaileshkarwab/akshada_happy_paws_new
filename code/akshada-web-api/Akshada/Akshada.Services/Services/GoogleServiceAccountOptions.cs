using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class GoogleServiceAccountOptions
    {
        public string Type { get; set; }
        public string Project_Id { get; set; }
        public string Private_Key_Id { get; set; }
        public string Private_Key { get; set; }
        public string Client_Email { get; set; }
        public string Client_Id { get; set; }
        public string Auth_Uri { get; set; }
        public string Token_Uri { get; set; }
        public string Auth_Provider_X509_Cert_Url { get; set; }
        public string Client_X509_Cert_Url { get; set; }
        public string Universe_Domain { get; set; }

        public string UploadFolderId { get; set; }
    }
}
