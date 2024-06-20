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


            if (Offer.OfferMediaId == null)
            {
                Offer.OfferMediaId = new List<int>();
            }

            if (OfferMediaFiles != null && OfferMediaFiles.Count > 0)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                foreach (var file in OfferMediaFiles)
                {
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

                    _context.OfferMedia.Add(offerMedia);
                    await _context.SaveChangesAsync();  
                    Offer.OfferMediaId.Add(offerMedia.OfferMediaId);
                }

            }
            Offer.CreatedAt = DateTime.Now;
             _context.Offer.Add(Offer);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Detail", new { id = Offer.OfferId });
        }
    }
}
