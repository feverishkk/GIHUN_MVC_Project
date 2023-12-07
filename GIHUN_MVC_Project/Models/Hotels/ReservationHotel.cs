using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.Models.Hotels
{
    public class ReservationHotel
    {
        public string? Booking_Id { get; set; } = string.Empty;

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
        public int? Children { get;set; } = default;

        [DisplayName("애기")]
        [Required]
        public int? Infants { get; set; } = default;

        [DisplayName("애완 동물")]
        [Required]
        public int? Pets { get; set; } = default;

    }
}
