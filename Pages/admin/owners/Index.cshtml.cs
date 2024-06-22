using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Helpers;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.owners
{
    public class IndexModel : PageModel
    {
		private readonly MVCRealEstateContext _context;

		public IndexModel(MVCRealEstateContext context)
		{
			_context = context;
		}

		public IList<OwnerInfo> Owners { get; set; }
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

            var count = await _context.OwnerInfo.CountAsync();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            Owners = await _context.OwnerInfo
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }
    }
}
