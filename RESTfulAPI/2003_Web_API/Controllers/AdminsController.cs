using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _2003_Web_API.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2003_Web_API.Controllers
{
    public class AdminsController : Controller
    {
        //private readonly COMP2003_WContext dataAccess;
        private readonly AdminMethods dataAccess;

        public AdminsController(COMP2003_WContext context)
        {
            dataAccess = new AdminMethods(context);
            //dataAccess = context;
        }


        // GET: api/Admins
        // - Validate Admin -
        [HttpGet]
        [Route("admin/validate")]
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

        // POST: api/Admins
        // - Register Admin -
        [HttpPost]
        [Route("admin/register")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Post(string token, [FromBody] Customer customer)
        {

            int responseMessage = await dataAccess.Register(token, customer);
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
        [Route("admin/changePassword")]
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

        // - Edit admin -
        [HttpPut]
        [Route("admin/EditAdmin")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> EditAdmin(string token, [FromBody] Customer customer)
        {
            int responseMessage = await dataAccess.EditAdmin(token, customer);
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


        // Admin actions

        // - Edit customer -
        [HttpPut]
        [Route("admin/EditCustomer")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> EditCustomer(string token, [FromBody] Customer customer)
        {
            int responseMessage = await dataAccess.EditCustomer(token, customer);
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




        // - Delete customer -
        [HttpDelete]
        [Route("admin/DeleteCustomer")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> DeleteCustomer(string token, int customerId)
        {
            int responseMessage = await dataAccess.DeleteCustomer(token, customerId);
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

        // - Delete customer -
        [HttpDelete]
        [Route("admin/cancelOrder")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> CancelOrder(string token, int orderId, int customerId)
        {
            int responseMessage = await dataAccess.CancelOrderAdmin(token, orderId ,customerId);
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





    }
}
