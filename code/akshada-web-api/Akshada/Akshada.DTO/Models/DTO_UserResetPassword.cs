using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_UserResetPassword: DTO_PasswordBase
    {
        public string NewPassword { get; set; }

    }

    public class DTO_UpdateUserPin
    {
        public string? ExistingPin { get; set; }
        public string NewPin { get; set; }
    }


    public class DTO_PasswordBase
    {
        
        public string? ExistingPassWord { get; set; }
    }
}
