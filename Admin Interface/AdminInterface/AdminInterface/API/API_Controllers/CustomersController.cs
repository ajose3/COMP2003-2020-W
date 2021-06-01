using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _2003_Web_API.Models;
using AdminInterface.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2003_Web_API.Controllers
{
    public class CustomersController : Controller
    {
        //private readonly COMP2003_WContext dataAccess;
        private readonly CustomerMethods dataAccess;

        public CustomersController(COMP2003_WContext context)
        {
            dataAccess = new CustomerMethods(context);
            //dataAccess = context;
        }


        // GET: api/Customers
        // - Validate Customer -
        [HttpGet]
        [Route("customer/validate")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Get(string email, string password)
        {
            string token = await dataAccess.Validate(email, password);
            // check token
            if (token == "0" || token == "400" || token == "500")
            {
                return BadRequest(token);
            }
            else
            {
                return Ok(token);
            }
        }

        // GET: api/Customers
        // - Logout Customer -
        [HttpDelete]
        [Route("customer/logOut")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> LogOut(string token)
        {
            string response = await dataAccess.LogOut(token);
            // check token
            if (response == "400" || response == "401" || response == "500")
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }


        // POST: api/Customers
        // - Register Customer -
        [HttpPost]
        [Route("customer/register")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Post([FromBody] Customer customer)
        {

            int responseMessage = await dataAccess.Register(customer);
            // check response
            if (responseMessage == 200)
            {
                return Ok(responseMessage);
            }
            else
            {
                return BadRequest(responseMessage);
            }
        }

        // PUT: api/Customers
        // - Edit Customer -
        [HttpPut]
        [Route("customer/edit")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Put(string token, [FromBody] Customer customer)
        {
            int responseMessage = await dataAccess.Edit(token, customer);
            // "9B-4B45-AEE8-F307034EA892"
            // check response
            if (responseMessage == 200)
            {
                return Ok(responseMessage);
            }
            else
            {
                return BadRequest(responseMessage);
            }
        }

        // PUT: api/Customers
        // - Change Password -
        [HttpPut]
        [Route("customer/changePassword")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> ChangePassword(string token, string newPassword)
        {
            int responseMessage = await dataAccess.ChangePassword(token, newPassword);
            // check response
            if (responseMessage == 200)
            {
                return Ok(responseMessage);
            }
            else
            {
                return BadRequest(responseMessage);
            }
        }

        // DELETE: api/Customers
        [HttpDelete]
        [Route("customer/delete")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Delete(string token)
        {
            int responseMessage = await dataAccess.Delete(token);
            // check response
            if (responseMessage == 200)
            {
                return Ok(responseMessage);
            }
            else
            {
                return BadRequest(responseMessage);
            }
        }

        // - Customer details -
        [HttpGet]
        [Route("customer/details")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> GetDetails(string token)
        {
            return Ok(await dataAccess.GetDetails(token));
        }
    }
}
