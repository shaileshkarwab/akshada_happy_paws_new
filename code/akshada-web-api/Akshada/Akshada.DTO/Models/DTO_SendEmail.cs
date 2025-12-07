using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_SendEmail
    {
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public string EmailRecipient { get; set; }
        public List<string[]> EmailCCRecipients { get; set; }
    }
}
