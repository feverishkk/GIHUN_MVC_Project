using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.ViewModels.Users
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get;set; } = string.Empty;

        [Required]
        [MinLength(1), MaxLength(30)]
        public string Password { get; set; } = string.Empty;
    }
}
