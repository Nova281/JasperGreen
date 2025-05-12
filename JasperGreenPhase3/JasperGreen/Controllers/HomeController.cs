//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Home Controller represents the bridge between the JasperGreenContext.cs and all of the Views under Home. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.
using System.Diagnostics;
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;

namespace JasperGreen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
