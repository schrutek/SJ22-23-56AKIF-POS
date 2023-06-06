using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Domain.Dtos;
using System.Text.Json;

namespace Spg.SpengerShop.MvcFrontEnd.Filters
{
    public class AuthorisationFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? token = context.HttpContext.Request.Cookies["usernamecookie6akif"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
            
            UserInformationDto? userInformation = JsonSerializer.Deserialize<UserInformationDto>(token);
            if (userInformation is null)
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
            string hash = HashHelper.CalculateHash($"{userInformation.FullName}-{userInformation.UserName}-{userInformation.Role}", "gI976UUn3/m59A==");
            if (hash != userInformation.Signature)
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
            if (userInformation.Role.ToLower() != "Admin".ToLower()) // guest, admin
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
        }
    }
}
