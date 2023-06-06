using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Domain.Dtos;
using System.Collections;
using System.IO;
using System.Text;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet()]
        public IActionResult Login()
        {
            return View(new LoginDto());
        }
        [HttpPost()]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            return View(dto);
        }

        [HttpGet()]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("usernamecookie6akif");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet()]
        public IActionResult Register()
        {
            return View(new RegisterDto() {  BirthDate = DateTime.Now.AddYears(-14) });
        }

        [HttpPost()]
        public IActionResult Register(RegisterDto dto)
        {
            // Check existing EMail Address DB lookup
            // TODO: Call service
            //if (_service.CheckEmail(dto.EMail).... "a@b.at")
            if (dto.EMail == "a@b.at")
            {
                ModelState.AddModelError("", "EMail existiert bereits!");
            }
            // If ... AddMOdelError...
            if (ModelState.IsValid)
            {
                // TODO: Regrierung in DB eintragen

                //try
                //{
                //    // _customerService.Create(dto)
                //}
                //catch (...Exception)
                //{
                //    // TODO: wie geht man damit um
                //    ModelState.AddModelError("", "Wahtever ist schief gegangen!");
                //}
                return RedirectToAction("Index", "Home"); // Wenn alles OK
            }
            else
            {
                return View(dto); // Bei Fehler
            }
        }
    }
}
