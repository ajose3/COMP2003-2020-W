using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdminInterface.Models;
using AdminInterface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminInterface.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            // if user is already logged in
            if (User.Identity.IsAuthenticated)
            {
                // redirect
                return Redirect("~/Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            WebDataService dataService = new WebDataService();
            await dataService.GetValidateAdmin(email, password);
            //contact api (admin login)
            if (Token.value == null || Token.value.Length <= 3)
            {
                return Redirect("~/Login");
            }

            //if password correct
            else
            {
                // create an authentication cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                // sign user in
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // redirect
                return Redirect("~/Home");
            }
            
        }

    }
}
