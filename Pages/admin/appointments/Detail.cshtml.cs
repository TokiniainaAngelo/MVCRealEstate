using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

namespace MVCRealEstate.Pages.admin.appointments
{
    public class DetailModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public DetailModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Appointment = await _context.Appointment
                .Include(a => a.User)
                .Include(a => a.Offer)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (Appointment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
