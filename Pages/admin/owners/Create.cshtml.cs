using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.owners
{
    public class CreateModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public CreateModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OwnerInfo OwnerInfo { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OwnerInfo.Add(OwnerInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
