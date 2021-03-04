using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdminInterface.Models;
using AdminInterface.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminInterface.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            WebDataService dataService = new WebDataService();
            await dataService.GetValidateAdmin(email, password);
            //contact api (admin login)
            if (Token.value == null || Token.value == "1")
            {
                return Redirect("~/Login");
            }

            //if password correct
            else
            {
                return Redirect("~/Home");
            }
            



        }
    }
}
