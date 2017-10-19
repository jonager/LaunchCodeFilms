using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LaunchCodeFilms.Models;
using LaunchCodeFilms.Data;

namespace LaunchCodeFilms.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext context;
        //public HomeController(ApplicationDbContext dbContext)
        //{
        //    context = dbContext;
        //}

        public IActionResult Index()
        {
            //ApplicationUser user = context.Users.Single(
            //    c => c.Id == 1);
            //UserProfile newUserProfile = new UserProfile
            //{
            //    ThemeColor = "red",
            //    Gender = 'm',
            //    Description = "test1",
            //    Picture = "profile Picture",
            //    User = user
            //};

            //context.UserProfiles.Add(newUserProfile);
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
