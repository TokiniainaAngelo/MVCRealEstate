using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Controllers
{
    public class OffersController : Controller
    {
        private readonly MVCRealEstateContext _context;

        public OffersController(MVCRealEstateContext context)
        {
            _context = context;
        }

		// GET: Offers
		// Search
		public async Task<IActionResult> Index(string search)
		{
			if (_context.Offer == null)
			{
				return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
			}
			// Retrieve session values
			var userId = HttpContext.Session.GetString("UserId");
			var login = HttpContext.Session.GetString("Login");

			// Use the session values as needed
			ViewData["UserId"] = userId;
			ViewData["Login"] = login;

			var offers = _context.Offer
						 .Include(o => o.Agency)
						 .Include(o => o.OwnerInfo)
						 .Include(o => o.Location)
						 .AsQueryable();

			if (!String.IsNullOrEmpty(search))
			{
				offers = offers.Where(s => s.Reference!.Contains(search) || s.Description!.Contains(search));
			}

			return View(await offers.ToListAsync());
		}

		// GET: Offers/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			// Retrieve session values
			var userId = HttpContext.Session.GetString("UserId");
			var login = HttpContext.Session.GetString("Login");
			// Use the session values as needed
			ViewData["UserId"] = userId;
			ViewData["Login"] = login;


			var offer = await _context.Offer
                .Include(o => o.Agency)
                .Include(o => o.Location)
                .Include(o => o.OwnerInfo)
				.FirstOrDefaultAsync(m => m.OfferId == id);
			
			if (offer == null)
            {
                return NotFound();
            }

			var appointments = await _context.Appointment.Where(a => a.OfferId == id).ToListAsync();
			var offerMedias = await _context.OfferMedia.Where(m => offer.OfferMediaId.Contains(m.OfferMediaId)).ToListAsync();
			var viewModel = new OfferDetailsViewModel { Appointments = appointments, Offer = offer, Medias=offerMedias };

			return View(viewModel);
        }

		private bool OfferExists(int id)
        {
            return _context.Offer.Any(e => e.OfferId == id);
        }
      
	}
}
