using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.offers
{
    public class DetailModel : PageModel
    {
		private readonly MVCRealEstateContext _context;

		public DetailModel(MVCRealEstateContext context)
		{
			_context = context;
		}

		public Offer Offer { get; set; }
		public List<OfferMedia> OfferMedias { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Offer = await _context.Offer
				.Include(o => o.Location)
				.Include(o => o.OfferMedias)
				.Include(o => o.Agency)
				.Include(o => o.OwnerInfo)
				.FirstOrDefaultAsync(m => m.OfferId == id);

			if (Offer == null)
			{
				return NotFound();
			}

			OfferMedias = await _context.OfferMedia
				.Where(o => Offer.OfferMediaId.Contains(o.OfferMediaId))
				.ToListAsync();
			return Page();
		}
	}
}
