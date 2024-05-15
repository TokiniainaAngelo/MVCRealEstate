document.addEventListener("DOMContentLoaded", function () {
    var map = L.map('map').setView([51.505, -0.09], 13); // Set initial view with default coordinates

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    var marker = L.marker([51.505, -0.09]).addTo(map)
        .bindPopup('A pretty CSS3 popup.<br> Easily customizable.')
        .openPopup();
});

function updateMap(lat, lng) {
    map.setView([lat, lng], 13);
    marker.setLatLng([lat, lng]).update();
}