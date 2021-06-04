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
    public class LogoutController : Controller
    {
        //// GET: /<controller>/
        //public async Task<IActionResult> IndexAsync()
        //{
        //    // sign out
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Redirect("~/Login");
        //}
        // GET: /<controller>/
        public async Task<IActionResult> IndexAsync()
        {
            // sign out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            DeleteCookies();
            Token.value = null;
            return Redirect("~/Login");
        }



        private void DeleteCookies()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
        }

    }
}
