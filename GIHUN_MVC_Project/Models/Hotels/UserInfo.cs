using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.Models.Hotels
{
    public class UserInfo
    {
        public string? Guid { get; set; } = string.Empty;

        [Display(Name = "성")]
        [Required]
        public string? Last_Name { get; set; } = default;

        [Display(Name = "이름")]
        [Required]
        public string? First_Name { get; set; } = default;

        [Display(Name = "이메일")]
        [Required]
        [EmailAddress]
        public string? Email { get; set; } = default;

        [Display(Name = "핸드폰 번호")]
        [Required]
        [MinLength(10), MaxLength(14)]
        public string? Mobile { get; set; } = default;
    }
}
