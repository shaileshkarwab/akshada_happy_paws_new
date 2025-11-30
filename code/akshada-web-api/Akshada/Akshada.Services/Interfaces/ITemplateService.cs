using Akshada.DTO.Enums;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface ITemplateService<T> where T : class
    {
        Task<string> GetFormattedTemplate(T model, EmaiNotificationTemplate notificationTemplate,DTO_CompanyInformation companyInfo);
    }
}
