using LaunchCodeFilms.Data;
using LaunchCodeFilms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unirest_net.http;

namespace LaunchCodeFilms.Controllers
{
    public class HelperController : Controller
    {
        private readonly ApplicationDbContext context;
        public HelperController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public object GetMovie(string movie_idapi)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/" + movie_idapi + "?api_key==en-US&region=US&append_to_response=credits")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object movie = JsonConvert.DeserializeObject<object>(request.Body);

            return movie;
        }

        public object GetCredits(string movie_idapi)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/"+movie_idapi+"/credits?api_key=")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object movie = JsonConvert.DeserializeObject<object>(request.Body);

            return movie;
        }

        public void AddMovie(int movie_idapi)
        {
            Movie newMovie = new Movie
            {
                MovieIDAPI = movie_idapi
            };

            context.Movies.Add(newMovie);
            context.SaveChanges();
        }

        public Queue GetQueueRecord(ApplicationUser user, Movie movieAdded)
        {
            Queue existingRecord = context.Queues
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movieAdded.ID).DefaultIfEmpty(null).First();

            return existingRecord;
        }

        public object GetPopularMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/popular?api_key=&language=en-US&page=1")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }

        public object GetUpcomingMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/upcoming?api_key=&language=en-US&page=1&region=US")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }


        public void SetRating(int user_id, int movie_idapi, int rating)
        {
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if (movie == null)
            {
                AddMovie(movie_idapi);
            }

            Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            Review existingRecord = context.Reviews
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movieAdded.ID).FirstOrDefault();

            if(existingRecord == null)
            {
                Review newRecord = new Review
                {
                    UserId = user.Id,
                    MovieId = movieAdded.ID,
                    Rating = rating
                };
                movieAdded.NumberRatings++;
                context.Reviews.Add(newRecord);
            }
            else
            {
                if(rating == 0 && existingRecord.Rating != 0)
                {
                    movieAdded.NumberRatings--;
                }
                existingRecord.Rating = rating;
            }
            context.SaveChanges();
        }

        public void SetDescription(int user_id, int movie_idapi, string description)
        {
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if (movie == null)
            {
                AddMovie(movie_idapi);
            }

            Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            Review existingRecord = context.Reviews
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movieAdded.ID).FirstOrDefault();


            if (existingRecord == null)
            {
                Review newRecord = new Review
                {
                    UserId = user.Id,
                    MovieId = movie.ID,
                    Description = description,
                    ReviewDate = DateTime.UtcNow
                };
                movie.NumberReviews++;
                context.Reviews.Add(newRecord);
            }
            else
            {
                // TODO: add logic to remove review and decrease NumberReviews
                existingRecord.Description = description;
            }
            context.SaveChanges();
        }

        public void AddToWatchlist(int user_id, int movie_idapi)
        {
            ApplicationUser user = context.Users.FirstOrDefault(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if (movie == null)
            {
                AddMovie(movie_idapi);
            }

            Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);
            Queue existingRecord = GetQueueRecord(user, movieAdded);

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user.Id,
                    MovieId = movieAdded.ID,
                    Watchlist = true
                };
                context.Queues.Add(newRecord);
            }
            else
            {
                existingRecord.Watchlist = !existingRecord.Watchlist;
            }
            context.SaveChanges();
        }


        public void AddToFavorite(int user_id, int movie_idapi)
        {
            //ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            ApplicationUser user = context.Users.FirstOrDefault(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if(movie == null)
            {
                AddMovie(movie_idapi);
            }

            Movie movieAdded = context.Movies.Single(c => c.MovieIDAPI == movie_idapi);
            Queue existingRecord = GetQueueRecord(user, movieAdded);

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user_id,
                    MovieId = movieAdded.ID,
                    Favorite = true
                };
                context.Queues.Add(newRecord);
                movieAdded.NumberFavorites++;
            }
            else
            {
                existingRecord.Favorite = !existingRecord.Favorite;
                movieAdded.NumberFavorites--;
            }    
            context.SaveChanges();
        }

        public void AddToWatched(int user_id, int movie_idapi)
        {
            //ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            ApplicationUser user = context.Users.FirstOrDefault(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if (movie == null)
            {
                AddMovie(movie_idapi);
            }

            Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);
            Queue existingRecord = GetQueueRecord(user, movieAdded);

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user_id,
                    MovieId = movieAdded.ID,
                    Watched = true
                };
                context.Queues.Add(newRecord);
            }
            else
            {
                existingRecord.Watched = !existingRecord.Watched;
            }
            context.SaveChanges();
        }

        public void AddToNotify(int user_id, int movie_idapi)
        {
            //ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            ApplicationUser user = context.Users.FirstOrDefault(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if (movie == null)
            {
                AddMovie(movie_idapi);
            }

            Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);
            Queue existingRecord = GetQueueRecord(user, movieAdded);

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user_id,
                    MovieId = movieAdded.ID,
                    NotifyTheater = true
                };
                context.Queues.Add(newRecord);
            }
            else
            {
                existingRecord.NotifyTheater = !existingRecord.NotifyTheater;
            }
            context.SaveChanges();
        }
    }
}
