﻿using _2003_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2003_Web_API.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderMethods dataAccess;
        public OrdersController(COMP2003_WContext context)
        {
            dataAccess = new OrderMethods(context);
        }


        // Get: api/reviews
        // - Get Order -
        [HttpGet]
        [Route("orders/get")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Get(string token)
        {

            List<Order> orders = await dataAccess.Get(token);
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
        public async Task<ActionResult<int>> Post(string token, int productId, int quantity)
        {

            int responseMessage = await dataAccess.Add(token, productId, quantity);
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

    }
}