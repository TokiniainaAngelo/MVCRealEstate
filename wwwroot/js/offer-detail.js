function setModalContent(period, booked) {
    var details = "Do you want to book the appointment on " + period + "?";
    document.getElementById('appointmentDetails').innerHTML = details;

    // Set up the confirm button action
    var confirmButton = document.getElementById('confirmButton');
    confirmButton.onclick = function () {
        // Add your booking logic here
        console.log("Appointment booked on " + period);
        // Close the modal
        $('#appointmentModal').modal('hide');
    };
}
