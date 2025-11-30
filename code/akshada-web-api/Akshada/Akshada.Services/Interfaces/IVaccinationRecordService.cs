using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface IVaccinationRecordService
    {
        bool SaveVaccinationRecord(DTO_VaccinationRecord vaccinationRecord);

        PagedList<DTO_VaccinationSummary> GetAllVaccinationRecords(DTO_FilterAndPaging filterAndPaging);

        DTO_VaccinationRecord GetVaccinationRecordById(string rowId);

        bool DeleteVaccinationRecordById(string rowId);

        bool UpdateVaccinationRecord(string vaccinationRowId, DTO_VaccinationRecord vaccinationRecord);
    }
}
