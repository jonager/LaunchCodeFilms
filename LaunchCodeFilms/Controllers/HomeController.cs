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

        public object GetPopularMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/popular?api_key=?&language=en-US&page=1")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               //.header("User-Agent", "Spotify API Console v0.1")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }

        public IActionResult Index()
        {
            
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
