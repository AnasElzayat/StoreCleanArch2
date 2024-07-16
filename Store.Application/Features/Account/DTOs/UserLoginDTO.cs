using System.ComponentModel.DataAnnotations;

namespace Store.Application.Features.Account.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
