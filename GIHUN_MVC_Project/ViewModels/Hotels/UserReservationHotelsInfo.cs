using GIHUN_MVC_Project.Models.Hotels;

namespace GIHUN_MVC_Project.ViewModels.Hotels
{
    public class UserReservationHotelsInfo
    {
        public string? userId { get; set; } = string.Empty;
        public ReservationHotel? ReservationHotel { get; set; } = default;
        public Hotel? Hotel { get; set; } = default;
        public Paid? Paid { get; set; } = default;
    }
}
