using GIHUN_MVC_Project.Models.Hotels;
using GIHUN_MVC_Project.Models.Users;
using GIHUN_MVC_Project.ViewModels.Hotels;
using System.Data.SqlClient;

namespace GIHUN_MVC_Project.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<SqlParameter> Register(User model);
        string Login(string userEmail);
        string GetUserGuId(string Email);
        string GetUserPassword(string Email, string Guid);
    }
}
