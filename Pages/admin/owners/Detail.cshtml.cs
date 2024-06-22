using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Helpers;
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
        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var redirectResult = SessionHelper.RedirectIfNotLoggedIn(this);
            if (redirectResult != null)
            {
                return redirectResult;
            }
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
            if (owner == null)
            {
                return NotFound();
            }

            var offersWithOwner = await _context.Offer.AnyAsync(o => o.OwnerInfoId == id);
            if (offersWithOwner)
            {
                ErrorMessage = "Le propriétaire ne peut pas être supprimé car des offres sont associées à lui.";
                return RedirectToPage(new { id = id });
            }

            _context.OwnerInfo.Remove(owner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
