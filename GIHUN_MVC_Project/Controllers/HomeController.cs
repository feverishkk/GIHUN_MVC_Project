using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Models;
using GIHUN_MVC_Project.ViewModels.Hotels;
using GIHUN_MVC_Project.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GIHUN_MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReservationHotelRepository _hotelRepository;

        
        public HomeController(IReservationHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;

            
        }

        public IActionResult Index()
        {
            var item1 = _hotelRepository.GetByCountryId(3); // 일본
            var item2 = _hotelRepository.GetByCountryId(107); // UK
            var item3 = _hotelRepository.GetByCountryId(205); // 이태리
            var item4 = _hotelRepository.GetByCountryId(181); // 미국

            ViewBag.Item1 = item1;
            ViewBag.Item2 = item2;
            ViewBag.Item3 = item3;
            ViewBag.Item4 = item4;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
