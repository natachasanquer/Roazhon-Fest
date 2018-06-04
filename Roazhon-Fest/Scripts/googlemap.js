function myMap() {
    var pos;
    var rennes = { lat: 48.1172660, lng: -1.6777926 };
    var newMap = new google.maps.Map(document.getElementById("googleMap"), {
        zoom: 12,
        center: rennes
    });

    var marker = new google.maps.Marker({
        position: rennes,
        animation: google.maps.Animation.BOUNCE
    });
    marker.setMap(newMap);



    var infoWindow = new google.maps.InfoWindow({ map: newMap });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            infoWindow.setPosition(pos);
            infoWindow.setContent('Location found.');
            newMap.setCenter(pos);
            marker.setPosition(pos);
        }, function () {
            handleLocationError(true, infoWindow, newMap.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation

        pos = rennes;
        
        infoWindow.setContent('Location not found.');
        handleLocationError(false, infoWindow, newMap.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}




function initMap() {
    var directionsDisplay = new google.maps.DirectionsRenderer();
    var directionsService = new google.maps.DirectionsService();
    var map;

    var map = new google.maps.Map(document.getElementById('dirMap'), {
        zoom: 6,
        center: { lat: 41.85, lng: -87.65 }
    });
    directionsDisplay.setMap(map);


    document.getElementById('confirm').addEventListener('click', function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    });
}

function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var waypts = [];
    var checkboxArray = document.getElementById('waypoints');
    for (var i = 0; i < checkboxArray.length; i++) {
        if (checkboxArray.options[i].selected) {
            waypts.push({
                location: checkboxArray[i].value,
                stopover: true
            });
        }
    }

    directionsService.route({
        origin: document.getElementById('newStart').value,
        destination: document.getElementById('newEnd').value,
        waypoints: waypts,
        optimizeWaypoints: true,
        travelMode: 'DRIVING'
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
            var route = response.routes[0];
            var summaryPanel = document.getElementById('directions-panel');
            summaryPanel.innerHTML = '';
            // For each route, display summary information.
            for (var i = 0; i < route.legs.length; i++) {
                var routeSegment = i + 1;
                summaryPanel.innerHTML += '<b>Route Segment: ' + routeSegment +
                    '</b><br>';
                summaryPanel.innerHTML += route.legs[i].start_address + ' to ';
                summaryPanel.innerHTML += route.legs[i].end_address + '<br>';
                summaryPanel.innerHTML += route.legs[i].distance.text + '<br><br>';
            }
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
}