using Akshada.DTO.Models;
using Akshada.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Akshada.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly DTO_SMTPSettings _smtpSettings;
        public EmailService(IOptions<DTO_SMTPSettings> smtpSettings) {
            this._smtpSettings = smtpSettings.Value;
        }

        public Task SendEmail(DTO_SendEmail email)
        {
            try
            {
                var fromAddress = new MailAddress(this._smtpSettings.FromAddress, this._smtpSettings.FromName);
                string fromPassword = this._smtpSettings.Password;
                var smtp = new SmtpClient
                {
                    Host = this._smtpSettings.Host,
                    Port = this._smtpSettings.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                };

                using (var message = new MailMessage(fromAddress, new MailAddress(email.EmailRecipient))
                {
                    Subject = email.EmailSubject,
                    Body = email.EmailBody,
                    IsBodyHtml = true
                })
                {

                    smtp.Send(message);
                }
                return Task.CompletedTask;
            }
            catch (Exception ex) { 
                return Task.FromException(ex);
            }
        }
    }
}
