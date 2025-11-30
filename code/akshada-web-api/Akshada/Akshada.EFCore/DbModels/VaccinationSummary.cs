using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.DbModels
{
    public class VaccinationSummary
    {
        public Guid RowId { get; set; }
        public DateTime? RecordEntryDate { get; set; }
        public DateTime? VaccinatedDate { get; set; }
        public string CustomerName { get; set; }
        public string PetName { get; set; }
        public string ContactName { get; set; }
        public string Mobile { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
