using GIHUN_MVC_Project.Models.Hotels;
using GIHUN_MVC_Project.Models.Users;
using System.ComponentModel;

namespace GIHUN_MVC_Project.ViewModels.Hotels
{
    public class HotelsInfoViewModel
    {
        public int? hotel_id { get; set; }
        public int? chain_id { get; set; }
        [DisplayName("체인 이름")]
        public string? chain_name { get; set; } = string.Empty;  
        public int? brand_id { get; set; }
        [DisplayName("브랜드 이름")]
        public string? brand_name { get; set; } = string.Empty;
        [DisplayName("호텔 이름")]
        public string? hotel_name { get; set; } = string.Empty;
        [DisplayName("호텔 풀 네임")]
        public string? hotel_formerly_name { get; set; } = string.Empty;
        [DisplayName("주소 1")]
        public string? addressline1 { get; set; } = string.Empty;
        [DisplayName("주소 2")]
        public string? addressline2 { get; set; } = string.Empty;
        [DisplayName("우편번호")]
        public string? zipcode { get; set; } = string.Empty;
        [DisplayName("도시")]
        public string? city { get; set; } = string.Empty;
        [DisplayName("스테이트(state)")]
        public string? state { get; set; } = string.Empty;
        [DisplayName("나라")]
        public string? country { get; set; } = string.Empty;
        public string? countryisocode { get; set; } = string.Empty;
        [DisplayName("평점")]
        public double? star_rating { get; set; }
        public double? longitude { get; set; }
        public double? latitude { get; set; }
        [DisplayName("주소")]
        public string? url { get; set; } = string.Empty;
        [DisplayName("체크인 가능한 날짜")]
        public string? checkin { get; set; } = string.Empty;
        [DisplayName("체크아웃 해야하는 날짜")]
        public string? checkout { get; set; } = string.Empty;
        [DisplayName("총 방 개수")]
        public int? numberrooms { get; set; }
        [DisplayName("총 층수")]
        public int? numberfloors { get; set; }
        [DisplayName("오픈한 년도")]
        public int? yearopened { get; set; }
        [DisplayName("보수 한 년도")]
        public int? yearrenovated { get; set; }
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
        [DisplayName("개요")]
        public string? overview { get; set; } = string.Empty;
        public int? rates_from { get; set; }
        public int? continent_id { get; set; }
        [DisplayName("대륙 이름")]
        public string? continent_name { get; set; } = string.Empty;
        public int? city_id { get; set; }
        public int? country_id { get; set; }
        [DisplayName("리뷰 개수")]
        public int? number_of_reviews { get; set; }
        [DisplayName("순위 평균")]
        public double? rating_average { get; set; }
        [DisplayName("통화")]
        public string? rates_currency { get; set; } = string.Empty;

        public ReservationHotelViewModel? ReservationHotel { get; set; }

    }


}
