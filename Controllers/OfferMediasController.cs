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
    public class OfferMediasController : Controller
    {
        private readonly MVCRealEstateContext _context;

        public OfferMediasController(MVCRealEstateContext context)
        {
            _context = context;
        }

        // GET: OfferMedias
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfferMedia.ToListAsync());
        }

        // GET: OfferMedias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerMedia = await _context.OfferMedia
                .FirstOrDefaultAsync(m => m.OfferMediaId == id);
            if (offerMedia == null)
            {
                return NotFound();
            }

            return View(offerMedia);
        }

        // GET: OfferMedias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfferMedias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferMediaId,Type,Path")] OfferMedia offerMedia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offerMedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offerMedia);
        }

        // GET: OfferMedias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerMedia = await _context.OfferMedia.FindAsync(id);
            if (offerMedia == null)
            {
                return NotFound();
            }
            return View(offerMedia);
        }

        // POST: OfferMedias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferMediaId,Type,Path")] OfferMedia offerMedia)
        {
            if (id != offerMedia.OfferMediaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offerMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferMediaExists(offerMedia.OfferMediaId))
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
            return View(offerMedia);
        }

        // GET: OfferMedias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerMedia = await _context.OfferMedia
                .FirstOrDefaultAsync(m => m.OfferMediaId == id);
            if (offerMedia == null)
            {
                return NotFound();
            }

            return View(offerMedia);
        }

        // POST: OfferMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offerMedia = await _context.OfferMedia.FindAsync(id);
            if (offerMedia != null)
            {
                _context.OfferMedia.Remove(offerMedia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferMediaExists(int id)
        {
            return _context.OfferMedia.Any(e => e.OfferMediaId == id);
        }
    }
}
