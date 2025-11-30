using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class VaccinationRecordRepository : GenericRepository<VaccinationRecord>, IVaccinationRecordRepository
    {
        public VaccinationRecordRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }

        public IQueryable<VaccinationSummary> GetAllVaccinationRecords()
        {
            const string sql = @"SELECT a.row_id as RowId, a.record_entry_date as RecordEntryDate, a.vaccination_date as VaccinatedDate, c.customer_name as CustomerName, d.pet_name as PetName, e.contact_name as ContactName,
            e.mobile as Mobile, min(due_date) as DueDate
            FROM vaccination_record a
            left outer join important_contact e on a.vet_or_clinic_name_imp_contact_id = e.id
            , vaccination_record_detail b, customer c, customer_pets d
            where a.id = b.vaccination_record_master_id and c.id = a.customer_id and d.customer_id = c.id and a.pet_id = d.id
            group by a.record_entry_date, a.vaccination_date, c.customer_name, d.pet_name, e.contact_name, e.mobile,
            a.row_id order by min(due_date) desc";

            return akshadaPawsContext.Set<VaccinationSummary>()
                .FromSqlRaw(sql)
                .AsNoTracking()
                .AsQueryable();
        }
    }
}
