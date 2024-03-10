using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;
using System.Diagnostics;

namespace MVCRealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		private readonly MVCRealEstateContext _context;


		public HomeController(ILogger<HomeController> logger, MVCRealEstateContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
			// Retrieve session values
			var userId = HttpContext.Session.GetString("UserId");
			var login = HttpContext.Session.GetString("Login");
			var offers =  _context.Offer.Include(o => o.Agency)
				.Include(o => o.Location)
				.Include(o => o.OwnerInfo).OrderByDescending(x => x.CreatedAt).Take(3).ToList();
			// Use the session values as needed
			ViewData["UserId"] = userId;
			ViewData["Login"] = login;
            ViewData["Offers"] = offers;
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
