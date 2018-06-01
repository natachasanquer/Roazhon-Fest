function myMap() {
    var rennes = { lat: 48.1172660, lng: -1.6777926 };
    var newMap = new google.maps.Map(document.getElementById("googleMap"), {
        zoom: 12,
        center: rennes
    });
    
    var marker = new google.maps.Marker({
        position: rennes,
        map: newMap
    });
}

/*
function myMap() {
    var mapProp = {
        center: new google.maps.LatLng(48.1172660, -1.6777926),
        zoom: 12,
    };
    var newMap = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    var marker = new google.maps.Marker({
        position: mapProp,
        map: newMap
    });
}*/

function initMap() {
    var uluru = { lat: -25.363, lng: 131.044 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 4,
        center: uluru
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}