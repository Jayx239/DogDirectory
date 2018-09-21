/* The client side library for displaying the dog list and random dog images */

var DogListErrorMessage = "<h4>Uh oh!!! We can't find our list of dogs right now!</h4>";
var DogImageErrorMessage = "<h4>Uh oh!!! We can't find a picture of that dog right now!</h4>";

function FormatDogListOutput(data) {
    var output = "<ul>";
    for (var breedIndex in data) {
        var breed = data[breedIndex]
        var breedName = breed.BreedName;
        output += "<li>" + "<a onclick=\"GetDogImage('name=" + breedName + "')\">" + breedName + "</a>" + "</li>";
        var variations = breed.Variations;
        for (var variationIndex in variations) {
            var variation = variations[variationIndex].Name;
            output += "<li>" + "<a onclick=\"GetDogImage('name=" + breedName + "%2F" + variation+ "')\">" + variation + " "+ breedName + "</a>" + "</li>";
        }
    }
    output += "</ul>";
    return output;
}

function GetDogImage(queryString) {
    $.ajax({
        url: "/home/API2GetDogImage/",
        data: queryString,
        method: "POST",
        success: function (data) {
            $("#ImageArea img").attr("src", data);
        },
        error: function () {
            $("#ImageArea #ErrorMessage").html(DogImageErrorMessage);
        }
    })
}

function LoadDogList() {
    $.ajax({
        url: "/home/API2GetDogList/",
        method: "GET",
        success: function (data) {
            $("#DogListArea").html(FormatDogListOutput(data));
        },
        error: function () {
            $("#DogListArea").html(DogListErrorMessage);
        }
    })
}

