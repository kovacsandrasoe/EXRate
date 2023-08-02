using System.ComponentModel.DataAnnotations;

namespace EXRate.Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
