using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.locations
{
    public class IndexModel : PageModel
    {
		private readonly MVCRealEstateContext _context;

		public IndexModel(MVCRealEstateContext context)
		{
			_context = context;
		}

		public IList<Location> Locations { get; set; }
		public int PageIndex { get; set; } = 1;
		public int TotalPages { get; set; }
		public const int PageSize = 5;

		public async Task OnGetAsync(int? current)
		{
			PageIndex = current ?? 1;

			var count = await _context.Offer.CountAsync();
			TotalPages = (int)Math.Ceiling(count / (double)PageSize);

			Locations = await _context.Location.Skip((PageIndex - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync(); ;
		}
	}
}
