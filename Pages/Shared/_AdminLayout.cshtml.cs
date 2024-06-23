using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MVCRealEstate.Pages.Shared
{
    public class _AdminLayoutModel : PageModel
    {
		public String UserLogin="";
        public void OnGet()
        {
			UserLogin = HttpContext.Session.GetString("UserLogin");

		}

		public IActionResult OnPostLogout()
		{
			HttpContext.Session.Clear();

			return RedirectToPage("/Login");
		}
	}
}
