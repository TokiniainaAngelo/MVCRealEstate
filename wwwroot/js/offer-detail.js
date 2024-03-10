$(document).ready(function () {
	function getAntiXsrfRequestToken() {
		return '@GetAntiXsrfRequestToken()';
	}

	const token = getAntiXsrfRequestToken()
	$('.appointment').click(function () {
		// Remove selected class from all appointments
		$('.appointment').removeClass('selected');

		// Add selected class to the clicked appointment
		$(this).addClass('selected');

		// Show the book button
		$('#bookButton').show();
	});

	$('#bookButton').click(function () {
		// Display the confirmation modal
		$('#confirmationModal').show();
	});

	$('#confirmBooking').click(function () {
		// Get the selected appointment id
		var selectedAppointmentId = $('.appointment.selected').data('id');
		var offer = $('.appointment.selected').data('offer');

		confirmBooking(selectedAppointmentId);
		downloadPdf(selectedAppointmentId);
	});

	$('#cancelBooking').click(function () {
		// Close the confirmation modal
		$('#confirmationModal').hide();
	});

	// Function to handle the confirmation button click
	function confirmBooking(appointmentId) {
		// Make an AJAX request to the controller action
		$.ajax({
			url: "/Appointments/BookAppointment/" + appointmentId,
			type: 'PATCH',
			contentType: 'application/json; charset=utf-8',
			headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val(), "RequestVerificationToken": token },
			success: function (data) {
				// Handle the success response
				console.log('Appointment booked successfully');
				// Optionally, update the element or perform other actions
			},
			error: function (error) {
				// Handle the error response
				console.error('Error booking appointment: ', error);
			}
		});
	}

	function downloadPdf(appointmentId) {
		// Make an asynchronous request to the server
		$.ajax({
			url: "/Appointments/GeneratePdfTicket",
			type: "POST",
			headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val(), "RequestVerificationToken": token },
			data: { appointmentId: appointmentId },
			success: function (data) {
				console.log(Buffer.from(data);
				var blob = new Blob([data], { type: "application/pdf" });

				// Create a link element and trigger a click to initiate download
				var link = document.createElement("a");
				link.href = window.URL.createObjectURL(blob);
				link.download = "rendez-vous.pdf";
				document.body.appendChild(link);
				link.click();
				document.body.removeChild(link);
			},
			error: function (error) {
				console.error("Error generating PDF:", error);
			}
		});
	}
});