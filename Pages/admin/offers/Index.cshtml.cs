using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Helpers;
using System.Text;

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

        public async Task<IActionResult> OnGetAsync(int? current)
        {
            var redirectResult = SessionHelper.RedirectIfNotLoggedIn(this);
            if (redirectResult != null)
            {
                return redirectResult;
            }

            PageIndex = current ?? 1;

            var count = await _context.Offer.CountAsync();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            Offers = await _context.Offer
                .Include(o => o.Location)
                .OrderBy(o => o.OfferId)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }

		public async Task<IActionResult> OnGetExportCSVAsync()
		{
			var offers = await _context.Offer.ToListAsync();

			var csvBuilder = new StringBuilder();
			csvBuilder.AppendLine("Référence,Type,Prix,Surface,Location,Description");

			foreach (var offer in offers)
			{
				csvBuilder.AppendLine($"{offer.Reference},{offer.Type},{offer.Price},{offer.Surface},{offer.Location?.City},{offer.Description}");
			}

			var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());
			var fileName = "offers.csv";

			return File(csvBytes, "text/csv", fileName);
		}
	}
}
