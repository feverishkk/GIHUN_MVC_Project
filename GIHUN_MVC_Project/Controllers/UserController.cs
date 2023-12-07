using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.ViewModels.Hotels;
using GIHUN_MVC_Project.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shared.Users;
using System.Net;
using System.Security.Claims;

namespace GIHUN_MVC_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationHotelRepository _reservationHotelRepository;   

        public UserController(IUserRepository userRepository, IReservationHotelRepository reservationHotelRepository)
        {
            _userRepository = userRepository;
            _reservationHotelRepository = reservationHotelRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ActionName("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }

            if ((model.Password != model.ConfirmPassword) || model.Password == null || model.ConfirmPassword == null || 
                 string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword) )
            {
                return View();
            }
            else
            {
                model.MemberSince = DateTime.Now;
                model.Guid = Guid.NewGuid().ToString();
                model.Password = HashAndVerify.Encrypt(model.Password);

                var result = await _userRepository.Register(model);

                if (Convert.ToInt32(result.SqlValue) != 0)
                {
                    return View("Register");
                }

                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ActionName("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var userEmail = _userRepository.Login(model.Email);
                var userGuId = _userRepository.GetUserGuId(userEmail);

                var userPW = _userRepository.GetUserPassword(userEmail, userGuId);

                var verifyPW = HashAndVerify.Encrypt(model.Password);

                if ( userPW != verifyPW )
                {
                    return BadRequest("비밀번호가 다릅니다.");
                }
                
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Email, model.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(99)
                };

                CookieOptions cookie = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(99),
                    Secure = true,
                    HttpOnly = true,
                    IsEssential = true
                };

                Response.Cookies.Append("Email", model.Email, cookie);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            Response.Cookies.Delete("Email");

            return RedirectToAction("Index", "Home");
        }





    }
}
