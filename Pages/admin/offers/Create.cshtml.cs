using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.offers
{
    public class CreateModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public CreateModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Offer Offer { get; set; }
        public List<Location> Locations { get; set; }
        public List<OwnerInfo> OwnerInfos { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Locations = await _context.Location.ToListAsync();
            OwnerInfos = await _context.OwnerInfo.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            // Validate the LocationId
            var locationExists = await _context.Location.AnyAsync(l => l.LocationId == Offer.LocationId);
            if (!locationExists)
            {
                ModelState.AddModelError("Offer.LocationId", "La localisation sélectionnée n'existe pas.");
                Locations = await _context.Location.ToListAsync();
                return Page();
            }

            _context.Offer.Add(Offer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Detail", new { id = Offer.OfferId });
        }
    }
}
