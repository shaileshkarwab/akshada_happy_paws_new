using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_User
    {

        public string? RowId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public string? LoginName { get; set; }

        public bool? Status { get; set; }

        public string? ImagePath { get; set; }

        public DTO_LookUp Role { get; set; } = null!;

        public string? Password { get; set; }

    }
}
