using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class ImportDataService : IImportDataService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private Int32 UserID;
        public ImportDataService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public bool SaveImportData(DTO_ImportData importData)
        {
            UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
            var dbImportData = this.mapper.Map<ImportDatum>(importData);
            dbImportData.RowId = System.Guid.NewGuid().ToString();
            dbImportData.CreatedAt = System.DateTime.Now;
            dbImportData.CreatedBy = UserID;
            dbImportData.UpdatedBy = UserID;
            dbImportData.UpdatedAt = System.DateTime.Now;
            this.unitOfWork.ImportDataRepository.Add(dbImportData);
            this.unitOfWork.Complete();
            return true;
        }
    }
}
