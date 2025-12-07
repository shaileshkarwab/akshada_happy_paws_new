using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace Akshada.Services.Services
{
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public VaccinationRecordService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public bool DeleteVaccinationRecordById(string rowId)
        {
            var vaccinationRecord = this.unitOfWork.VaccinationRecordRepository.FindFirst(c => c.RowId == rowId);
            if (vaccinationRecord == null)
            {
                throw new Exception("Failed to get the vaccination record");
            }
            this.unitOfWork.VaccinationRecordRepository.RemoveIncludingChildren(includes: "VaccinationRecordDetails", c => c.RowId == rowId);
            this.unitOfWork.Complete();
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploaddata", "client-pet-images", vaccinationRecord.VaccinationProofImage);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public PagedList<DTO_VaccinationSummary> GetAllVaccinationRecords(DTO_FilterAndPaging filterAndPaging)
        {
            var queryResponse = this.unitOfWork.VaccinationRecordRepository.GetAllVaccinationRecords();
            var pagedResponse = PagedList<VaccinationSummary>.ToPagedList(queryResponse, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var dtoSummary = this.mapper.Map<List<DTO_VaccinationSummary>>(pagedResponse);

            return new PagedList<DTO_VaccinationSummary>(
                    dtoSummary,
                    pagedResponse.TotalCount,
                    pagedResponse.CurrentPage,
                    pagedResponse.PageSize
                );
        }

        public DTO_VaccinationRecord GetVaccinationRecordById(string rowId)
        {
            try
            {
                var vaccinationRecord = this.unitOfWork.VaccinationRecordRepository.FindFirst(c => c.RowId == rowId, includeProperties: "Customer,Pet,Customer.CustomerPets,VaccinationRecordDetails,VaccinationRecordDetails.VaccinationSystem, VetOrClinicNameImpContact");
                if (vaccinationRecord == null)
                {
                    throw new Exception("Failed to get the details for the selected vaccination record");
                }
                var response = this.mapper.Map<DTO_VaccinationRecord>(vaccinationRecord);
                return response;
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

        public bool SaveVaccinationRecord(DTO_VaccinationRecord vaccinationRecord)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == vaccinationRecord.Customer.RowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the customer details");
                }

                var dbPet = this.unitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == vaccinationRecord.Pet.RowId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the customer pet details");
                }

                var dbVet = this.unitOfWork.ImportantContactRepo.FindFirst(c => c.RowId == vaccinationRecord.VetOrClinicNameImpContact.RowId);


                var dbVaccinationRecord = new VaccinationRecord
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    RecordEntryDate = DateTimeHelper.GetDateOnly(vaccinationRecord.RecordEntryDate),
                    CustomerId = dbCustomer.Id,
                    PetId = dbPet.Id,
                    VetOrClinicName = string.Empty,
                    VetOrClinicContactNumber = vaccinationRecord.VetOrClinicContactNumber,
                    Remarks = vaccinationRecord.Remarks,
                    VetOrClinicNameImpContactId = dbVet.Id,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    VaccinationProofImage = vaccinationRecord.VaccinationProofImage
                };

                foreach (var vcc in vaccinationRecord.VaccinationRecordDetails)
                {
                    //add vaccination record
                    if (string.IsNullOrEmpty(vcc.RowId) && vcc.Selected)
                    {
                        var dbVccSystemParam = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == vcc.VaccinationSystem.RowId);

                        if (dbVccSystemParam == null)
                        {
                            throw new Exception("Failed to get the vaccination name from the system");
                        }

                        dbVaccinationRecord.VaccinationRecordDetails.Add(new VaccinationRecordDetail
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            VaccinationSystemId = dbVccSystemParam.Id,
                            VaccinatedDate = DateTimeHelper.GetDateOnly(vcc.VaccinatedDate),
                            DueDate = DateTimeHelper.GetDateOnly(vcc.DueDate)
                        });
                    }
                }
                this.unitOfWork.VaccinationRecordRepository.Add(dbVaccinationRecord);
                this.unitOfWork.Complete();
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

        public bool UpdateVaccinationRecord(string vaccinationRowId, DTO_VaccinationRecord vaccinationRecord)
        {
            try
            {
                var vaccinationRecordDB = this.unitOfWork.VaccinationRecordRepository.FindFirst(c => c.RowId == vaccinationRowId);
                if (vaccinationRecordDB == null)
                {
                    throw new Exception("Failed to get the details for the vaccination record");
                }
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                vaccinationRecordDB.RecordEntryDate = DateTimeHelper.GetDateOnly(vaccinationRecord.RecordEntryDate);
                vaccinationRecordDB.CustomerId = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == vaccinationRecord.Customer.RowId).Id;
                vaccinationRecordDB.PetId = this.unitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == vaccinationRecord.Pet.RowId).Id;
                vaccinationRecordDB.VetOrClinicName = vaccinationRecord.VetOrClinicName;
                vaccinationRecordDB.VetOrClinicContactNumber = vaccinationRecord.VetOrClinicContactNumber;
                vaccinationRecordDB.Remarks = vaccinationRecord.Remarks;
                vaccinationRecordDB.VaccinationProofImage = vaccinationRecord.VaccinationProofImage;
                vaccinationRecordDB.VetOrClinicNameImpContactId = this.unitOfWork.ImportantContactRepo.FindFirst(c => c.RowId == vaccinationRecord.VetOrClinicNameImpContact.RowId)?.Id;
                vaccinationRecordDB.UpdatedAt = System.DateTime.Now;
                vaccinationRecordDB.UpdatedBy = userID;


                foreach (var m in vaccinationRecord.VaccinationRecordDetails)
                {
                    var dbVaccinationSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == m.VaccinationSystem.RowId);
                    //adding a record
                    if (string.IsNullOrEmpty(m.RowId) && m.Selected)
                    {

                        var newVaccinationRecord = new VaccinationRecordDetail
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            VaccinationSystemId = dbVaccinationSystem.Id,
                            VaccinatedDate = DateTimeHelper.GetDateOnly(m.VaccinatedDate),
                            DueDate = DateTimeHelper.GetDateOnly(m.DueDate)
                        };
                        vaccinationRecordDB.VaccinationRecordDetails.Add(newVaccinationRecord);
                    }
                    else if (m.Selected && !string.IsNullOrEmpty(m.RowId))
                    {
                        var vaccinationDetail = this.unitOfWork.VaccinationRecordDetailRepository.FindFirst(c => c.RowId == m.RowId);
                        vaccinationDetail.VaccinationSystemId = dbVaccinationSystem.Id;
                        vaccinationDetail.VaccinatedDate = DateTimeHelper.GetDateOnly(m.VaccinatedDate);
                        vaccinationDetail.DueDate = DateTimeHelper.GetDateOnly(m.DueDate);
                        this.unitOfWork.VaccinationRecordDetailRepository.Update(vaccinationDetail);
                    }
                }
                this.unitOfWork.VaccinationRecordRepository.Update(vaccinationRecordDB);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    Message = ex.Message,
                    SystemException = ex,
                    StatusCode = (Int32)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
