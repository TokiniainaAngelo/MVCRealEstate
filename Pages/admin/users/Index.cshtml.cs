using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.users
{
    public class IndexModel : PageModel
    {
		private readonly MVCRealEstateContext _context;

		public IndexModel(MVCRealEstateContext context)
		{
			_context = context;
		}

		public IList<User> Users { get; set; }
		public int PageIndex { get; set; } = 1;
		public int TotalPages { get; set; }
		public const int PageSize = 5;

		public async Task OnGetAsync(int? current)
		{
			PageIndex = current ?? 1;

			var count = await _context.OwnerInfo.CountAsync();
			TotalPages = (int)Math.Ceiling(count / (double)PageSize);
			Users = await _context.User.Skip((PageIndex - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync(); 
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var user = await _context.User.FindAsync(id);
			if (user != null)
			{
				_context.User.Remove(user);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage();
		}
	}
}
