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

        public object GetMovie()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/335984?&language=en-US")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object movie = JsonConvert.DeserializeObject<object>(request.Body);

            return movie;
        }

        public object GetPopularMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/popular?&language=en-US&page=1")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }


        public void SetRating(int user_id, int movie_id)
        {
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.Single(c => c.MovieIDAPI == movie_id);

            Review newRecord = new Review
                {
                    UserId = user.Id,
                    MovieId = movie.ID,
                    Rating = 1
                };

                context.Reviews.Add(newRecord);
                context.SaveChanges();
        }

        public void AddToWatchlist(int user_id, int movie_idapi)
        {
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.Single(c => c.MovieIDAPI == movie_idapi);

            Queue existingRecord = context.Queues
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movie.ID).FirstOrDefault();

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user.Id,
                    MovieId = movie.ID,
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
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.Single(c => c.MovieIDAPI == movie_idapi);

            Queue existingRecord = context.Queues
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movie.ID).FirstOrDefault();

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user_id,
                    MovieId = movie.ID,
                    Favorite = true
                };
                context.Queues.Add(newRecord);
            }
            else
            {
                existingRecord.Favorite = !existingRecord.Favorite;
            }    
            context.SaveChanges();
        }

        public void AddToWatched(int user_id, int movie_idapi)
        {
            //ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.Single(c => c.MovieIDAPI == movie_idapi);

            Queue existingRecord = context.Queues
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movie.ID).FirstOrDefault();

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user_id,
                    MovieId = movie.ID,
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
            ApplicationUser user = context.Users.Single(c => c.Id == user_id);
            Movie movie = context.Movies.Single(c => c.MovieIDAPI == movie_idapi);

            Queue existingRecord = context.Queues
                .Where(cell => cell.UserId == user.Id)
                .Where(cell => cell.MovieId == movie.ID).FirstOrDefault();

            if (existingRecord == null)
            {
                Queue newRecord = new Queue
                {
                    UserId = user_id,
                    MovieId = movie.ID,
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
