$(document).ready(function () {
	$('#confirmButton').click(function () {
		var appointmentId = $(this).data('appointment-id'); // Get the appointment ID from the button's data attribute

		$.ajax({
			url: '/AppointmentsController/BookAppointment/' + appointmentId, // Update the URL to your controller's route
			type: 'PATCH',
			headers: {
				'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
			},
			success: function (response) {
				// Handle success, perhaps close the modal and show a success message
				$('#appointmentModal').modal('hide');
				alert('Appointment booked successfully');
			},
			error: function (xhr, status, error) {
				// Handle error
				alert('An error occurred while booking the appointment');
			}
		});
	});
});