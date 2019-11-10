using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Models.Dto.User
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [RegularExpression(@"^\S+$")]
        public string Password { get; set; }
    }
}
