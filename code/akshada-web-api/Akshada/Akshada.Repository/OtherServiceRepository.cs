using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class OtherServiceRepository : GenericRepository<OtherServicesOffered>, IOtherServiceRepository
    {
        public OtherServiceRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }

        public Task<IQueryable<DailyOtherServiceList>> GetOtherPetServices(DTO_FilterAndPaging filterAndPaging)
        {
            var fromDate = System.DateOnly.FromDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");
            var toDate = System.DateOnly.FromDateTime(System.DateTime.Now).ToString("yyyy-MM-dd");

            var fromDateDB = filterAndPaging.DateFilters.FirstOrDefault(c => c.DbColumnName == "ServiceRequiredOnDate" && !string.IsNullOrEmpty(c.FromValue));
            if (fromDateDB != null) {
                fromDate = DateTimeHelper.GetDate( fromDateDB.FromValue).ToString("yyyy-MM-dd");
            }

            var toDateDB = filterAndPaging.DateFilters.FirstOrDefault(c => c.DbColumnName == "ServiceRequiredOnDate" && !string.IsNullOrEmpty(c.ToValue));
            if (toDateDB != null)
            {
                toDate = DateTimeHelper.GetDate( toDateDB.ToValue).ToString("yyyy-MM-dd");
            }

            var paramFromDate  = new MySqlParameter("@fromDate", fromDate);
            var paramToDate  = new MySqlParameter("@toDate", toDate);

            var result = akshadaPawsContext.Set<DailyOtherServiceList>()
                .FromSqlRaw(Akshada.EFCore.RawSQL.WalkingServiceRecord.OtherServiceSQL, paramFromDate, paramToDate)
                .AsNoTracking()
                .AsQueryable();

            return Task.FromResult(result);

        }

        public async Task<OtherServiceExecutionDetail> ReteriveOtherPetServices(string otherServiceRequestId, string otherServiceAssignedToRowId)
        {
            var paramOtherServiceRequestId = new MySqlParameter("@otherServiceRequestId", otherServiceRequestId);
            var paramOtherServiceAssignedToRowId = new MySqlParameter("@otherServiceAssignedToRowId", otherServiceAssignedToRowId);
            var result = akshadaPawsContext.Set<OtherServiceExecutionDetail>()
                .FromSqlRaw(Akshada.EFCore.RawSQL.OtherServiceSQL.OhterServiceExecuteSQL, paramOtherServiceRequestId, paramOtherServiceAssignedToRowId)
                .AsNoTracking()
                .AsQueryable();

            var retriveResult = result.FirstAsync().Result;

            var imageOfferedResult =
            from a in akshadaPawsContext.SystemParameters
            where a.EnumId == 7 || a.EnumId == 8
            join bTemp in
            (
            from img in akshadaPawsContext.OtherServicesOfferedImages
            join offer in akshadaPawsContext.OtherServicesOffereds
                on img.OtherServicesOfferedMasterId equals offer.Id
            where offer.RowId == retriveResult.OtherServiceOfferRowId
            select new
            {
                img.RowId,
                img.ImageTypeSystemId,
                img.ImageName,
                img.UploadImageName
            }
            )
            on a.Id equals bTemp.ImageTypeSystemId into imageGroup
            from b in imageGroup.DefaultIfEmpty()
            select new OtherServiceExecutionDetailImage
            {
                ParamRowID = a.RowId,
                ParamValue = a.ParamValue,
                OtherServiceOfferedImageRowID = b != null ? b.RowId : null,
                ImageName = b != null ? b.ImageName : null,
                UploadedImageName = b != null ? b.UploadImageName : null
            };
            retriveResult.OtherServiceExecutionDetailImages = imageOfferedResult.ToListAsync().Result;
            return retriveResult;
        }
    }
}
