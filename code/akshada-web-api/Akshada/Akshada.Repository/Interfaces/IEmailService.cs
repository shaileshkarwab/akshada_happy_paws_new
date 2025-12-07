using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(DTO_SendEmail email);
    }
}
