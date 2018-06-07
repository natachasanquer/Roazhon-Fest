//au moment du chargement de la page, on exécute cette fonction
window.onload = function () {
    let location;
    // on crée un objet XMLHttpRequest : objet JavaScript pour récupérer facilement des données via HTTP. En gros, c'est l'Ajax
    var xhr = new XMLHttpRequest();
    // La méthode open démarre la connexion, en mode lecture ou écriture, pour recevoir des données du serveur ou lui en envoyer
    xhr.open('get',
        "https://maps.googleapis.com/maps/api/geocode/json?address=nantes&key=AIzaSyBG1YsUa6hRRT6uY-ZX8gJXuxWwm-Iwx3A");
    xhr.send();
    xhr.onreadystatechange = function () { //Call a function when the state changes.
        //La connexion passe par plusieurs états successifs qui sont assignés à l'attribut readyState de l'objet.
        //Quand l'état final est atteint, les données peuvent être récupérées dans un autre attribut. Elles sont du texte pur ou un document XML. Le format JSON est aussi reconnu.
        // statut est à 4 si chargé
        if (xhr.readyState === 4 && xhr.status === 200) {
            location = JSON.parse(this.responseText).results[0].geometry.location;
            NearestCity(location.lat, location.lng);
        }
    }

}

// Convert Degrees to Radians
function Deg2Rad(deg) {
    return deg * Math.PI / 180;
}

// What ???
// lat 1 et lon 1 pour le point de départ
// on calcule la distance entre les deux points 1 et 2
function PythagorasEquirectangular(lat1, lon1, lat2, lon2) {
    lat1 = Deg2Rad(lat1);
    lat2 = Deg2Rad(lat2);
    lon1 = Deg2Rad(lon1);
    lon2 = Deg2Rad(lon2);
    var R = 6371; // rayon de la terre en km
    var x = (lon2 - lon1) * Math.cos((lat1 + lat2) / 2);
    var y = (lat2 - lat1);
    var d = Math.sqrt(x * x + y * y) * R;
    return d;
}

var lat = 20; // user's latitude
var lon = 40; // user's longitude

// fonction pour trouver les parking les plus proches
function NearestCity(latitude, longitude) {
    let parks = [];
    let parkings = [];
    // on appelle la fonction VanillaRequests avec laquelle on chaque la liste des parkings de l'api de Rennes
    VanillaRequests('get', 'http://data.citedia.com/r1/parks', {}, {}).then((responseText) => {
        JSON.parse(responseText).parks.forEach((park) => {
            // parkInformation est une classe du json et free un attribut de cette classe
            console.log('park.parkInformation.free', park.parkInformation.free)
            if (park.parkInformation.free > 10) {
                parks.push(park);
            }
        })
        console.log('parks', parks);
        // on boucle sur la liste de parkings
        for (let i = 0; i < parks.length; i++) {
            VanillaRequests('get',
                "https://maps.googleapis.com/maps/api/geocode/json?address=parking%20" +
                parks[i].parkInformation.name +
                ", rennes&key=AIzaSyBG1YsUa6hRRT6uY-ZX8gJXuxWwm-Iwx3A",
                {},
                {}).then((response) => {
                    console.log('response', parks);
                    parkings.push({
                        'id': parks[i].id,
                        'coordinates': JSON.parse(response).results[0].geometry.location,
                        'places': parks[i].parkInformation.free
                    });
                    if (parkings.length === parks.length) {
                        parkings = determinateClosestParkings(latitude, longitude, parkings);
                        parkings.sort(function (a, b) {
                            return a.dif - b.dif;
                        });
                        console.log('parkings', parkings);
                        parkings.forEach((parking) => {
                            insRow(parking);
                        });
                        document.getElementById("maps").src =
                            'https://www.google.com/maps/embed/v1/directions?origin=rennes&destination=' +
                            parkings[0].coordinates.lat +
                            ',' +
                            parkings[0].coordinates.lng +
                            '&key=AIzaSyBG1YsUa6hRRT6uY-ZX8gJXuxWwm-Iwx3A'
                    }
                });
        }
    })
}

// fonction qui ???
function changeMapsView(lat, lng) {
    document.getElementById("maps").src =
        'https://www.google.com/maps/embed/v1/directions?origin=rennes&destination=' +
        lat +
        ',' +
        lng +
        '&key=AIzaSyBG1YsUa6hRRT6uY-ZX8gJXuxWwm-Iwx3A';
}

// fonction pour insérer une ligne dans le tableau, à chaque nouveau parking
function insRow(parking) {
    var newRow = $("<tr>");
    var cols = "";
    cols += '<td>' + parking.id + '</td>';
    cols += '<td>' + parking.places + '</td>';
    // dans le bouton trajet, on passe la latitude et la longitude du parking
    cols += '<td><button class="btn" onclick="changeMapsView(' +
        parking.coordinates.lat +
        ',' +
        parking.coordinates.lng +
        ' )">trajet</button></td>';
    // on crée la nouvelle ligne et on l'ajoute au tableau
    newRow.append(cols);
    $("table").append(newRow);
}

// fonction qui détermine les parkings les plus proches du lieu de l'événement en passant la latitude et la longitude du lieu, et tous les parkings
function determinateClosestParkings(latitude, longitude, parkings) {
    // pour chaque parking on appelle la fonction PythagorasEquirectangular afin de passer ses coordonnées sur un plan
    parkings.forEach((parking) => {
        var dif = PythagorasEquirectangular(latitude, longitude, parking.coordinates.lat, parking.coordinates.lng);
        // on crée un tableau de parkings
        parking['dif'] = dif;
    });
    return parkings;
}

// fonction pour aller chercher les url
// method définit si la méthode est en get ou post
// adresse est le point de départ
// arguments ?
// parameters ?
function VanillaRequests(method, adress, arguments, parameters) {
    return new Promise((resolve, reject) => {
        var xhr = new XMLHttpRequest();
        if (method === 'GET' && arguments !== {}) {
            Object.keys(arguments).forEach((key, index) => {
                if (index !== (arguments.length - 1)) {
                    adress = adress + '?' + key + '=' + arguments(key) + '&';
                } else {
                    adress = adress + '?' + key + '=' + arguments(key);
                }
            });
            xhr.open(method, adress);
            xhr.send();
        } else if (method === 'POST' && parameters !== {}) {
            xhr.open(method, adress);
            xhr.send(parameters);
        } else {
            xhr.open(method, adress);
            xhr.send();
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                resolve(xhr.responseText);
            }
        }
    })
}