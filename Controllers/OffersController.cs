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

			var offers = from o in _context.Offer
						 select o;

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

            var offer = await _context.Offer
                .Include(o => o.Agency)
                .Include(o => o.Location)
                .Include(o => o.OwnerInfo)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            var appointments = await _context.Appointment.Where(a => a.OfferId == id).ToListAsync();
            if (offer == null)
            {
                return NotFound();
            }

            var viewModel = new OfferDetailsViewModel { Appointments = appointments, Offer = offer };

			return View(viewModel);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "Address");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId");
            ViewData["OwnerInfoId"] = new SelectList(_context.OwnerInfo, "OwnerInfoId", "FirstName");
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferId,Reference,Type,LocationId,Description,OfferMediaId,Price,Surface,Active,ReverseRanking,RentOrSale,AgencyId,OwnerInfoId,CreatedAt")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "Address", offer.AgencyId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", offer.LocationId);
            ViewData["OwnerInfoId"] = new SelectList(_context.OwnerInfo, "OwnerInfoId", "FirstName", offer.OwnerInfoId);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offer.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "Address", offer.AgencyId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", offer.LocationId);
            ViewData["OwnerInfoId"] = new SelectList(_context.OwnerInfo, "OwnerInfoId", "FirstName", offer.OwnerInfoId);
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferId,Reference,Type,LocationId,Description,OfferMediaId,Price,Surface,Active,ReverseRanking,RentOrSale,AgencyId,OwnerInfoId,CreatedAt")] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.OfferId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "AgencyId", "Address", offer.AgencyId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", offer.LocationId);
            ViewData["OwnerInfoId"] = new SelectList(_context.OwnerInfo, "OwnerInfoId", "FirstName", offer.OwnerInfoId);
            return View(offer);
        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offer
                .Include(o => o.Agency)
                .Include(o => o.Location)
                .Include(o => o.OwnerInfo)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offer.FindAsync(id);
            if (offer != null)
            {
                _context.Offer.Remove(offer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return _context.Offer.Any(e => e.OfferId == id);
        }
      
	}
}
