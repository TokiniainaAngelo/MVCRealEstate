using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Helpers;
using MVCRealEstate.Models;
using System.Globalization;
using System.Text;

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

        public async Task<IActionResult> OnGetAsync(int? current)
        {
            var redirectResult = SessionHelper.RedirectIfNotLoggedIn(this);
            if (redirectResult != null)
            {
                return redirectResult;
            }

            PageIndex = current ?? 1;

            var count = await _context.Location.CountAsync();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            Locations = await _context.Location
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }

		public async Task<IActionResult> OnPostImportCSVAsync(IFormFile csvFile)
		{
			if (csvFile == null || csvFile.Length == 0)
			{
				ModelState.AddModelError(string.Empty, "Veuillez sélectionner un fichier CSV.");
				return Page();
			}

			using (var reader = new StreamReader(csvFile.OpenReadStream(), Encoding.UTF8))
			{
				var lineNumber = 0;
				while (!reader.EndOfStream)
				{
					var line = await reader.ReadLineAsync();
					if (lineNumber == 0)
					{
						// Skip the header line
						lineNumber++;
						continue;
					}

					var values = line.Split(',');

					if (values.Length < 4)
					{
						// Skip lines with insufficient data
						continue;
					}

					var location = new Location
					{
						Address = values[0],
						City = values[1],
						latitude =values[2],
						longitude = values[3],
					};

					_context.Location.Add(location);
				}

				await _context.SaveChangesAsync();
			}

			return RedirectToPage();
		}
	}
}
