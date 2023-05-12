using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.MvcFrontEnd.Filters;
using Spg.SpengerShop.MvcFrontEnd.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string message = string.Empty;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            message = "Hello World!!!";
            return View();
        }

        [HttpGet()]
        [AuthorisationFilter()]
        public IActionResult Privacy()
        {
            return View("Privacy");


            //return RedirectToAction("Unauthorized", "Home");
        }

        [HttpGet()]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}