using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Models;
using AdminInterface.Models;

// API controllers have to be in AdminInterface namespace to be initialised with context
namespace AdminInterface.API_Controllers
{
    public class OrdersController : Controller
    {
        private readonly API_OrderMethods dataAccess;
        public OrdersController(COMP2003_WContext context)
        {
            dataAccess = new API_OrderMethods(context);
        }


        // Get: api/reviews
        // - Get Order -
        [HttpGet]
        [Route("orders/get")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Get(string token)
        {

            List<API_Order> orders = await dataAccess.Get(token);
            return Ok(orders);
            //// check response
            //if (responseMessage == 200)
            //{
            //    return Ok(responseMessage);
            //}
            //else
            //{
            //    return BadRequest(responseMessage);
            //}
        }

        // POST: api/reviews
        // - Add Order -
        [HttpPost]
        [Route("orders/add")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Post(string token, int productId, int quantity, DateTime deliveryDate)
        {

            int responseMessage = await dataAccess.Add(token, productId, quantity, deliveryDate);
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

        [HttpDelete]
        [Route("orders/delete")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Delete(string token, int orderId)
        {

            int responseMessage = await dataAccess.Delete(token, orderId);
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

        [HttpGet]
        [Route("orders/lowStock")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> LowStock(string token)
        {

            List<API_Product> products = await dataAccess.GetLowStock(token);
            return Ok(products);
        }


        [HttpGet]
        [Route("orders/OrdersbyId")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> OrdersById(string token, int productId)
        {
            List<API_Order> orders = await dataAccess.GetOrdersbyId(token, productId);
            return Ok(orders);
        }
    }
}
