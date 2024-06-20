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

        [BindProperty]
        public IFormFileCollection OfferMediaFiles { get; set; }
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


            // Initialize the OfferMediaId list if null
            if (Offer.OfferMediaId == null)
            {
                Offer.OfferMediaId = new List<int>();
            }

            // Handle file uploads
            // Handle file uploads
            if (OfferMediaFiles != null && OfferMediaFiles.Count > 0)
            {
                // Ensure the directory exists
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                foreach (var file in OfferMediaFiles)
                {
                    // Save file to the server
                    var filePath = Path.Combine(uploadsFolderPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var offerMedia = new OfferMedia
                    {
                        Path = Path.Combine("uploads", file.FileName),
                        Type = "img"
                    };

                    // Add to OfferMedias list
                    _context.OfferMedia.Add(offerMedia);
                    await _context.SaveChangesAsync();  // Save to generate the ID

                    // Add the generated ID to Offer.OfferMediaId
                    Offer.OfferMediaId.Add(offerMedia.OfferMediaId);
                }

               /* // Update the Offer with the new OfferMediaIds
                _context.Offer.Update(Offer);
                await _context.SaveChangesAsync();*/
            }
            Offer.CreatedAt = new DateTime();
             _context.Offer.Add(Offer);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Detail", new { id = Offer.OfferId });
        }
    }
}
