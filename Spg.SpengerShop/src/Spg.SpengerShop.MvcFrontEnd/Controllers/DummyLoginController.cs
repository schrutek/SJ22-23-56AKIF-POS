using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.MvcFrontEnd.Helpers;
using Spg.SpengerShop.MvcFrontEnd.Models;
using System.Drawing;
using System.Text.Json;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class DummyLoginController : Controller
    {
        private readonly IAuthService _authService;

        public DummyLoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet()]
        public IActionResult Login()
        {
            return View(new LoginDto() { UserName="martin123" });
        }

        [HttpPost()]
        public IActionResult Login(LoginDto dto)
        {
            UserInformationDto userInformationDto = _authService.Login(dto.UserName, "", dto.Role);
            string json = JsonSerializer.Serialize(userInformationDto);
            //string json = JwtHelpers.GenerateToken(userInformationDto, "5Snh3qZNODtDd2Ibsj7irayIl6E1WWmpbvXtcSGlm1o=");

            // Username in Cookie merken
            HttpContext.Response.Cookies.Append("usernamecookie6akif", json, 
                new CookieOptions() 
                { 
                    Expires = DateTime.Now.AddMinutes(3), 
                });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout() 
        {
            HttpContext.Response.Cookies.Delete("usernamecookie6akif");

            return RedirectToAction("Index", "Home");
        }
    }
}
