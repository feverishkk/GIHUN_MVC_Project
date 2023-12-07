using GIHUN_MVC_Project.Models.Hotels;
using GIHUN_MVC_Project.Models.Users;

namespace GIHUN_MVC_Project.ViewModels.Hotels
{
    public class ReservationHotelViewModel
    {
        public UserInfo? UserInfo { get; set; }  
        public ReservationHotel? ReservationHotel { get; set; }
        public Hotel? Hotel { get; set; }
        public Paid? Paid { get; set; }

        public ReservationHotelViewModel(UserInfo userInfo, ReservationHotel reservationHotel, Hotel hotel, Paid paid)
        {
            UserInfo = userInfo;
            ReservationHotel = reservationHotel;
            Hotel = hotel;
            Paid = paid;
        }

        public ReservationHotelViewModel()
        {
        }

    }
}
