using System.ComponentModel.DataAnnotations;

namespace AcmeOrderSystem.Api.Contracts
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
