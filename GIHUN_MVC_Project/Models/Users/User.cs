using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.Models.Users
{
    public class User
    {
        public string? Guid { get; set; } = string.Empty;
        [Display(Name = "닉 네임")]
        [Required]
        [MinLength(1), MaxLength(15)]
        public string? Nick_Name { get; set; } = default;

        [Display(Name = "성")]
        [Required]
        public string? Last_Name { get; set; } = default;

        [Display(Name = "이름")]
        [Required]
        public string? First_Name { get; set; } = default;

        [Display(Name = "비밀번호")]
        [Required]
        [MinLength(2), MaxLength(30)]
        public string? Password { get; set; } = default;

        [Display(Name = "이메일")]
        [Required]
        [EmailAddress]
        public string? Email { get; set; } = default;

        [Display(Name = "핸드폰 번호")]
        [Required]
        [MinLength(10), MaxLength(14)]
        public string? Mobile { get; set; } = default;

        [Display(Name = "생일")]
        [Required]
        public DateTime BirthDay { get; set; }

        public DateTime MemberSince { get; set; }

    }
}
