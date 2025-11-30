using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IRawSqlService rawSqlService;
        public DashBoardService(IUnitOfWork UnitOfWork, IRawSqlService rawSqlService)
        {
            this.UnitOfWork = UnitOfWork;
            this.rawSqlService = rawSqlService;
        }
        public async Task<DTO_DashBoardController> GetDashBoardCounts(string date)
        {
            List<DTO_DashBoardCount> counts = new List<DTO_DashBoardCount>();
            var totalCustomers = this.UnitOfWork.CustomerRepository.GetAll().Count();
            var totalPets = this.UnitOfWork.CustomerPetsRepository.GetAll().Count();

            counts.Add(new DTO_DashBoardCount
            {
                Count = totalCustomers,
                Title = "Customers",
                Type = "customers",
                BoxIcon = "fa-user"
            });


            counts.Add(new DTO_DashBoardCount
            {
                Count = totalPets,
                Title = "Pets",
                Type = "pets",
                BoxIcon = "fa-dog"
            });

            var customers = this.UnitOfWork.CustomerRepository.GetAllWithInclude(includeProperties: "AreaLocationSystem").ToList();
            var coustomerLocationCout = customers.GroupBy(c => c.AreaLocationSystem?.ParamValue)
                .Select(g => new DTO_DashBoardCount
                {
                    BoxIcon = string.Empty,
                    Title = "Area Wise Customer Count",
                    Count = g.Count(),
                    Type = string.IsNullOrEmpty(g.Key) ? "NA" : g.Key
                })
                .ToList();


            var pets = this.UnitOfWork.CustomerPetsRepository.GetAllWithInclude(includeProperties: "BreedSystem").ToList();
            var petBreedCount = pets.GroupBy(c => c.BreedSystem?.ParamValue)
                .Select(g => new DTO_DashBoardCount
                {
                    BoxIcon = string.Empty,
                    Title = "Pet Breed Count",
                    Count = g.Count(),
                    Type = string.IsNullOrEmpty(g.Key) ? "NA" : g.Key
                })
                .ToList();


            //service and location wise count
            var serviceAndLocationWiseCount = this.rawSqlService.GetDashboardServiceAndLocationCount(date);

            var serviceCounts = serviceAndLocationWiseCount.GroupBy(c => c.ServiceName)
                .Select(g => new DTO_DashBoardCount
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToList();


            var locationCounts = serviceAndLocationWiseCount.GroupBy(c => c.AreaLocation)
                .Select(g => new DTO_DashBoardCount
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToList();

            var response = new DTO_DashBoardController
            {
                DashBoardCounts = counts,
                AreaWiseCustomers = coustomerLocationCout.OrderByDescending(c => c.Count).ToList(),
                BreedWiseCount = petBreedCount.OrderByDescending(c => c.Count).ToList(),
                ServiceCountForDate = serviceCounts,
                LocationCountForDate = locationCounts,
            };
            return await Task.Run(() =>
            {
                return response;
            });

        }

        public Task<DTO_GoogleFormSubmisonList> GetGoogleFormSubmissionData()
        {
            var items = this.UnitOfWork.GoogleFormSubmissionRepository.Find(c=>c.CustomerId == null && c.PetId == null);
            var totalItems = items.Count();
            var itemsForDisplay = items.ToList().Skip(0).Take(5).ToList();
            var response = new DTO_GoogleFormSubmisonList
            {
                TotalRecords = totalItems,
                RequestData = GetGoogleFormSubmissionList(itemsForDisplay)
            };
            return Task.FromResult(response);
        }

        List<DTO_GoogleFormSubmission> GetGoogleFormSubmissionList(List<GoogleFormSubmission> formList)
        {
            var response = new List<DTO_GoogleFormSubmission>();
            foreach (var m in formList)
            {
                var formObject = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO_GoogleFormSubmission>(m.JsonData);
                formObject.RowId = m.RowId;
                response.Add(formObject);
            }
            return response;
        }

        public async Task<DTO_WebRequestList> GetWebSiteSataService()
        {
            var websiteDataServiceList = this.UnitOfWork.WebsiteServiceRepository.GetActiveWebRequest();
            var totalCount = websiteDataServiceList.Count();
            var response = websiteDataServiceList.ToList();
            var requests = new List<RequestData>();
            foreach (var r in response)
            {
                var req = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestData>(r.JsonData);
                req.RowId = r.RowId;
                requests.Add(req);
            }

            return await Task.Run(() =>
            {
                return new DTO_WebRequestList
                {
                    TotalRecords = totalCount,
                    RequestData = requests.OrderByDescending(c => c.Booking_date).Skip(0).Take(4).ToList()
                };
            });
        }

        public Task<DTO_Customer> ReteriveGoogleFormSubmissionData(string rowID)
        {
            var googleFormResponse = this.UnitOfWork.GoogleFormSubmissionRepository.FindFirst(c=>c.RowId == rowID);
            var googleData = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO_GoogleFormSubmission>(googleFormResponse.JsonData);
            var dtoCustomer = new DTO_Customer
            {
                CustomerName = googleData.ClientName,
                Email = googleData.EmailId,
                Mobile = googleData.MobileNumber,
                Address = googleData.AddressLocation,
                UserName = googleData.EmailId,
                IsActive = true,
                GoogleFormSubmissionJsonData = googleFormResponse.JsonData
            };
            dtoCustomer.CustomerPets = new List<DTO_CustomerPet>();
            dtoCustomer.CustomerPets.Add(new DTO_CustomerPet { 
                IsActive = true,
                PetName = googleData.CanineName,
                PetPastIllness = googleData.PastillnesshistoryCanine
            });
            return Task.FromResult(dtoCustomer);
        }
    }
}
