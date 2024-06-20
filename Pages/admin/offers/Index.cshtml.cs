using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;

namespace MVCRealEstate.Pages.admin.offers
{
    public class IndexModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public IndexModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        public IList<Models.Offer> Offers { get; set; }
		public int PageIndex { get; set; } = 1;
		public int TotalPages { get; set; }
		public const int PageSize = 5;

		public async Task OnGetAsync(int? current)
		{
			PageIndex = current ?? 1;

			var count = await _context.Offer.CountAsync();
			TotalPages = (int)Math.Ceiling(count / (double)PageSize);

			Offers = await _context.Offer
				.Include(o => o.Location)
				.OrderBy(o => o.OfferId)
				.Skip((PageIndex - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync();
		}
    }
}
