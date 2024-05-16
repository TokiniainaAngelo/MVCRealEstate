using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;
using Microsoft.AspNetCore.Http;


namespace MVCRealEstate.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVCRealEstateContext _context;


        public UsersController(MVCRealEstateContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Login,Password,Email")] User user)
        {
            // Set FullName and Type here
            user.FullName = user.FirstName + " " + user.LastName;
            user.Type = UserType.Client.ToString();

          
                var get_user = _context.User.FirstOrDefault(p => p.Login == user.Login);
                if (get_user == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                     return RedirectToAction("Login", "Users"); // Redirect to login
                }
                else
                {
                    ViewBag.Message = "UserName already exists";
                    return View(user);
                }
           
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
                var get_user = _context.User.Single(p => p.Login == user.Login
                && p.Password == user.Password);
                if (get_user != null)
                {
                HttpContext.Session.SetString("UserId", get_user.UserId.ToString());
                HttpContext.Session.SetString("Login", get_user.Login.ToString());

                return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Login ou mot de passe incorrecte");
                }

			return View();
        }
		public IActionResult Logout()
		{
			// Clear the session
			HttpContext.Session.Clear();

			// Redirect to the home page
			return RedirectToAction("Index", "Home");
		}


		// GET: Users/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,FullName,Login,Password,Type")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }

       
    }

}
