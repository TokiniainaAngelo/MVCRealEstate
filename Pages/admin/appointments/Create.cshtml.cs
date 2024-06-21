using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.appointments
{
    public class CreateModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public CreateModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public IList<Offer> Offers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Offers = await _context.Offer.ToListAsync();
            Appointment = new Appointment
            {
                IsBooked = false,
                UserId = '1'
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            Console.WriteLine(Appointment.ToString());
            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
