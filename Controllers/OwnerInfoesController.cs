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
    public class OwnerInfoesController : Controller
    {
        private readonly MVCRealEstateContext _context;

        public OwnerInfoesController(MVCRealEstateContext context)
        {
            _context = context;
        }

        // GET: OwnerInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OwnerInfo.ToListAsync());
        }

        // GET: OwnerInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerInfo = await _context.OwnerInfo
                .FirstOrDefaultAsync(m => m.OwnerInfoId == id);
            if (ownerInfo == null)
            {
                return NotFound();
            }

            return View(ownerInfo);
        }

        // GET: OwnerInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OwnerInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerInfoId,FirstName,LastName,FullName,Phones,Email,Address")] OwnerInfo ownerInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ownerInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ownerInfo);
        }

        // GET: OwnerInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerInfo = await _context.OwnerInfo.FindAsync(id);
            if (ownerInfo == null)
            {
                return NotFound();
            }
            return View(ownerInfo);
        }

        // POST: OwnerInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OwnerInfoId,FirstName,LastName,FullName,Phones,Email,Address")] OwnerInfo ownerInfo)
        {
            if (id != ownerInfo.OwnerInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownerInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerInfoExists(ownerInfo.OwnerInfoId))
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
            return View(ownerInfo);
        }

        // GET: OwnerInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerInfo = await _context.OwnerInfo
                .FirstOrDefaultAsync(m => m.OwnerInfoId == id);
            if (ownerInfo == null)
            {
                return NotFound();
            }

            return View(ownerInfo);
        }

        // POST: OwnerInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerInfo = await _context.OwnerInfo.FindAsync(id);
            if (ownerInfo != null)
            {
                _context.OwnerInfo.Remove(ownerInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerInfoExists(int id)
        {
            return _context.OwnerInfo.Any(e => e.OwnerInfoId == id);
        }
    }
}
