namespace Akshada.DTO.Models
{
    public class DTO_VerifiedUser
    {
        public string Token { get; set; }
        public bool UserVerified { get; set; }

        public bool IsCompanyPresent { get; set; }
    }
}
