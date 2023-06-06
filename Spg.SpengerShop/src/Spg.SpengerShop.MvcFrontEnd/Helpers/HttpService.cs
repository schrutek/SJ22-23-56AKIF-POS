using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.MvcFrontEnd.Models;
using System.Text.Json;

namespace Spg.SpengerShop.MvcFrontEnd.Helpers
{
    public class HttpService
    {
        private readonly HttpContext _context;

        public HttpService(IHttpContextAccessor context)
        {
            _context = context.HttpContext;
        }

        public string GetUserName()
        {
            string? json = _context.Request.Cookies["usernamecookie6akif"];
            if (string.IsNullOrEmpty(json))
            {
                return "nicht angemeldet";
            }

            UserInformationDto? userInformation = JsonSerializer.Deserialize<UserInformationDto>(json);

            return userInformation?.FullName 
                ?? "unbekannt";
        }

        public string IsLoggedIn
        {
            get
            {
                if (!string.IsNullOrEmpty(_context.Request.Cookies["usernamecookie6akif"]))
                {
                    return "";
                }
                return "disabled";
            }
        }
    }
}
