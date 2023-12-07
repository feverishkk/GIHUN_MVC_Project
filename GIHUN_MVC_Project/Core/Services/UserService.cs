using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Models.Hotels;
using GIHUN_MVC_Project.Models.Users;
using GIHUN_MVC_Project.ViewModels.Hotels;
using NuGet.Protocol;
using System.Data.SqlClient;

namespace GIHUN_MVC_Project.Core.Services
{
    public class UserService : IUserRepository
    {

        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetUserGuId(string Email)
        {
            string user = string.Empty;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("SELECT [Guid] FROM Users WHERE Email = @userEmail", connection);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add("@userEmail", System.Data.SqlDbType.VarChar).Value = Email;
                connection.Open();


                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = reader[0].ToString();
                    }
                    return user;
                }
            }
        }

        public string GetUserPassword(string Email, string Guid)
        {
            string user = string.Empty;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("SELECT Password FROM Users WHERE Email = @userEmail AND [Guid] = @Guid", connection);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add("@userEmail", System.Data.SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Guid", System.Data.SqlDbType.VarChar).Value = Guid;
                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = reader[0].ToString();
                    }
                    return user;
                }
            }
        }

        public string Login(string userEmail)
        {
            string user = string.Empty;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("SELECT Email FROM Users WHERE Email = @userEmail", connection);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add("@userEmail", System.Data.SqlDbType.VarChar).Value = userEmail;
                connection.Open();


                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        user = reader[0].ToString();
                    }
                    return user;
                }
            }
        }

        public async Task<SqlParameter> Register(User model)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("usp_Register_User", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_guid", model.Guid.ToString());
                cmd.Parameters.AddWithValue("@p_nick_name", model.Nick_Name.ToString());
                cmd.Parameters.AddWithValue("@p_last_name", model.Last_Name.ToString());
                cmd.Parameters.AddWithValue("@p_first_name", model.First_Name.ToString());
                cmd.Parameters.AddWithValue("@p_password", model.Password.ToString());
                cmd.Parameters.AddWithValue("@p_mobile", model.Mobile.ToString());
                cmd.Parameters.AddWithValue("@p_email", model.Email.ToString());
                cmd.Parameters.AddWithValue("@p_birthday", Convert.ToDateTime(model.BirthDay));
                cmd.Parameters.AddWithValue("@p_member_since", Convert.ToDateTime(model.MemberSince));

                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@p_result";
                outParam.Direction = System.Data.ParameterDirection.Output;
                outParam.SqlDbType = System.Data.SqlDbType.Int;

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return outParam;
            }
        }
    }
}
