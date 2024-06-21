using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCRealEstate.Data;
using MVCRealEstate.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCRealEstate.Pages.admin.owners
{
    public class EditModel : PageModel
    {
		private readonly MVCRealEstateContext _context;

		public EditModel(MVCRealEstateContext context)
		{
			_context = context;
		}

		[BindProperty]
		public OwnerInfo OwnerInfo { get; set; }

        [BindProperty]
        public string PhonesAsString { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
		{
            OwnerInfo = await _context.OwnerInfo.FindAsync(id);

            if (OwnerInfo == null)
            {
                return NotFound();
            }

            PhonesAsString = string.Join(", ", OwnerInfo.Phones);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var ownerToUpdate = await _context.OwnerInfo.FindAsync(OwnerInfo.OwnerInfoId);

            if (ownerToUpdate == null)
            {
                return NotFound();
            }

            ownerToUpdate.FirstName = OwnerInfo.FirstName;
            ownerToUpdate.LastName = OwnerInfo.LastName;
            ownerToUpdate.FullName = OwnerInfo.FirstName+' '+OwnerInfo.LastName;
            ownerToUpdate.Email = OwnerInfo.Email;
            ownerToUpdate.Address = OwnerInfo.Address;
            if (!string.IsNullOrEmpty(PhonesAsString))
            {
                ownerToUpdate.Phones = PhonesAsString.Split(',').Select(p => p.Trim()).ToList();
            }
            else
            {
                ownerToUpdate.Phones = new List<string>();
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
