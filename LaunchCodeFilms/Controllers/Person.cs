using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Controllers
{
    public class Person : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
