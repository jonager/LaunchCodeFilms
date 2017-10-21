// Write your JavaScript code.
"use strict";

function getPopularMovies() {

    $.ajax(
        {
            url: "/Helper/GetPopularMovies",
            success: function (data) {
                console.log(data);
                let movies = data.results;
                $("body").append(
                    `<div style = "margin-bottom:50px;">  
                         <h1>Movie of the Day</h1>
                         <p>${movies[0].original_title}</p>
                         <img src = "https://image.tmdb.org/t/p/w500${movies[0].poster_path}">
                     </div>`
                );

                for (let i = 1; i < 9; i++){
                    $("body").append(
                        `<div style = "margin-bottom:20px;">  
                            <h2>${movies[i].original_title}</h2>
                            <img src = "https://image.tmdb.org/t/p/w500${movies[i].poster_path}">
                        </div>`
                    );
                }
                
            },
            error: function () {
                console.log("Error!!!")
            }
        }
    )
}

function displayRating() {
    $.ajax({
        url: "/Helper/GetMovie",
        success: function (data) {
            console.log(data);
            let rating = data.vote_average;
            $(".rating").append(`<p>${rating}</p>`)
        },
        error: function (data) {
            console.log("Error!!!")
        },
    })
}


function setRating(e) {
    console.log(e);
    let user_id = $(e).data("userid");
    let movie_idapi = $(e).data("movieidapi");

     $.ajax({
        url: `/Helper/SetRating?user_id=${user_id}&movie_id=${movie_idapi}`
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
    getPopularMovies();
    displayRating();

    // prevents a tags from redirecting
    $("a.no_click").click(function (e) {
        e.preventDefault();
    });
}); //end .ready