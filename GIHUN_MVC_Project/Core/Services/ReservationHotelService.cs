using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Models.Hotels;
using GIHUN_MVC_Project.Models.Users;
using GIHUN_MVC_Project.ViewModels.Hotels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace GIHUN_MVC_Project.Core.Services
{
    public class ReservationHotelService : IReservationHotelRepository
    {
        private readonly IConfiguration _configuration;

        public ReservationHotelService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<HotelsInfoViewModel> GetAll()
        {
            var result = HotelList();

            return result;
        }

        public HotelsInfoViewModel? GetByHotelId(int id)
        {
            var hotel = HotelList();

            HotelsInfoViewModel? result = new HotelsInfoViewModel();

            if (hotel == null || hotel.Count <= 0)
            {
                return null;
            }
            else
            {
                result = hotel?.Find(x => x?.hotel_id == id);
            }

            return result;
        }

        public HotelsInfoViewModel? GetByHotelName(string hotelName)
        {
            var hotel = HotelList();

            HotelsInfoViewModel? result = new HotelsInfoViewModel();

            if (hotel == null || hotel.Count <= 0)
            {
                return null;
            }
            else
            {
                result = hotel?.Find(x => x?.hotel_name == hotelName);
            }

            return result;
        }

        public HotelsInfoViewModel? GetByCountryId(int id)
        {
            var hotel = HotelList();

            HotelsInfoViewModel? result = new HotelsInfoViewModel();

            if (hotel == null || hotel.Count <= 0)
            {
                return null;
            }
            else
            {
                result = hotel?.Find(x => x?.country_id == id);
            }

            return result;
        }

        /// <summary>
        /// 호텔 예약하는 기능
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SqlParameter> Create(HotelsInfoViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("usp_Insert_Reservation_Hotel", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_user_guid", model.ReservationHotel.UserInfo.Guid);
                cmd.Parameters.AddWithValue("@p_booking_id", model.ReservationHotel.ReservationHotel.Booking_Id);

                cmd.Parameters.AddWithValue("@p_check_in", model.ReservationHotel.ReservationHotel.Check_In);
                cmd.Parameters.AddWithValue("@p_check_out", model.ReservationHotel.ReservationHotel.Check_Out);
                cmd.Parameters.AddWithValue("@p_adults", model.ReservationHotel.ReservationHotel.Adults);
                cmd.Parameters.AddWithValue("@p_children", model.ReservationHotel.ReservationHotel.Children);
                cmd.Parameters.AddWithValue("@p_infants", model.ReservationHotel.ReservationHotel.Infants);
                cmd.Parameters.AddWithValue("@p_pets", model.ReservationHotel.ReservationHotel.Pets);
                cmd.Parameters.AddWithValue("@p_resv_last_name", model.ReservationHotel.UserInfo.Last_Name);
                cmd.Parameters.AddWithValue("@p_resv_first_name", model.ReservationHotel.UserInfo.First_Name);
                cmd.Parameters.AddWithValue("@p_resv_mobile", model.ReservationHotel.UserInfo.Mobile);
                cmd.Parameters.AddWithValue("@p_resv_email", model.ReservationHotel.UserInfo.Email);

                cmd.Parameters.AddWithValue("@p_paid_guid", model.ReservationHotel.Paid.Paid_Guid);
                cmd.Parameters.AddWithValue("@p_paid_total", model.ReservationHotel.Paid.Paid_Total);
                cmd.Parameters.AddWithValue("@p_paid_currency", model.ReservationHotel.Paid.Paid_Currency);
                cmd.Parameters.AddWithValue("@p_paid_date", model.ReservationHotel.Paid.Paid_Date);

                cmd.Parameters.AddWithValue("@p_hotel_name", model.hotel_name);
                cmd.Parameters.AddWithValue("@p_hotel_service_rating", model.star_rating);
                cmd.Parameters.AddWithValue("@p_hotel_country", model.country);
                cmd.Parameters.AddWithValue("@p_hotel_location_rating", model.rating_average);
                cmd.Parameters.AddWithValue("@p_hotel_location", model.city);
                cmd.Parameters.AddWithValue("@p_hotel_address", model.addressline1);
                cmd.Parameters.AddWithValue("@p_hotel_postcode", model.zipcode);

                SqlParameter outputParam = new SqlParameter();
                outputParam.ParameterName = "@result";
                outputParam.Direction = ParameterDirection.Output;
                outputParam.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(outputParam);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return outputParam;
            }
        }

        public UpdateReservationHotelViewModel Details(string userId, string bookingId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var hotels = new UpdateReservationHotelViewModel();
                var cmd = new SqlCommand("usp_Get_Reservation_Hotel_Details", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_user_id", userId);
                cmd.Parameters.AddWithValue("@p_booking_id", bookingId);
            
                connection.Open();
                cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        hotels.Hotel_Name = reader["Hotel_Name"].ToString();
                        hotels.Addressline1 = reader["Hotel_Address"].ToString();
                        hotels.Country = reader["Hotel_Country"].ToString();
                        hotels.City = reader["Hotel_Location"].ToString();
                        hotels.Zipcode = reader["Hotel_PostCode"].ToString();
                        hotels.Hotel_Location_Rating = (double)reader["Hotel_Location_Rating"];
                        hotels.Hotel_Service_Rating = (double)reader["Hotel_Service_Rating"];
                        hotels.Booking_Id = reader["Booking_Id"].ToString();

                        hotels.Paid_Currency = reader["Paid_Currency"].ToString();
                        hotels.Paid_Date =  Convert.ToDateTime(reader["Paid_Date"]);
                        hotels.Paid_Total = Convert.ToInt32(reader["Paid_Total"]);

                        hotels.Adults =    Convert.ToInt32(reader["Adults"]);
                        hotels.Children =  Convert.ToInt32(reader["Children"]);
                        hotels.Infants =   Convert.ToInt32(reader["Infants"]);
                        hotels.Pets =      Convert.ToInt32(reader["Pets"]);
                        hotels.Check_In =  Convert.ToDateTime(reader["Check_In"]);
                        hotels.Check_Out = Convert.ToDateTime(reader["Check_Out"]);

                        hotels.Last_Name =  reader["Reservation_User_Last_Name"].ToString();
                        hotels.First_Name = reader["Reservation_User_First_Name"].ToString();
                        hotels.Mobile =     reader["Reservation_MobileNumber"].ToString();
                        hotels.Email =     reader["Reservation_Email"].ToString();

                        hotels.photo1 = HotelList().FindAll(x => x.hotel_name == reader["Hotel_Name"].ToString()).Select(x => x.photo1).FirstOrDefault();
                        hotels.photo2 = HotelList().FindAll(x => x.hotel_name == reader["Hotel_Name"].ToString()).Select(x => x.photo2).FirstOrDefault();
                        hotels.photo3 = HotelList().FindAll(x => x.hotel_name == reader["Hotel_Name"].ToString()).Select(x => x.photo3).FirstOrDefault();
                        hotels.photo4 = HotelList().FindAll(x => x.hotel_name == reader["Hotel_Name"].ToString()).Select(x => x.photo4).FirstOrDefault();
                        hotels.photo5 = HotelList().FindAll(x => x.hotel_name == reader["Hotel_Name"].ToString()).Select(x => x.photo5).FirstOrDefault();

                    }
                }

                return hotels;
            }
        }

        public async Task<SqlParameter> Update(UpdateReservationHotelViewModel model, string userId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("usp_Update_Reservation_Hotel", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_user_guid", userId);

                cmd.Parameters.AddWithValue("@p_booking_id", model.Booking_Id);
                cmd.Parameters.AddWithValue("@p_check_in", model.Check_In);
                cmd.Parameters.AddWithValue("@p_check_out", model.Check_Out);
                cmd.Parameters.AddWithValue("@p_adults", model.Adults);
                cmd.Parameters.AddWithValue("@p_children", model.Children);
                cmd.Parameters.AddWithValue("@p_infants", model.Infants);
                cmd.Parameters.AddWithValue("@p_pets", model.Pets);

                cmd.Parameters.AddWithValue("@p_resv_last_name", model.Last_Name);
                cmd.Parameters.AddWithValue("@p_resv_first_name", model.First_Name);
                cmd.Parameters.AddWithValue("@p_resv_mobile", model.Mobile);
                cmd.Parameters.AddWithValue("@p_resv_email", model.Email);

                cmd.Parameters.AddWithValue("@p_paid_guid", model.Booking_Id);
                cmd.Parameters.AddWithValue("@p_paid_total", model.Paid_Total);
                cmd.Parameters.AddWithValue("@p_paid_currency", model.Paid_Currency);
                cmd.Parameters.AddWithValue("@p_paid_date", model.Paid_Date);

                cmd.Parameters.AddWithValue("@p_hotel_name", model.Hotel_Name);

                SqlParameter outputParam = new SqlParameter();
                outputParam.ParameterName = "@result";
                outputParam.Direction = ParameterDirection.Output;
                outputParam.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(outputParam);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return outputParam;
            }
        }

        /// <summary>
        /// 나라이름으로 호텔 찾는 기능
        /// 아직 사용X
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public List<HotelsInfoViewModel>? SearchHotelByCountry(string country)
        {
            if (country == null || string.IsNullOrEmpty(country) || string.IsNullOrWhiteSpace(country))
            {
                return null;
            }

            var hotelList = HotelList();

            if (hotelList.Exists(x => x.country == country) == false)
            {
                return null;
            }

            List<HotelsInfoViewModel> hotelModel = new List<HotelsInfoViewModel>();

            var hotelFind = hotelList.FindAll(x => x.country == country).DistinctBy(x => x.addressline1).ToList();

            foreach (var hotel in hotelFind)
            {
                hotelModel.Add(new HotelsInfoViewModel
                {
                    hotel_id = hotel.hotel_id,
                    addressline1 = hotel.addressline1,
                    addressline2 = hotel.addressline2,
                    hotel_name = hotel.hotel_name,
                    brand_name = hotel.brand_name,
                    rating_average = hotel.rating_average,
                    chain_name = hotel.chain_name,
                    city = hotel.city,
                    continent_name = hotel.continent_name,
                    country = hotel.country,
                    hotel_formerly_name = hotel.hotel_formerly_name,
                    latitude = hotel.latitude,
                    longitude = hotel.longitude,
                    overview = hotel.overview,
                    number_of_reviews = hotel.number_of_reviews,
                    numberfloors = hotel.numberfloors,
                    numberrooms = hotel.numberrooms,
                    photo1 = hotel.photo1,
                    photo2 = hotel.photo2,
                    photo3 = hotel.photo3,
                    photo4 = hotel.photo4,
                    photo5 = hotel.photo5,
                    rates_currency = hotel.rates_currency,
                    star_rating = hotel.star_rating,
                    state = hotel.state,
                    url = hotel.url,
                    yearopened = hotel.yearopened,
                    yearrenovated = hotel.yearrenovated,
                    rates_from = hotel.rates_from,
                    zipcode = hotel.zipcode
                });
            }

            return hotelModel;
        }

        /// <summary>
        /// Json파일 -> List형태로
        /// </summary>
        /// <returns></returns>
        private List<HotelsInfoViewModel>? HotelList()
        {
            var location = @"C:\Users\gihun\source\repos\GIHUN_MVC_Project\Shared\HotelList\hotels.json";
            var list = JsonConvert.DeserializeObject<List<HotelsInfoViewModel>>(File.ReadAllText(location));

            return list;
        }

        /// <summary>
        /// 유저가 좋아하는 호텔을 즐겨찾기 해주는 기능
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SqlParameter? AddLikeHotels(int hotelId, string userId)
        {
            var hotelList = HotelList();

            if (userId == null || string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var hotelName = hotelList.FirstOrDefault(x => x.hotel_id == hotelId);

            if (hotelName.hotel_name == string.Empty || string.IsNullOrEmpty(hotelName.hotel_name))
            {
                return null;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var cmd = new SqlCommand("usp_User_Likes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_user_id", userId);
                    cmd.Parameters.AddWithValue("@p_hotel_name", hotelName.hotel_name);
                    cmd.Parameters.AddWithValue("@p_hotel_service_rating", hotelName.star_rating);
                    cmd.Parameters.AddWithValue("@p_hotel_country", hotelName.country);
                    cmd.Parameters.AddWithValue("@p_hotel_location_rating", hotelName.rating_average);
                    cmd.Parameters.AddWithValue("@p_hotel_location", hotelName.city);
                    cmd.Parameters.AddWithValue("@p_hotel_address", hotelName.addressline1);
                    cmd.Parameters.AddWithValue("@p_hotel_postcode", hotelName.zipcode);

                    SqlParameter outputParam = new SqlParameter();
                    outputParam.ParameterName = "@result";
                    outputParam.Direction = ParameterDirection.Output;
                    outputParam.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    return outputParam;
                }

            }

        }

        /// <summary>
        /// 유저가 즐겨찾기한 호텔 목록들을 가져오는 기능
        /// </summary>
        /// <param name="user_Guid"></param>
        /// <returns></returns>
        public List<HotelViewModel>? GetUserLikeHotelList(string user_Guid)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var hotels = new List<HotelViewModel>();
                var cmd = new SqlCommand("usp_Get_User_Likes_Hotel", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_user_guid", user_Guid);

                connection.Open();
                cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hotels.Add(new HotelViewModel
                        {
                            Hotel_Name = (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name"))),
                            Hotel_Address = (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Address"))),
                            Hotel_Country = (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Country"))),
                            Hotel_Location = (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Location"))),
                            Hotel_Location_Rating = (double)(reader?.GetValue(reader.GetOrdinal("Hotel_Location_Rating"))),
                            Hotel_PostCode = (string)(reader?.GetValue(reader.GetOrdinal("Hotel_PostCode"))),
                            Hotel_Service_Rating = (double)(reader?.GetValue(reader.GetOrdinal("Hotel_Service_Rating"))),
                            

                            Hotel_Id = (int)HotelList().FindAll(x => x.hotel_name == (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name")))).Select(x => x.hotel_id).FirstOrDefault(),
                            photo1 = HotelList().FindAll(x => x.hotel_name == (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name")))).Select(x => x.photo1).FirstOrDefault(),
                            photo2 = HotelList().FindAll(x => x.hotel_name == (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name")))).Select(x => x.photo2).FirstOrDefault(),
                            photo3 = HotelList().FindAll(x => x.hotel_name == (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name")))).Select(x => x.photo3).FirstOrDefault(),
                            photo4 = HotelList().FindAll(x => x.hotel_name == (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name")))).Select(x => x.photo4).FirstOrDefault(),
                            photo5 = HotelList().FindAll(x => x.hotel_name == (string)(reader?.GetValue(reader.GetOrdinal("Hotel_Name")))).Select(x => x.photo5).FirstOrDefault()
                        });
                        hotels.ToList();
                    }
                }
                return hotels;
            }
        }

        /// <summary>
        /// 고객이 예약한 호텔들의 목록들을 가져온다.
        /// </summary>
        /// <param name="user_Guid"></param>
        /// <returns></returns>
        public List<UserReservationHotelsInfo> GetUserReservationHotels(string user_Guid)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var hotelsInfo = new List<UserReservationHotelsInfo>();

                
                var cmd = new SqlCommand("usp_Get_User_Reservation_Hotel_Info", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_user_id", user_Guid);

                connection.Open();
                cmd.ExecuteNonQuery();
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reservationHotel = new ReservationHotel();
                        var hotel = new Hotel();
                        var paid = new Paid();

                        reservationHotel.Booking_Id = reader["Booking_Id"].ToString();
                        reservationHotel.Check_In = Convert.ToDateTime(reader["Check_In"]);
                        reservationHotel.Check_Out = Convert.ToDateTime(reader["Check_Out"]);
                        reservationHotel.Adults = Convert.ToInt32(reader["Adults"]);
                        reservationHotel.Children = Convert.ToInt32(reader["Children"]);
                        reservationHotel.Infants = Convert.ToInt32(reader["Infants"]);
                        reservationHotel.Pets = Convert.ToInt32(reader["Pets"]);

                        hotel.Hotel_Name = reader["Hotel_Name"].ToString();
                        hotel.Hotel_Country = reader["Hotel_Country"].ToString();
                        hotel.Hotel_Location = reader["Hotel_Location"].ToString();
                        hotel.Hotel_Address = reader["Hotel_Address"].ToString();
                        hotel.Hotel_PostCode = reader["Hotel_PostCode"].ToString();

                        paid.Paid_Currency = reader["Paid_Currency"].ToString();
                        paid.Paid_Date = Convert.ToDateTime(reader["Paid_Date"]);
                        paid.Paid_Total = Convert.ToInt32(reader["Paid_Total"]);

                        hotelsInfo.Add(new UserReservationHotelsInfo
                        {
                            ReservationHotel = reservationHotel,
                            Hotel = hotel,
                            Paid = paid
                        });
                    }

                    return hotelsInfo;
                }
            }
        }
    }
}
