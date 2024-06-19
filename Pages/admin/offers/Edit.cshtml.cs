using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.offers
{
    public class EditModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public EditModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Offer Offer { get; set; }
        public List<Location> Locations { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Offer = await _context.Offer.FirstOrDefaultAsync(m => m.OfferId == id);
            Locations = await _context.Location.ToListAsync();

            if (Offer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           /* if (!ModelState.IsValid)
            {
                Locations = await _context.Location.ToListAsync();
                return Page();
            }*/

            // Validate the LocationId
            var locationExists = await _context.Location.AnyAsync(l => l.LocationId == Offer.LocationId);
            if (!locationExists)
            {
                ModelState.AddModelError("Offer.LocationId", "La localisation sélectionnée n'existe pas.");
                Locations = await _context.Location.ToListAsync();
                return Page();
            }

            var offerToUpdate = await _context.Offer.FirstOrDefaultAsync(m => m.OfferId == Offer.OfferId);

            if (offerToUpdate == null)
            {
                return NotFound();
            }

            offerToUpdate.Reference = Offer.Reference;
            offerToUpdate.Type = Offer.Type;
            offerToUpdate.Price = Offer.Price;
            offerToUpdate.Surface = Offer.Surface;
            offerToUpdate.Description = Offer.Description;
            offerToUpdate.RentOrSale = Offer.RentOrSale;
            offerToUpdate.LocationId = Offer.LocationId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(Offer.OfferId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Detail", new { id = Offer.OfferId });
        }

        private bool OfferExists(int id)
        {
            return _context.Offer.Any(e => e.OfferId == id);
        }
    }
}
