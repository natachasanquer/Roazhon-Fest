var pos;
var rennes = { lat: 48.1172660, lng: -1.6777926 };
var direction;

function myMap() {
    var map = new google.maps.Map(document.getElementById("googleMap"), {
        zoom: 12,
        center: rennes
    });

    panel = document.getElementById("panel");

    var marker = new google.maps.Marker({
        position: rennes,
        animation: google.maps.Animation.BOUNCE
    });
    marker.setMap(map);

    var infoWindow = new google.maps.InfoWindow({ map: map });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            infoWindow.setPosition(pos);
            infoWindow.setContent('Location found.');
            map.setCenter(pos);
            marker.setPosition(pos);
            console.log(pos);
            document.getElementById("origin").value = pos.lat + ", " + pos.lng;
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        infoWindow.setContent('Location not found.');
        handleLocationError(false, infoWindow, map.getCenter());
    }

    direction = new google.maps.DirectionsRenderer({
        map: map,
        panel: panel // Dom element pour afficher les instructions d'itinéraire
    });

    //document.getElementById('confirm').addEventListener('click', function () {
      //  calculateAndDisplayRoute(directionsService, directionsDisplay);
    //});
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    pos = rennes;
    console.log(pos);
    document.getElementById("origin").value = pos.lat + ", " + pos.lng;
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}


function calculate () {
    origin = document.getElementById("origin").value; // Le point départ
    destination = document.getElementById("destination").value; // Le point d'arrivé
    if (origin && destination) {
        var request = {
            origin: origin,
            destination: destination,
            travelMode: google.maps.DirectionsTravelMode.DRIVING // Mode de conduite
        }
        var directionsService = new google.maps.DirectionsService(); // Service de calcul d'itinéraire
        directionsService.route(request, function (response, status) { // Envoie de la requête pour calculer le parcours
            if (status == google.maps.DirectionsStatus.OK) {
                direction.setDirections(response); // Trace l'itinéraire sur la carte et les différentes étapes du parcours
            }
        });
    }
};