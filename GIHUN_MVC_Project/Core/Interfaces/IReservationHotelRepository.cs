using GIHUN_MVC_Project.ViewModels.Hotels;
using System.Data.SqlClient;

namespace GIHUN_MVC_Project.Core.Interfaces
{
    public interface IReservationHotelRepository
    {
        List<HotelsInfoViewModel> GetAll();

        HotelsInfoViewModel? GetByHotelName(string hotelName);
        HotelsInfoViewModel? GetByCountryId(int Id);
        HotelsInfoViewModel? GetByHotelId(int Id);

        List<HotelViewModel> GetUserLikeHotelList(string userEmail);
        List<UserReservationHotelsInfo> GetUserReservationHotels(string user_Guid);

        Task<SqlParameter> Create(HotelsInfoViewModel model);
        Task<SqlParameter> Update(UpdateReservationHotelViewModel model, string userId);
        UpdateReservationHotelViewModel Details(string userId, string bookingId);
        
        List<HotelsInfoViewModel>? SearchHotelByCountry(string Country);
        SqlParameter? AddLikeHotels(int hotelId, string userId);

    }
}
