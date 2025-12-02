using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class ServiceRequestRepository : GenericRepository<WalkingServiceRequest>, IServiceRequestRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ServiceRequestRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
            this.httpContextAccessor = services.GetService<IHttpContextAccessor>();
        }

        public List<WalkingServiceRequest> GetCustomerServiceRequests(string customerRowId, string petRowId)
        {
            try
            {
                var dbCustomer = this.akshadaPawsContext.Customers.FirstOrDefault(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the customer");
                }

                var dbPet = this.akshadaPawsContext.CustomerPets.FirstOrDefault(c => c.RowId == petRowId && c.CustomerId == dbCustomer.Id);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the customer");
                }
                var walkingServiceRequests = this.akshadaPawsContext.WalkingServiceRequests
                    .Include(c => c.ServiceSystem)
                    .Include(c => c.FrequencySystem)
                    .Where(c => c.PetId == dbPet.Id
                && c.CustomerId == dbCustomer.Id && c.IsActive == true)
                    .OrderBy(c => c.FromDate).ToList();
                return walkingServiceRequests;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public WalkingServiceRequest GetDetailsForWalkingService(string customerRowId, string petRowId, string serviceRequestRowId)
        {
            try
            {
                var dbCustomer = this.akshadaPawsContext.Customers.FirstOrDefault(c => c.RowId == customerRowId && c.IsActive == true);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the customer details");
                }

                var dbPet = this.akshadaPawsContext.CustomerPets.FirstOrDefault(c => c.RowId == petRowId && c.IsActive == true && c.IsDataComplete == true);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the customer pet details");
                }

                var dbWalkingServiceRequest = this.akshadaPawsContext.WalkingServiceRequests.FirstOrDefault(c => c.RowId == serviceRequestRowId);
                if (dbWalkingServiceRequest == null)
                {
                    throw new Exception("Failed to get the customer request details");
                }

                var allWeekDays = new[]
                {
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
                };

                var walkingServiceRequest = this.akshadaPawsContext.WalkingServiceRequests
                    .Include(c => c.FrequencySystem)
                    .Include(c => c.ServiceSystem)
                    .Include(c => c.WalkingServiceRequestDays)
                    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules)
                    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules).Include(c => c.WalkingServiceRequestDayScheduleAssignedToUsers)
                    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules).Include(c => c.WalkingServiceRequestDayScheduleAssignedToUsers).ThenInclude(c => c.User)
                    .Where(c => c.Id == dbWalkingServiceRequest.Id
                    && c.PetId == dbPet.Id
                    && c.CustomerId == dbCustomer.Id
                    ).FirstOrDefault();


                var result = from day in allWeekDays
                             join wsrDay in walkingServiceRequest.WalkingServiceRequestDays
                                 on day equals wsrDay.WeekDayName into gj
                             from sub in gj.DefaultIfEmpty()
                             select new WalkingServiceRequestDay
                             {
                                 WeekDayName = day,
                                 IsSelected = sub == null ? false : sub.IsSelected, // true if exists
                                 WalkingServiceRequestDaySchedules = sub?.WalkingServiceRequestDaySchedules,
                                 RowId = sub?.RowId,
                             };
                walkingServiceRequest.WalkingServiceRequestDays = result.ToList();
                return walkingServiceRequest;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public IQueryable<WalkingServiceRequestQuery> GetPetServiceDetails(DTO_FilterAndPaging filterAndPagings)
        {
            string fromDate = DateTimeHelper.GetDate(System.DateTime.Now.ToString("dd-MM-yyyy")).ToString("yyyy-MM-dd");
            string toDate = DateTimeHelper.GetDate(System.DateTime.Now.ToString("dd-MM-yyyy")).ToString("yyyy-MM-dd");
            var fDate = filterAndPagings.DateFilters.FirstOrDefault(c => c.DbColumnName == "FromDate");
            if (fDate != null)
            {
                fromDate = DateTimeHelper.GetDate(fDate.FromValue).ToString("yyyy-MM-dd");
            }
            var tDate = filterAndPagings.DateFilters.FirstOrDefault(c => c.DbColumnName == "ToDate");
            if (tDate != null)
            {
                toDate = DateTimeHelper.GetDate(tDate.ToValue).ToString("yyyy-MM-dd");
            }
            

            MySqlParameter paramFromDate = new MySqlParameter("@fromDate", fromDate);
            MySqlParameter paramToDate = new MySqlParameter("@toDate", toDate);

            var query = akshadaPawsContext.Set<WalkingServiceRequestQuery>()
                .FromSqlRaw(Akshada.EFCore.RawSQL.WalkingServiceRecord.WalkingServiceSQL, paramFromDate, paramToDate)
                .AsNoTracking()
                .AsQueryable();

            return query;

            //var selectedDays = searchCriteria.selectedWeekDays.Where(c => c.selected == true).Select(c => c.displayName).ToList();
            //var selectedServices = this.akshadaPawsContext.WalkingServiceRequests
            //    .Include(c => c.WalkingServiceRequestDays.Where(d => d.IsSelected == true && selectedDays.Contains(d.WeekDayName)))
            //    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules)
            //    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules).ThenInclude(c=>c.WalkingServiceRequestDayScheduleAssignedToUsers)
            //    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules).ThenInclude(c => c.WalkingServiceRequestDayScheduleAssignedToUsers).ThenInclude(c=>c.User)
            //    .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules).ThenInclude(c=>c.NewUserAssignToWalkingServices)
            //    .Include(c => c.FrequencySystem)
            //    .Include(c => c.ServiceSystem)
            //    .Include(c => c.Customer)
            //    .Include(c => c.Customer).ThenInclude(c => c.AreaLocationSystem)
            //    .Include(c => c.Pet)
            //    .Include(c => c.Pet).ThenInclude(c => c.BreedSystem)
            //    .Include(c => c.Pet).ThenInclude(c => c.Colour)
            //    .Where(c => c.FromDate <= toDate && c.ToDate >= fromDate)
            //    .ToList();
            //return selectedServices;
        }

        public IQueryable<WalkingServiceRequest> GetWalkingServiceRequests()
        {
            var selectedServices = this.akshadaPawsContext.WalkingServiceRequests
                .Include(c => c.WalkingServiceRequestDays.Where(d => d.IsSelected == true))
                .Include(c => c.WalkingServiceRequestDays).ThenInclude(c => c.WalkingServiceRequestDaySchedules)
                .Include(c => c.FrequencySystem)
                .Include(c => c.ServiceSystem)
                .Include(c => c.Customer)
                .Include(c => c.Customer).ThenInclude(c => c.AreaLocationSystem)
                .Include(c => c.Pet)
                .Include(c => c.Pet).ThenInclude(c => c.BreedSystem)
                .Include(c => c.Pet).ThenInclude(c => c.Colour)
                .OrderBy(c => c.FromDate).ThenBy(c => c.Customer.CustomerName)
                .AsQueryable();
            return selectedServices;

        }

        public bool UpdateCustomerPetWalkingServiceRequest(string customerRowId, string petRowId, string serviceRequestId, DTO_WalkingServiceRequest updateEntity)
        {
            try
            {
                var UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);

                // check for customer 
                var dbCustomer = this.akshadaPawsContext.Customers.FirstOrDefault(c => c.RowId == customerRowId && c.IsActive == true);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the selected customer");
                }

                // check for customer pet
                var dbPet = this.akshadaPawsContext.CustomerPets.FirstOrDefault(c => c.RowId == petRowId && c.CustomerId == dbCustomer.Id && c.IsActive == true && c.IsDataComplete == true);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the details for the selected customer pet");
                }

                //check for service request
                var dbServiceRequest = this.akshadaPawsContext.WalkingServiceRequests.FirstOrDefault(
                    c => c.RowId == serviceRequestId
                    && c.IsActive == true
                    && c.CustomerId == dbCustomer.Id
                    && c.PetId == dbPet.Id
                    );
                if (dbServiceRequest == null)
                {
                    throw new Exception("Failed to get the details for the service request");
                }

                dbServiceRequest.UpdatedAt = System.DateTime.Now;
                dbServiceRequest.UpdatedBy = UserID;
                dbServiceRequest.ServiceSystemId = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == updateEntity.ServiceSystem.RowId).Id;
                dbServiceRequest.FrequencySystemId = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == updateEntity.FrequencySystem.RowId).Id;
                dbServiceRequest.FromDate = DateTimeHelper.GetDate(updateEntity.FromDate);
                dbServiceRequest.ToDate = DateTimeHelper.GetDate(updateEntity.ToDate);
                dbServiceRequest.IsActive = updateEntity.IsActive;
                dbServiceRequest.RegularDayRate = updateEntity.RegularDayRate;
                dbServiceRequest.SpecialDayRate = updateEntity.SpecialDayRate;
                dbServiceRequest.IsChargedMonthly = updateEntity.IsChargedMonthly;

                foreach (var walkingServiceRequestDay in updateEntity.WalkingServiceRequestDays)
                {
                    var walkingDays = new WalkingServiceRequestDay();
                    

                    if (walkingServiceRequestDay.RowId == null)
                    {
                        walkingDays = new WalkingServiceRequestDay
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            IsSelected = walkingServiceRequestDay.IsSelected.Value,
                            WeekDayName = walkingServiceRequestDay.WeekDayName
                        };
                        dbServiceRequest.WalkingServiceRequestDays.Add(walkingDays);
                    }
                    else
                    {
                        var dbWalkingDay = this.akshadaPawsContext.WalkingServiceRequestDays.FirstOrDefault(
                            c => c.RowId == walkingServiceRequestDay.RowId);

                        dbWalkingDay.IsSelected = walkingServiceRequestDay.IsSelected.Value;
                        dbWalkingDay.WeekDayName = walkingServiceRequestDay.WeekDayName;
                        this.akshadaPawsContext.Entry(dbWalkingDay).CurrentValues.SetValues(dbWalkingDay);
                        this.akshadaPawsContext.Entry(dbWalkingDay).State = EntityState.Modified;
                        walkingDays = dbWalkingDay;
                    }

                    // checking for the schedule
                    foreach (var daySchedule in walkingServiceRequestDay.WalkingServiceRequestDaySchedules)
                    {
                        if (daySchedule.RowId == null)
                        {
                            walkingDays.WalkingServiceRequestDaySchedules.Add(new WalkingServiceRequestDaySchedule
                            {
                                RowId = System.Guid.NewGuid().ToString(),
                                FromTime = Convert.ToDateTime(daySchedule.FromTime),
                                ToTime = Convert.ToDateTime(daySchedule.ToTime)
                            });
                            
                        }
                        else
                        {
                            var dbDaySchedule = this.akshadaPawsContext.WalkingServiceRequestDaySchedules.FirstOrDefault(c => c.RowId == daySchedule.RowId);
                            dbDaySchedule.FromTime = Convert.ToDateTime(daySchedule.FromTime);
                            dbDaySchedule.ToTime = Convert.ToDateTime(daySchedule.ToTime);
                            this.akshadaPawsContext.Entry(dbDaySchedule).CurrentValues.SetValues(dbDaySchedule);
                            this.akshadaPawsContext.Entry(dbDaySchedule).State = EntityState.Modified;
                        }
                    }
                }

                this.akshadaPawsContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
