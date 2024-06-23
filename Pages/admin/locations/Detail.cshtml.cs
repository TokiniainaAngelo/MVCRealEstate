using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Helpers;
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
		public string ErrorMessage { get; set; }


		public async Task<IActionResult> OnGetAsync(int? id)
		{
            var redirectResult = SessionHelper.RedirectIfNotLoggedIn(this);
            if (redirectResult != null)
            {
                return redirectResult;
            }

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
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var location = await _context.Location.FindAsync(id);

			if (location == null)
			{
				return NotFound();
			}
			var offersWithLocation = await _context.Offer.AnyAsync(o => o.LocationId == id);
			if (offersWithLocation)
			{
				ErrorMessage = "Cette localisation est encore associée à une offre";
				return RedirectToPage(new { id = id });
			}

			_context.Location.Remove(location);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}

	}
}
