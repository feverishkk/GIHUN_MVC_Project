using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.ViewModels.Hotels
{
    public class UpdateReservationHotelViewModel
    {
        [DisplayName("호텔 이름")]
        public string? Hotel_Name { get; set; } = string.Empty;

        [DisplayName("주소 1")]
        public string? Addressline1 { get; set; } = string.Empty;

        [DisplayName("나라")]
        public string? Country { get; set; } = string.Empty;

        [DisplayName("도시")]
        public string? City { get; set; } = string.Empty;

        [DisplayName("우편번호")]
        public string? Zipcode { get; set; } = string.Empty;

        [Display(Name = "호텔 서비스 평점")]
        [Required]
        public double? Hotel_Service_Rating { get; set; }

        [Display(Name = "호텔 지역 평점")]
        [Required]
        public double? Hotel_Location_Rating { get; set; }

        [DisplayName("예약번호")]
        public string Booking_Id { get; set; } = string.Empty;

        [DisplayName("숙박 일 수")]
        [Required]
        public int? StayDays { get; set; } = default;

        [DisplayName("체크 인")]
        [Required]
        public DateTime? Check_In { get; set; } = default;

        [DisplayName("체크 아웃")]
        [Required]
        public DateTime? Check_Out { get; set; } = default;

        [DisplayName("성인")]
        [Required]
        public int? Adults { get; set; } = default;

        [DisplayName("아이")]
        [Required]
        public int? Children { get; set; } = default;

        [DisplayName("애기")]
        [Required]
        public int? Infants { get; set; } = default;

        [DisplayName("애완 동물")]
        [Required]
        public int? Pets { get; set; } = default;

        [Display(Name = "총 액수")]
        public int? Paid_Total { get; set; } = 1;

        [Display(Name = "통화")]
        public string? Paid_Currency { get; set; } = "USD";

        [Display(Name = "결제한 날")]
        public DateTime Paid_Date { get; set; }

        [DisplayName("사진 1")]
        public string? photo1 { get; set; } = string.Empty;
        [DisplayName("사진 2")]
        public string? photo2 { get; set; } = string.Empty;
        [DisplayName("사진 3")]
        public string? photo3 { get; set; } = string.Empty;
        [DisplayName("사진 4")]
        public string? photo4 { get; set; } = string.Empty;
        [DisplayName("사진 5")]
        public string? photo5 { get; set; } = string.Empty;

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
