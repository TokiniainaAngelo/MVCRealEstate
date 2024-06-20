using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.locations
{
    public class DetailModel : PageModel
    {
		private readonly MVCRealEstateContext _context;

		public DetailModel(MVCRealEstateContext context)
		{
			_context = context;
		}

		public Location Location { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Location = await _context.Location.FirstOrDefaultAsync(m => m.LocationId == id);

			if (Location == null)
			{
				return NotFound();
			}

			return Page();
		}
	}
}
