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
    public class InterestsController : Controller
    {
        private readonly MVCRealEstateContext _context;

        public InterestsController(MVCRealEstateContext context)
        {
            _context = context;
        }

        // GET: Interests
        public async Task<IActionResult> Index()
        {
            var mVCRealEstateContext = _context.Interest.Include(i => i.Offer).Include(i => i.User);
            return View(await mVCRealEstateContext.ToListAsync());
        }

        // GET: Interests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest = await _context.Interest
                .Include(i => i.Offer)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.InterestId == id);
            if (interest == null)
            {
                return NotFound();
            }

            return View(interest);
        }

        // GET: Interests/Create
        public IActionResult Create()
        {
            ViewData["OfferId"] = new SelectList(_context.Set<Offer>(), "OfferId", "Description");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName");
            return View();
        }

        // POST: Interests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestId,Name,OfferId,UserId,LastUpdatedAt,CreatedAt")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OfferId"] = new SelectList(_context.Set<Offer>(), "OfferId", "Description", interest.OfferId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", interest.UserId);
            return View(interest);
        }

        // GET: Interests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest = await _context.Interest.FindAsync(id);
            if (interest == null)
            {
                return NotFound();
            }
            ViewData["OfferId"] = new SelectList(_context.Set<Offer>(), "OfferId", "Description", interest.OfferId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", interest.UserId);
            return View(interest);
        }

        // POST: Interests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InterestId,Name,OfferId,UserId,LastUpdatedAt,CreatedAt")] Interest interest)
        {
            if (id != interest.InterestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterestExists(interest.InterestId))
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
            ViewData["OfferId"] = new SelectList(_context.Set<Offer>(), "OfferId", "Description", interest.OfferId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", interest.UserId);
            return View(interest);
        }

        // GET: Interests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest = await _context.Interest
                .Include(i => i.Offer)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.InterestId == id);
            if (interest == null)
            {
                return NotFound();
            }

            return View(interest);
        }

        // POST: Interests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interest = await _context.Interest.FindAsync(id);
            if (interest != null)
            {
                _context.Interest.Remove(interest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterestExists(int id)
        {
            return _context.Interest.Any(e => e.InterestId == id);
        }
    }
}
