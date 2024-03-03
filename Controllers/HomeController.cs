using Microsoft.AspNetCore.Mvc;
using MVCRealEstate.Models;
using System.Diagnostics;

namespace MVCRealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			// Retrieve session values
			var userId = HttpContext.Session.GetString("UserId");
			var login = HttpContext.Session.GetString("Login");

			// Use the session values as needed
			ViewData["UserId"] = userId;
			ViewData["Login"] = login;
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
