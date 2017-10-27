"use strict";
// HELPERS ///
function getPopularMovies() {
    $.ajax(
        {
            url: "/Helper/GetPopularMovies",
            success: function (data) {
                return data;       
            },
            error: function () {
                console.log("Error!!!")
            }
        }
    )
}

function getMovie(movie_idapi) {
    $.ajax({
        url: "/Helper/GetMovie?movie_idapi=" + movie_idapi,
        success: function (data) {
        }
    })
}

// HOMEPAGE //
function heroCarousel() {
    $.ajax({
        url: "/Helper/GetPopularMovies",
        success: function (data) {
            let top3 = data.results.slice(0,3);
            let titles = $(".movie-title")
            let imgs = $(".img-fluid");
            let imgURL = "https://image.tmdb.org/t/p/w1280"
            for (var i = 0; i < top3.length; i++) {
                imgs[i].setAttribute("src", imgURL + top3[i].backdrop_path);
                titles[i].innerHTML = top3[i].original_title;
            }
        }
    })
}

function currentPopular() {
    $.ajax({
        url: "/Helper/GetPopularMovies",
        success: function (data) {
            let popularMovies = data.results.slice(3, 6);
            console.log(popularMovies);
            let titles = $(".poptitle")
            let imgs = $(".popimg");
            let overviews = $(".poptext");
            console.log(imgs);
            let imgURL = "https://image.tmdb.org/t/p/w342"
            for (var i = 0; i < popularMovies.length; i++) {
                imgs[i].setAttribute("src", imgURL + popularMovies[i].poster_path);
                titles[i].innerHTML = popularMovies[i].original_title;
                if (popularMovies[i].overview.length > 90) {
                    overviews[i].innerHTML = popularMovies[i].overview.slice(0, 90) +"  . . .";
                    continue;
                }
                overviews[i].innerHTML = popularMovies[i].overview;
            }
        }
    })
}

function upcomingMovies() {
    $.ajax({
        url: "/Helper/GetUpcomingMovies",
        success: function (data) {
            let upcomingMovies = data.results.slice(0,3);
            console.log(upcomingMovies);
            let titles = $(".uptitle")
            let imgs = $(".upimg");
            let overviews = $(".uptext");
            console.log(imgs);
            let imgURL = "https://image.tmdb.org/t/p/w342"
            for (var i = 0; i < upcomingMovies.length; i++) {
                imgs[i].setAttribute("src", imgURL + upcomingMovies[i].poster_path);
                titles[i].innerHTML = upcomingMovies[i].original_title;
                if (upcomingMovies[i].overview.length > 90) {
                    overviews[i].innerHTML = upcomingMovies[i].overview.slice(0, 90) + "  . . .";
                    continue;
                }
                overviews[i].innerHTML = upcomingMovies[i].overview;
            }
        }
    })
}

//DISPLAY A SINGLE MOVIE//

function heroMovie(movie_idapi) {
    $.ajax({
        url: "/Helper/GetMovie?movie_idapi=" + movie_idapi,
        success: function (data) {
            let imgURLPoster = "https://image.tmdb.org/t/p/w500" + data.poster_path
            let imgURLBackdrop = "https://image.tmdb.org/t/p/w1280" + data.backdrop_path
            let poster = $(".hero-poster");
            let title = $(".hero-title");
            let director = $(".hero-director");
            let overview = $(".overview");
            let mainCast = $(".hero-cast");
            let tempCast = "";
            let crew = data.credits.crew
            let mainCastAPI = data.credits.cast.slice(0, 5);
            let background = $(".heroSection");
            for (let i = 0; i < crew.length; i++) {
                if (crew[i].job == "Director") {
                    director.text(crew[i].name);
                    break;
                }
            }

            for (let j = 0; j < mainCastAPI.length; j++) {
                 tempCast += mainCastAPI[j].name + ", ";
            }

            // Set background image
            background.css("background", `url(${imgURLBackdrop}) no-repeat`); 
            background.css("background-size", "cover");
            background.css("background-attachment", "fixed");

            poster.attr("src", imgURLPoster);
            title.text(data.original_title);
            mainCast.text(tempCast.slice(0,tempCast.length-2) + ".");
            overview.text(data.overview);
        }
    })
}


function castSection(movie_idapi) {
    $.ajax({
        url: "/Helper/GetMovie?movie_idapi=" + movie_idapi,
        success: function (data) {
            let profileImgs = $(".profile-img");
            let actorNames = $(".actor-name");
            let charNames = $(".char-name");
            let porfileImgURL = "https://image.tmdb.org/t/p/w1280";
            let mainCastAPI = data.credits.cast.slice(0, 5);
            let crew = data.crew;
  
            for (let i = 0; i < mainCastAPI.length; i++) {
                profileImgs[i].setAttribute("src", porfileImgURL + mainCastAPI[i].profile_path);
                actorNames[i].innerHTML = mainCastAPI[i].name;
                charNames[i].innerHTML = mainCastAPI[i].character;
            }
        }
    })
}



//function getCredits(movie_idapi) {
//    $.ajax({
//        url: "/Helper/GetCredits?movie_idapi=" + movie_idapi,
//        success: function (data) {
//            console.log(data);]
//            let crew = data.crew;
//            let director = $(".hero-director"); 

//            for (var i = 0; i < crew.length; i++) {
//                if()
//            }
//            director.text(data.crew);
//        }
//    })
//}


function setRating(e) {
    console.log(e);
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");
    let rating = $(e).data("rating");
    console.log(user_id, movie_idapi, rating);
     $.ajax({
        url: `/Helper/SetRating?user_id=${user_id}&movie_id=${movie_idapi}&rating=${rating}`
     })   
}

function setDescription(e) {
    console.log(e);
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");
    let description = $(e).data("description");

    $.ajax({
        url: `/Helper/SetDescription?user_id=${user_id}&movie_id=${movie_idapi}&description=${description}`
    })
}
/* 
        QUEUE TABLE FUNCTIONS
                                */


function addToWatchlist(e) {
    console.log(e);

    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");

    $.ajax({
        url: `/Helper/AddToWatchlist?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}

function addToFavorite(e) {
    console.log(e);

    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");
    console.log(user_id);
    console.log(movie_idapi);

    $.ajax({
        url: `/Helper/AddToFavorite?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}

function addToWatched(e) {
    console.log(e);

    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");

    $.ajax({
        url: `/Helper/AddToWatched?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}

function addToNotify(e) {
    console.log(e);

    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");

    $.ajax({
        url: `/Helper/AddToNotify?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}


/* 
        END QUEUE TABLE FUNCTIONS
                                      */

$(document).ready(function () {
    
    //displayRating();
    //carouselHome();

   
    //
    // Homepage
    //
   
    if ($(".hero-carousel").length) {
        heroCarousel();
    }
    if ($(".popular-list").length) {
        currentPopular();
    }

    if ($(".upcoming-list").length) {
        upcomingMovies();
    }

    //
    //  End Homepage
    //

    //
    //  Single Movie
    //
     if ($('.heroSection').length) {
         heroMovie(346364);
         castSection(346364);
    }
    // prevents a tags from redirecting
    $("a.no_click").click(function (e) {
        e.preventDefault();
    });

   
    //
    //  End Index.html
    //

    $('[data-toggle="popover"]').popover();
    $('[data-toggle="tooltip"]').tooltip()
}); //end .ready