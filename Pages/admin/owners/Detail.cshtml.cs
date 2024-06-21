using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.owners
{
    public class DetailModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public DetailModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        public OwnerInfo OwnerInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            OwnerInfo = await _context.OwnerInfo.FirstOrDefaultAsync(m => m.OwnerInfoId == id);

            if (OwnerInfo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {

            var owner = await _context.OwnerInfo.FindAsync(id);
            if (owner != null)
            {
                _context.OwnerInfo.Remove(owner);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
