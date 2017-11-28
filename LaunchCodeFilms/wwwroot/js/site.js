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

//function getMovie(movie_idapi) {
//    $.ajax({
//        url: "/Helper/GetMovie?movie_idapi=" + movie_idapi,
//        success: function (data) {
//        }
//    })
//}

function getPerson(person_id) {
    $.ajax({
        url: "/Helper/GetPerson?person_id=" + person_id,
        success: function (data) {
            console.log(data);
        }
    })
}
function search(searchTerm) {
    $.ajax({
        url: "/Helper/Search?searchTerm=" + searchTerm,
        success: function (data) {
            console.log(data);
            let movies = data.results;
            let links = $(".search-link");
            let imgs = $(".search-img");
            let titles = $(".search-name");
            let posterImgURL = "https://image.tmdb.org/t/p/w342";
           
            for (var i = 0; i < movies.length; i++) {
                //imgs[i].setAttribute("src", posterImgURL + movies[i].poster_path);
                //titles[i].innerHTML = movies[i].title;
                //links[i].setAttribute("href", "/Movie?id=" + movies[i].id);
                console.log(movies[i].id)
                $(".row-search").append(
                        `<div class="col-3 mb-3">
                            <a class="search-link" href="/Movie?id=${movies[i].id}">
                                <div class="card">
                                    <img class="card-img-top search-img" src="https://image.tmdb.org/t/p/w342${movies[i].poster_path}" alt="actor picture">
                                    <div class="card-body">
                                        <p class="card-title search-name text-center">${movies[i].title}</p>
                                        <p class = "card-subtitle"></p>
                                    </div>
                                </div>
                            </a>
                       </div>`
                );
            }
    
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

            let comment = $('.hero-movie');
            let movies = $('[data-id=""]'); 

            for (var i = 0; i < top3.length; i++) {
                imgs[i].setAttribute("src", imgURL + top3[i].backdrop_path);
                titles[i].innerHTML = top3[i].original_title;
                comment[i].setAttribute("href", "/Movie?id=" + top3[i].id);
                movies[i].setAttribute("data-id", top3[i].id);
            }
        }
    })
}

function currentPopular() {
    $.ajax({
        url: "/Helper/GetPopularMovies",
        success: function (data) {
            let popularMovies = data.results.slice(3, 6);
            let titles = $(".poptitle")
            let imgs = $(".popimg");
            let overviews = $(".poptext");
            let imgURL = "https://image.tmdb.org/t/p/w342"
            let movies = $(".pop");
            console.log(movies);
            console.log(popularMovies);

            for (var i = 0; i < popularMovies.length; i++) {
                imgs[i].setAttribute("src", imgURL + popularMovies[i].poster_path);
                titles[i].innerHTML = popularMovies[i].original_title;
                movies[i].setAttribute("href", "/Movie?id=" + popularMovies[i].id);
                console.log(movies[i]);
                if (popularMovies[i].overview.length > 90) {
                    overviews[i].innerHTML = popularMovies[i].overview.slice(0, 90) +"  . . .";
                    continue;
                }
                //overviews[i].innerHTML = popularMovies[i].overview;
            }
        }
    })
}

function upcomingMovies() {
    $.ajax({
        url: "/Helper/GetUpcomingMovies",
        success: function (data) {
            let upcomingMovies = data.results.slice(0,3);
            let titles = $(".uptitle")
            let imgs = $(".upimg");
            let overviews = $(".uptext");
            let imgURL = "https://image.tmdb.org/t/p/w342"
            let movies = $(".up");

            for (var i = 0; i < upcomingMovies.length; i++) {
                imgs[i].setAttribute("src", imgURL + upcomingMovies[i].poster_path);
                titles[i].innerHTML = upcomingMovies[i].original_title;
                movies[i].setAttribute("href", "/Movie?id=" + upcomingMovies[i].id);
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

function heroMovie(id) {
    $.ajax({
        url: "/Helper/GetMovie?movie_idapi=" + id,
        success: function (data) {
            console.log(data);
            let imgURLPoster = "https://image.tmdb.org/t/p/w500" + data.poster_path
            let imgURLBackdrop = "https://image.tmdb.org/t/p/w1280" + data.backdrop_path
            let poster = $(".hero-poster");
            let title = $(".hero-title");
            let director = $(".hero-director");
            let overview = $(".overview");
            let mainCast = $(".hero-cast");
            let tempCast = "";
            let crew = data.credits.crew;
            let mainCastAPI = data.credits.cast.slice(0, 5);
            let background = $(".heroSection");

            let comment = $('[href = "/Movie/Comments"]'); 
            comment.attr("href", `/Movie/Comments?id=${data.id}`);

            let review = $('[href = "/Movie/Reviews"]');
            review.attr("href", `/Movie/Reviews?id=${data.id}`);

            let data_moviesIDs = $('[data-id=""]');

            for (var k = 0; k < data_moviesIDs.length; k++) {
                data_moviesIDs[k].setAttribute("data-id", data.id);
            }

            for (let i = 0; i < crew.length; i++) {
                if (crew[i].job === "Director") {
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
            let actor_profile = $(".actor-profile");
            let porfileImgURL = "https://image.tmdb.org/t/p/w1280";
            let mainCastAPI = data.credits.cast.slice(0, 5);
            let crew = data.crew;
  
            for (let i = 0; i < mainCastAPI.length; i++) {
                profileImgs[i].setAttribute("src", porfileImgURL + mainCastAPI[i].profile_path);
                actorNames[i].innerHTML = mainCastAPI[i].name;
                charNames[i].innerHTML = mainCastAPI[i].character;
                actor_profile[i].setAttribute("href", "/Person?id=" + mainCastAPI[i].id);
            }
        }
    })
}

function similarSection(movie_idapi) {
    $.ajax({
        url: "/Helper/SimilarMovies?movie_idapi=" + movie_idapi,
        success: function (data) {

            let similarLinks = $(".similar-link");
            let simlarImgs = $(".similar-img");
            let similarNames = $(".similar-name");
            let posterImgURL = "https://image.tmdb.org/t/p/w342";
            let similar5 = data.results.slice(0, 5);

            for (let i = 0; i < similar5.length; i++) {
                simlarImgs[i].setAttribute("src", posterImgURL + similar5[i].poster_path);
                similarNames[i].innerHTML = similar5[i].title;
                similarLinks[i].setAttribute("href", "/Movie?id=" + similar5[i].id);
            }
        }
    })
}
//EDN DISPLAY A SINGLE MOVIE//

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

//Review Page//
function addReviewQuery(id) {
    let url = $('[href = "/Movie/AddReview"]');
    url.attr("href", `/Movie/AddReview?id=${id}`);
}

function getReviews(id) {
    $.ajax({
        url: "/Helper/GetReviews?movie_idapi=" + id,
        success: function (data) {
            let reviewContainer = $(".review-list");
            console.log(data);
            console.log(data.Description);
            if (data == 1) {
                reviewContainer.append(
                    `<div class="card">
                        <div class="card-body">
                            No reviews yet.
                        </div>
                    </div>`);
            } else {
                for (var i = 0; i < data.length; i++) {
                    reviewContainer.append(
                        `<div class="card p-2 mt-5 m-3"> 
                            <div class="card-body">
                                <h4 class="card-title">Reviewed by ${data[i].User.UserName}</h4>
                                <p class="card-text">${data[i].Description}</p>
                            </div>
                        </div>`);
                }
            }
        }
    })
}

function setRating(e) {
    let user_id = $(e).data("userid");
    let id = $(e).data("id");
    let rating = $(e).data("rating");
    console.log(id);
     $.ajax({
        url: `/Helper/SetRating?user_id=${user_id}&id=${id}&rating=${rating}`
     })   
}

function setDescription(e) {
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("id");
    let description = $(e).data("description");

    $.ajax({
        url: `/Helper/SetDescription?user_id=${user_id}&movie_id=${movie_idapi}&description=${description}`
    })
}
/* 
        QUEUE TABLE FUNCTIONS
                                */


function addToWatchlist(e) {
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("id");

    $.ajax({
        url: `/Helper/AddToWatchlist?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}

function addToFavorite(e) {
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("id");

    $.ajax({
        url: `/Helper/AddToFavorite?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}

function addToWatched(e) {
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("id");

    $.ajax({
        url: `/Helper/AddToWatched?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}

function addToNotify(e) {
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("id");

    $.ajax({
        url: `/Helper/AddToNotify?user_id=${user_id}&movie_idapi=${movie_idapi}`
    })
}


/* 
        END QUEUE TABLE FUNCTIONS
                                      */

$(document).ready(function () {

    $("form#search-term").on("submit", function (e) {
        e.preventDefault();
        let searchForm = new Object();
        searchForm.searchTerm = $("#q").val();
        window.location.href = "https://localhost:44383/Movie/Search?query=" + searchForm.searchTerm;
    })

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
    
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        else {
            return decodeURI(results[1]) || 0;
        } 
    }

    if ($('.heroSection').length) {
        heroMovie($.urlParam('id'));
        castSection($.urlParam('id'));
        similarSection($.urlParam('id'));
    }
    if ($.urlParam('query')) {
        search($.urlParam('query'));
    }

    if ($('.reviews').length) {
        addReviewQuery($.urlParam('id'));
        getReviews($.urlParam('id'));
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