// create a variable to store the parkings 'database' in
var parkings;

// use fetch to retrieve it, and report any errors that occur in the fetch operation
// once the parkings have been successfully loaded and formatted as a JSON object
// using response.json(), run the initialize() function
// au chargement de la paage, on lance le fetch qui va récupérer les données
window.onload = function () {
    fetch('http://data.citedia.com/r1/parks/').then(function (response) {
        if (response.ok) {
            console.log(response);
            // on récupère les données au format texte
            response.text().then(function (text) {
                console.log(text);
                // on parse les données en json et on les affecte à la variable parkings
                parkings = JSON.parse(text);
                initialize();
            })
        } else {
            console.log('Network request for parkings.json failed with response ' + response.status + ': ' + response.statusText);
        }
    });
}

function ajouterLigne() {

    var tableau = document.getElementById("PTable");

    // à partir de la variable parkings, on va chercher la longueur des parks
    for (var i = 0; i < parkings.parks.length; i++) {

        var newRow = $("<tr>");
        var cols = "";
        // à partir de la variable parking, on va chercker les parks avec leur id et on affecte
        // leur nom à la colonne 1, puis leur nb de places max à la colonne 2
        cols += '<td>' + parkings.parks[i].parkInformation.name + '</td>';
        cols += '<td>' + parkings.parks[i].parkInformation.max + '</td>';
        //cols += '<td><button>Itinéraire</button></td>';
        /*cols += '<td><button class="btn" onclick="changeMapsView(' +
            parking.coordinates.lat +
            ',' +
            parking.coordinates.lng +
            ' )">trajet</button></td>';
        */
        // dans le bouton trajet, on passe la latitude et la longitude du parking
        /*cols += '<td><button class="btn" onclick="changeMapsView(' +
            parking.coordinates.lat +
            ',' +
            parking.coordinates.lng +
            ' )">trajet</button></td>';*/
        // on crée la nouvelle ligne et on l'ajoute au tableau
        newRow.append(cols);
        $("table").append(newRow);
    }
}

// sets up the app logic, declares required variables, contains all the other functions
function initialize() {
    // grab the UI elements that we need to manipulate
    var name = document.querySelector('#name');
    console.log(name);
    var nbPlaces = document.querySelector('#max');
    var itineraryBtn = document.querySelector('button');
    //var main = document.querySelector('main');

    // keep a record of what the last name and nbPlaces entered were
    var lastName = name.value;
    // no search has been made yet
    var lastNbPlaces = nbPlaces.value;

    // these contain the results of filtering by name, and nbPlaces
    // finalGroup will contain the parkings that need to be displayed after
    // the searching has been done. Each will be an array containing objects.
    // Each object will represent a parking
    var nameGroup;
    var finalGroup;

    // To start with, set finalGroup to equal the entire parkings database
    // then run updateDisplay(), so ALL parkings are displayed initially.
    finalGroup = parkings;
    ajouterLigne();
    //updateDisplay();

    // Set both to equal an empty array, in time for searches to be run
    nameGroup = [];
    finalGroup = [];

    // when the itineraryBtn is clicked, invoke selectName() to start
    // a search running to select the name of parkings we want to display
    itineraryBtn.onclick = selectName;

    function selectName(e) {
        // Use preventDefault() to stop the form submitting — that would ruin
        // the experience
        e.preventDefault();

        // Set these back to empty arrays, to clear out the previous search
        nameGroup = [];
        finalGroup = [];

        // if the name and nbPlaces are the same as they were the last time a
        // search was run, the results will be the same, so there is no point running
        // it again — just return out of the function
        if (name.value === lastName && max.value === lastNbPlaces) {
            return;
        } else {
            // update the record of last name and nbPlaces
            lastName = name.value;
            lastNbPlaces = max.value;
            // In this case we want to select all parkings, then filter them by the search
            // term, so we just set nameGroup to the entire JSON object, then run selectParkings()
            if (name.value === 'All') {
                nameGroup = parkings;
                selectParkings();
                // If a specific name is chosen, we need to filter out the parkings not in that
                // name, then put the remaining parkings inside nameGroup, before running
                // selectParkings()
            } else {
                // the values in the <option> elements are uppercase, whereas the categories
                // store in the JSON (under "type") are lowercase. We therefore need to convert
                // to lower case before we do a comparison
                var lowerCaseType = name.value.toLowerCase();
                for (var i = 0; i < parkings.length; i++) {
                    // If a parking's type property is the same as the chosen name, we want to
                    // dispay it, so we push it onto the nameGroup array
                    if (parkings[i].type === lowerCaseType) {
                        nameGroup.push(parkings[i]);
                    }
                }

                // Run selectParkings() after the filtering has bene done
                selectParkings();
            }
        }
    }
/*
    // selectParkings() Takes the group of parkings selected by selectName(), and further
    // filters them by the tnered nbPlaces (if one has bene entered)
    function selectParkings() {
        // If no nbPlaces has been entered, just make the finalGroup array equal to the nameGroup
        // array — we don't want to filter the parkings further — then run updateDisplay().
        if (max.value === '') {
            finalGroup = nameGroup;
            updateDisplay();
        } else {
            // Make sure the nbPlaces is converted to lower case before comparison. We've kept the
            // parking names all lower case to keep things simple
            var lowerCaseMax = max.value.trim().toLowerCase();
            // For each parking in nameGroup, see if the nbPlaces is contained inside the parking name
            // (if the indexOf() result doesn't return -1, it means it is) — if it is, then push the parking
            // onto the finalGroup array
            for (var i = 0; i < nameGroup.length; i++) {
                if (nameGroup[i].name.indexOf(lowerCaseMax) !== -1) {
                    finalGroup.push(nameGroup[i]);
                }
            }

            // run updateDisplay() after this second round of filtering has been done
            updateDisplay();
        }

    }
*/
    /*
    // start the process of updating the display with the new set of parkings
    function updateDisplay() {
        // remove the previous contents of the <main> element
        while (main.firstChild) {
            main.removeChild(main.firstChild);
        }

        // if no parkings match the nbPlaces, display a "No results to display" message
        if (finalGroup.length === 0) {
            var para = document.createElement('p');
            para.textContent = 'No results to display!';
            main.appendChild(para);
            // for each parking we want to display, pass its parking object to fetchBlob()
        } else {
            for (var i = 0; i < finalGroup.length; i++) {
                fetchBlob(finalGroup[i]);
            }
        }
    }*/
/*
    // fetchBlob uses fetch to retrieve the image for that parking, and then sends the
    // resulting image display URL and parking object on to showParking() to finally
    // display it
    function fetchBlob(parking) {
        // construct the URL path to the image file from the parking.image property
        var url = 'images/' + parking.image;
        // Use fetch to fetch the image, and convert the resulting response to a blob
        // Again, if any errors occur we report them in the console.
        fetch(url).then(function (response) {
            if (response.ok) {
                response.blob().then(function (blob) {
                    // Convert the blob to an object URL — this is basically an temporary internal URL
                    // that points to an object stored inside the browser
                    objectURL = URL.createObjectURL(blob);
                    // invoke showParking
                    showParking(objectURL, parking);
                });
            } else {
                console.log('Network request for "' + parking.name + '" image failed with response ' + response.status + ': ' + response.statusText);
            }
        });
    }
    */
    // Display a parking inside the <main> element
    function showParking(objectURL, parking) {
        // create <section>, <h2>, <p>, and <img> elements
        //var section = document.createElement('section');
        var heading = document.createElement('h2');
        var para = document.createElement('p');
        //var image = document.createElement('img');
        /*
        // give the <section> a classname equal to the parking "type" property so it will display the correct icon
        section.setAttribute('class', parking.type);
        */

        // Give the <h2> textContent equal to the parking "name" property, but with the first character
        // replaced with the uppercase version of the first character
        heading.textContent = parking.name.replace(parking.name.charAt(0), parking.name.charAt(0).toUpperCase());

        // Give the <p> textContent equal to the parking nbPlaces property
        para.textContent = parking.nbPlaces;

        /*
        // Set the src of the <img> element to the ObjectURL, and the alt to the parking "name" property
        image.src = objectURL;
        image.alt = parking.name;
        */

        // append the elements to the DOM as appropriate, to add the parking to the UI
        main.appendChild(section);
        section.appendChild(heading);
        section.appendChild(para);
        //section.appendChild(image);
    }
}

// fonction qui change la carte avec l'itinéraire vers le parking sélectionné
function changeMapsView(lat, lng) {
    document.getElementById("googleMap").src =
        'https://www.google.com/maps/embed/v1/directions?origin=rennes&destination=' +
        lat +
        ',' +
        lng +
        '&key=AIzaSyBG1YsUa6hRRT6uY-ZX8gJXuxWwm-Iwx3A';
}