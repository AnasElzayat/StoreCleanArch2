using System.ComponentModel.DataAnnotations;

namespace Store.Application.Features.Account.DTOs
{
    public class UserResgisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
