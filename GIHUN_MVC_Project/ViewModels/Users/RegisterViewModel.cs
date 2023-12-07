using GIHUN_MVC_Project.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.ViewModels.Users
{
    public class RegisterViewModel : User
    {
        [Display(Name = "비밀번호 확인")]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
