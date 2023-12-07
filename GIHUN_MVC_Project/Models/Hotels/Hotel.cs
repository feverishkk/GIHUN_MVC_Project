using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.Models.Hotels
{
    public class Hotel
    {
        public int Hotel_Id { get; set; } = default;

        [Display(Name ="호텔 이름")]
        [Required]
        public string? Hotel_Name { get; set; } = string.Empty;

        [Display(Name = "호텔 서비스 평점")]
        [Required]
        public double? Hotel_Service_Rating { get; set; } 

        [Display(Name = "나라")]
        [Required]
        public string? Hotel_Country { get; set; } = string.Empty;

        [Display(Name = "호텔 지역 평점")]
        [Required]
        public double? Hotel_Location_Rating { get; set; }

        [Display(Name = "위치")]
        [Required]
        public string? Hotel_Location { get; set; } = string.Empty;

        [Display(Name = "호텔 주소")]
        [Required]
        public string? Hotel_Address { get;set; } = string.Empty;

        [Display(Name = "우편번호")]
        [Required]
        public string? Hotel_PostCode { get; set; } = string.Empty;  

    }
}
