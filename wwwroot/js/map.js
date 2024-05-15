document.addEventListener("DOMContentLoaded", function () {
    var latitude = parseFloat(document.getElementById('latitude').value);
    var longitude = parseFloat(document.getElementById('longitude').value);
    var address = parseFloat(document.getElementById('address').value);

    var map = L.map('map').setView([latitude, longitude], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    var marker = L.marker([latitude, longitude]).addTo(map)
        .bindPopup('Coordonnees: ' + latitude + '<br> Longitude: ' + longitude)
        .openPopup();
});
