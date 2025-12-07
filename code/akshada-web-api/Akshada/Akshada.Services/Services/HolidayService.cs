using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public HolidayService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool DeleteHoliday(string rowId)
        {
            try
            {
                var dbHoliday = this.unitOfWork.HolidayScheduleRepository.FindFirst(c=>c.RowId == rowId);
                if(dbHoliday == null)
                {
                    throw new Exception("Failed to get the details for the selected holiday");
                }
                this.unitOfWork.HolidayScheduleRepository.Remove(dbHoliday);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public List<DTO_HolidaySchedule> GetAllHolidaysForYear(int year)
        {
            var dbHolidays = this.unitOfWork.HolidayScheduleRepository.GetAllWithInclude(x => x.HolidayDate.Year == year, includeProperties: "HolidayTypeSystem");
            var response = this.mapper.Map<List<DTO_HolidaySchedule>>(dbHolidays);
            return response;
        }

        public DTO_HolidaySchedule GetDetailByDate(string selectedDate)
        {
            var dbHoliday = this.unitOfWork.HolidayScheduleRepository.FindFirst(c => c.HolidayDate == DateTimeHelper.GetDateOnly(selectedDate), includeProperties: "HolidayTypeSystem");
            var response = this.mapper.Map<DTO_HolidaySchedule>(dbHoliday);
            return response;
        }

        public bool SaveHoliday(DTO_HolidaySchedule holidaySchedule)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbHolidaySystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == holidaySchedule.HolidayTypeSystem.RowId);
                if (dbHolidaySystem == null)
                {
                    throw new Exception("Failed to get the details for the selected holiday type");
                }
                var dbHoliday = new EFCore.DbModels.HolidaySchedule {
                    HolidayName = holidaySchedule.HolidayName,
                    HolidayDate = DateTimeHelper.GetDateOnly(holidaySchedule.HolidayDate),
                    UpdatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    HolidayTypeSystemId = dbHolidaySystem.Id,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    RowId = System.Guid.NewGuid().ToString()
                };
                
                this.unitOfWork.HolidayScheduleRepository.Add(dbHoliday);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public bool UpdateHoliday(string rowId, DTO_HolidaySchedule holidaySchedule)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbHoliday = this.unitOfWork.HolidayScheduleRepository.FindFirst(c => c.RowId == rowId);
                if (dbHoliday == null)
                {
                    throw new Exception("Failed to get the details for the selected holiday");
                }
                var dbHolidaySystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == dbHoliday.HolidayTypeSystem.RowId);
                if (dbHolidaySystem == null)
                {
                    throw new Exception("Failed to get the details for the selected holiday type");
                }
                dbHoliday.HolidayName = holidaySchedule.HolidayName;
                dbHoliday.HolidayDate = DateTimeHelper.GetDateOnly( holidaySchedule.HolidayDate);
                dbHoliday.UpdatedAt = System.DateTime.Now;
                dbHoliday.UpdatedBy = userID;
                dbHoliday.HolidayTypeSystemId = dbHolidaySystem.Id;
                this.unitOfWork.HolidayScheduleRepository.Update(dbHoliday);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }
    }
}
