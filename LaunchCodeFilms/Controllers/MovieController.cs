using LaunchCodeFilms.Data;
using LaunchCodeFilms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext context;
        public MovieController(ApplicationDbContext dbContext)
        {
            context = dbContext;
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(int movie_idapi)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Comments()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Reviews()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddReview(int id)
        {
            ViewBag.movie_id = id;
            AddReviewViewModel addReviewViewModel = new AddReviewViewModel { };
            return View(addReviewViewModel);
            
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddReview(AddReviewViewModel addReviewViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add new review
                if (ModelState.IsValid)
                {
                    ApplicationUser user = context.Users.Single(c => c.Id == addReviewViewModel.UserId);
                    Movie movie = context.Movies.FirstOrDefault(c => c.MovieIDAPI == addReviewViewModel.MovieId);

                    if (movie == null)
                    {
                        AddMovie(addReviewViewModel.MovieId);
                    }

                    Movie movieAdded = context.Movies.FirstOrDefault(c => c.MovieIDAPI == addReviewViewModel.MovieId);

                    Review existingRecord = context.Reviews
                        .Where(cell => cell.UserId == user.Id)
                        .Where(cell => cell.MovieId == movieAdded.ID).FirstOrDefault();


                    if (existingRecord == null)
                    {
                        Review newRecord = new Review
                        {
                            UserId = user.Id,
                            MovieId = movieAdded.ID,
                            Description = addReviewViewModel.Description,
                            ReviewDate = DateTime.UtcNow
                        };
                        movieAdded.NumberReviews++;
                        context.Reviews.Add(newRecord);
                    }
                    else
                    {
                        // TODO: add logic to remove review and decrease NumberReviews
                        existingRecord.Description = addReviewViewModel.Description;
                    }
                    context.SaveChanges();
                }
                return Redirect("/Movie/Reviews?id=" + addReviewViewModel.MovieId);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();

        }
    }
}
