using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace MVCRealEstate.Helpers
{
    public class SessionHelper
    {
        public static IActionResult RedirectIfNotLoggedIn(PageModel pageModel)
        {
            var username = pageModel.HttpContext.Session.GetString("UserLogin");
            var userType = pageModel.HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(username) && userType !="Admin")
            {
                return pageModel.RedirectToPage("/admin/Login");
            }

            return null;
        }
    }
}
