using Akshada.DTO.Enums;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class TemplateService<T> : ITemplateService<T> where T:class 
    {
        public readonly string _EmailTemplateFolderPath;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        public class FormattingModel
        {
            public DTO_CompanyInformation Company { get; set; }
            public T Model;
        }
        public TemplateService(IUnitOfWork unitOfWork, IConfiguration configuration) {
            _EmailTemplateFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploaddata", "email-templates");
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public async Task<string> GetFormattedTemplate(T inputModel, EmaiNotificationTemplate notificationTemplate, DTO_CompanyInformation companyInfo)
        {
            try
            {
                //read the template
                string templatePath = string.Format("{0}\\email_template_{1}.txt", this._EmailTemplateFolderPath, (Int32)notificationTemplate);
                string template = System.IO.File.ReadAllText(templatePath);
                var companyInfos = this.unitOfWork.CompanyInfoRepository.GetAll();
                var formattingModel = new FormattingModel
                {
                    Company = companyInfo,
                    Model = inputModel
                };
                formattingModel.Company.Logo = string.Format("{0}/profile_images/{1}", this.configuration.GetSection("AppSettings:UploadPath").Value.ToString(), companyInfo.Logo);
                var formattedTemplate = Template.Parse(template);
                var res = formattedTemplate.Render(formattingModel , member => member.Name);

                return await Task.Run(() =>
                {
                    return res;
                });
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
