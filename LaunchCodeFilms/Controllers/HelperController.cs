using LaunchCodeFilms.Data;
using LaunchCodeFilms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using unirest_net.http;
using Microsoft.Extensions.Configuration;

namespace LaunchCodeFilms.Controllers
{
    public class HelperController : Controller
    {
        private IConfiguration Configuration { get; set; }
        private readonly ApplicationDbContext context;

        public HelperController(ApplicationDbContext dbContext, IConfiguration config)
        {
            context = dbContext;
            Configuration = config;
        }

        public object GetMovie(string movie_idapi)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/" + movie_idapi + "?api_key=" + Configuration["APIKey"] + "&language=en-US&region=US&append_to_response=credits")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object movie = JsonConvert.DeserializeObject<object>(request.Body);

            return movie;
        }

        public object GetPerson(string person_id)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/person/" + person_id + "?api_key=" + Configuration["APIKey"] + "&language=en-US&append_to_response=credits")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object movie = JsonConvert.DeserializeObject<object>(request.Body);

            return movie;
        }


        public object GetCredits(string movie_idapi)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/" + movie_idapi + "/credits?api_key=" + Configuration["APIKey"])
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object movie = JsonConvert.DeserializeObject<object>(request.Body);

            return movie;
        }

        public object GetReviews(string movie_idapi)
        {
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == int.Parse(movie_idapi));

            if (movie == null)
            {
                return 1;
            }
            List<Review> reviews = context.Reviews.Include(cell => cell.User)
                .Where(cell => cell.MovieId == movie.ID).ToList();
            if (reviews.Count() == 0)
            {
                return 1;
            }

            string jsonData = JsonConvert.SerializeObject(reviews, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return JsonConvert.DeserializeObject<object>(jsonData); ;
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

        public object Search(string searchTerm)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/search/movie?api_key=" + Configuration["APIKey"]+ "&language=en-US&query=" + searchTerm + "&page=1&include_adult=false&region=US")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }

        public object GetPopularMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/popular?api_key=" + Configuration["APIKey"] + "&language=en-US&page=1")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }

        public object SimilarMovies(string movie_idapi)
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/" + movie_idapi + "/similar?api_key=" + Configuration["APIKey"] + "&language=en-US&page=1")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }


        public object GetUpcomingMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/upcoming?api_key=" + Configuration["APIKey"]  + "&language=en-US&page=1&region=US")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }

        [Authorize]
        public void SetRating(int user_id, int id, int rating)
        {
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == id);

            if (movie == null)
            {
                AddMovie(id);
            }

            Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == id);

            Review existingRecord = context.Reviews
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movieAdded.ID).FirstOrDefault();

            if (existingRecord == null)
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
                if (rating == 0 && existingRecord.Rating != 0)
                {
                    movieAdded.NumberRatings--;
                }
                existingRecord.Rating = rating;
            }
            context.SaveChanges();
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public void AddToFavorite(int user_id, int movie_idapi)
        {
            //ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            ApplicationUser user = context.Users.FirstOrDefault(c => c.Id == user_id);
            Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == movie_idapi);

            if (movie == null)
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
                if (existingRecord.Favorite == true)
                {
                    movieAdded.NumberFavorites--;
                }
                else
                {
                    movieAdded.NumberFavorites++;
                }

                existingRecord.Favorite = !existingRecord.Favorite;
            }
            context.SaveChanges();
        }

        [Authorize]
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

        [Authorize]
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