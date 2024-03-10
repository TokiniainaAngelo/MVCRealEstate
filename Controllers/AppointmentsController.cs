using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Migrations;
using MVCRealEstate.Models;


namespace MVCRealEstate.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly MVCRealEstateContext _context;

        public AppointmentsController(MVCRealEstateContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var mVCRealEstateContext = _context.Appointment.Include(a => a.Offer).Include(a => a.User);
            return View(await mVCRealEstateContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Offer)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["OfferId"] = new SelectList(_context.Offer, "OfferId", "Description");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,UserId,OfferId,Period,IsBooked")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OfferId"] = new SelectList(_context.Offer, "OfferId", "Description", appointment.OfferId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["OfferId"] = new SelectList(_context.Offer, "OfferId", "Description", appointment.OfferId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,UserId,OfferId,Period,IsBooked")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["OfferId"] = new SelectList(_context.Offer, "OfferId", "Description", appointment.OfferId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Offer)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

		// POST: Appointments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPatch]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> BookAppointment(int id)
		{


			if (ModelState.IsValid)
			{
				try
				{
					// Retrieve the existing appointment from the database
					var existingAppointment = await _context.Appointment.FindAsync(id);
					var user = HttpContext.Session.GetString("UserId");

					if (existingAppointment == null)
					{
						return NotFound();
					}

					existingAppointment.UserId = Int32.Parse(user);
					existingAppointment.IsBooked = true;

					_context.Update(existingAppointment);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}
				return RedirectToAction(nameof(Index));
			}

			// If the model state is not valid, you might want to populate any necessary ViewData or SelectList items here.

			return RedirectToAction("Details", "Offers");
		}
		[HttpPost]
		public async Task<FileContentResult> GeneratePdfTicket(int appointmentId)
		{
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));

			var appointment = await _context.Appointment.FindAsync(appointmentId);
            var user = await _context.User.FindAsync(userId);

			// Customize HTML content as per your requirements
			var htmlContent = $@"
    <h1>Appointment Ticket</h1>
    <p>Appointment ID: {appointment.AppointmentId}</p>
    <p>User ID: {user.LastName}</p>
    <!-- Add more appointment and user information as needed -->
";

			var renderer = new ChromePdfRenderer();
			var pdf = renderer.RenderHtmlAsPdf(htmlContent);

			// Specify content type and file name for the response
			var contentType = "application/pdf";
			var fileName = "AppointmentTicket.pdf";

			// Return the PDF content as a FileContentResult
			return File(pdf.BinaryData, contentType, fileName); ;
		}

		
		private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }
    }
}
