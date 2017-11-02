using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LaunchCodeFilms.Models;
using LaunchCodeFilms.Data;
using LaunchCodeFilms.HelperFuncs;
using unirest_net.http;
using Newtonsoft.Json;

namespace LaunchCodeFilms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        public HomeController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            //ApplicationUser user = context.Users.FirstOrDefault(c => c.Id == 3);

            //Movie newMovie = new Movie
            //{
            //    MovieIDAPI = 857
            //};

            //Review newReview = new Review
            //{
            //    Description = "test1 haoshfjdfojsfosjdofdsofosafojdfjsfosdfjodf" +
            //    "oaofdjifosdjfdjsfosdfdsofjs",
            //    MovieId = 346364,
            //    UserId = 3
            //};

            //context.Movies.Add(newMovie);
            //context.Reviews.Add(newReview);
            //context.SaveChanges();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
