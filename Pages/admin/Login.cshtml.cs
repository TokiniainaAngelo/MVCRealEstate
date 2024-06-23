using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;

namespace MVCRealEstate.Pages.admin
{
    public class LoginModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public LoginModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Login { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserLogin") != null && HttpContext.Session.GetString("UserRole") == "Admin")
            {
                return RedirectToPage("/admin/Offers");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if(HttpContext.Session.GetString("UserRole") != null)
            {
                return RedirectToPage("/admin/Offers");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(Login == null || Password == null) {
                ErrorMessage = "Veuillez remplire tout les champs";
                return Page();
            }

            var user = await _context.User
                        .FirstOrDefaultAsync(u => u.Login == Login && u.Password == Password && u.Type == "admin");

            if (user == null)
            {
                ErrorMessage = "Login / mot de passe incorrecte";
                return Page();
            }

            // Set session variables
            HttpContext.Session.SetString("UserLogin", user.Login);
            HttpContext.Session.SetString("UserRole", user.Type);
            HttpContext.Session.SetString("UserId", user.UserId.ToString());

            return RedirectToPage("/admin/Offers/Index");
        }
    }
}
