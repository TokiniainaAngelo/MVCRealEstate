using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Helpers;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.users
{
    public class EditModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public EditModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var redirectResult = SessionHelper.RedirectIfNotLoggedIn(this);
            if (redirectResult != null)
            {
                return redirectResult;
            }
            User = await _context.User.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userToUpdate = await _context.User.FindAsync(User.UserId);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = User.FirstName;
            userToUpdate.LastName = User.LastName;
            userToUpdate.FullName = User.FirstName + ' ' + User.LastName; 
            userToUpdate.Login = User.Login;
            userToUpdate.Password = User.Password;
            userToUpdate.Type = User.Type;
            userToUpdate.Email = User.Email;

            _context.Attach(userToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
