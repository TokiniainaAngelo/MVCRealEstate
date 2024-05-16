const apointmentId = null;

function showModal(button) {
    appointmentId = $(button).data('appointment-id'); // Get the appointment ID from the button's data attribute
    console.log(appointmentId);
    $('#confirmButton').data('appointment-id', appointmentId); // Set the appointment ID on the confirm button
    $('#appointmentModal').modal('show'); // Show the modal
}

$(document).ready(function () {

    $('#confirmButton').click(function () {
        var appointmentId = $(this).data('appointment-id');

        $.ajax({
            url: '/Appointments/BookAppointment/' + appointmentId, 
            type: 'PATCH',
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
               
                $('#appointmentModal').modal('hide');
            },
            error: function (xhr, status, error) {
                
                console.log('An error occurred while booking the appointment',error);
            }
        });
    });
});