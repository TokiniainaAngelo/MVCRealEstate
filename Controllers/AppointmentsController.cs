﻿using System;
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

			var userId = HttpContext.Session.GetString("UserId");
			var login = HttpContext.Session.GetString("Login");

			// Use the session values as needed
			ViewData["UserId"] = userId;
			ViewData["Login"] = login;

			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("Login", "Users");
			}

			var userIdInt = int.Parse(userId);

			var mVCRealEstateContext = _context.Appointment
				.Include(a => a.Offer)
					.ThenInclude(o => o.Agency)
				.Include(a => a.Offer)
					.ThenInclude(o => o.OwnerInfo)
				.Include(a => a.Offer)
					.ThenInclude(o => o.Location)
				.Include(a => a.User)
				.Where(a => a.User.UserId == userIdInt);

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

                    if(user == null)
                    {
						return NotFound();
                    }

					if (existingAppointment == null)
					{
						return NotFound();
					}

					existingAppointment.UserId = Int32.Parse(user);
					existingAppointment.IsBooked = !existingAppointment.IsBooked;

					_context.Update(existingAppointment);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException e)
				{
					return NotFound();
				}
				return RedirectToAction(nameof(Index));
			}

			// If the model state is not valid, you might want to populate any necessary ViewData or SelectList items here.

			return RedirectToAction("Details", "Offers");
		}

		// POST: Appointments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPatch]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CancelAppointment(int id)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Retrieve the existing appointment from the database
					var existingAppointment = await _context.Appointment.FindAsync(id);

					if (existingAppointment == null)
					{
						return NotFound();
					}

					// Update appointment properties
					existingAppointment.UserId = 1;
					existingAppointment.IsBooked = false; 

					_context.Update(existingAppointment);
					await _context.SaveChangesAsync();

					return RedirectToAction("Index", "Appointments");
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}
			}

			return RedirectToAction("Index", "Appointments");
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
