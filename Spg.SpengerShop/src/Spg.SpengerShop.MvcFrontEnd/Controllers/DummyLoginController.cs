using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.MvcFrontEnd.Helpers;
using Spg.SpengerShop.MvcFrontEnd.Models;
using System.Drawing;
using System.Security.Claims;
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

        //[HttpGet()]
        //public IActionResult Login()
        //{
        //    ViewData["roles"] = new SelectList(new List<RoleDto>()
        //    {
        //        new RoleDto() { Id = "guest", Name = "Guest"},
        //        new RoleDto() { Id = "admin", Name = "Admin"},
        //    }, "Id", "Name");

        //    return View(new LoginDto() { UserName="martin123" });
        //}

        [HttpGet()]
        public IActionResult Login()
        {
            IEnumerable<SelectListItem> rolesModel = new List<RoleDto>()
            {
                new RoleDto() { Id = "guest", Name = "Guest"},
                new RoleDto() { Id = "admin", Name = "Admin"},
            }.Select(r=> new SelectListItem(r.Name, r.Id));

            LoginDto loginModel = new LoginDto() { UserName = "hans" };

            return View((loginModel, rolesModel));
        }

        [HttpPost()]
        public IActionResult Login(LoginDto dto)
        {
            (bool isLoggedIn, UserInformationDto userInformationDto, string message) = _authService.Login(dto.UserName, "", dto.Role);
            
            string token = JsonSerializer.Serialize(userInformationDto);
            //string token = JwtHelpers.GenerateToken(userInformationDto, "5Snh3qZNODtDd2Ibsj7irayIl6E1WWmpbvXtcSGlm1o=");

            // Username in Cookie merken
            HttpContext.Response.Cookies.Append("usernamecookie6akif", token, 
                new CookieOptions() 
                { 
                    Expires = DateTime.Now.AddMinutes(3), 
                });

            //HttpContext.SignInAsync(new ClaimsPrincipal(), ...);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout() 
        {
            HttpContext.Response.Cookies.Delete("usernamecookie6akif");

            return RedirectToAction("Index", "Home");
        }
    }
}
