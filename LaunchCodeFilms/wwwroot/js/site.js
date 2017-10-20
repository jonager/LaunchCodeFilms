// Write your JavaScript code.
"use strict";

function getPopularMovies() {

    $.ajax(
        {
            url: "/Home/GetPopularMovies",
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
                    console.log()
                    

                    $("body").append(
                        `<div style = "margin-bottom:20px;">  
                            <h2>${movies[i].original_title}</h2>
                            <img src = "https://image.tmdb.org/t/p/w500${movies[i].poster_path}">
                        </div>`
                        
                    );
                }
                
            },
            error: function () {
                console.log("Error bitches!")
            }
        }
    )
}

$(document).ready(function () {
    getPopularMovies();
    });