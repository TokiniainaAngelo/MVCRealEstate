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
        public List<OfferMedia> OfferMedias { get; set; }

        [BindProperty]
        public IFormFileCollection OfferMediaFiles { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Offer = await _context.Offer.FirstOrDefaultAsync(m => m.OfferId == id);
            Locations = await _context.Location.ToListAsync();

            if (Offer == null)
            {
                return NotFound();
            }
            OfferMedias = await _context.OfferMedia
                .Where(o => Offer.OfferMediaId.Contains(o.OfferMediaId))
                .ToListAsync();
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

            // Handle file uploads
            if (OfferMediaFiles != null && OfferMediaFiles.Count > 0)
            {
                foreach (var file in OfferMediaFiles)
                {
                    var offerMedia = new OfferMedia
                    {
                        Path = Path.Combine("uploads", file.FileName),
                        Type = "img"
                    };

                    // Save file to the server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.OfferMedia.Add(offerMedia);
                    await _context.SaveChangesAsync();
                    offerToUpdate.OfferMediaId.Add(offerMedia.OfferMediaId);
                }

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
