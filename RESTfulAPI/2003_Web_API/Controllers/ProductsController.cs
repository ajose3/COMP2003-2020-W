using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _2003_Web_API.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2003_Web_API.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductMethods dataAccess;

        public ProductsController(COMP2003_WContext context)
        {
            dataAccess = new ProductMethods(context);
            //dataAccess = context;
        }


        // GET: api/Products
        // - Get Products -
        [HttpGet]
        [Route("products/get")]
        [Produces("application/json")]
        public async Task<ActionResult> Get()
        {
            List<Product> products = await dataAccess.Get();
            return Ok(products);
        }



        // POST: api/Customers
        // - Register Customer -
        [HttpPost]
        [Route("product/add")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Post(string token,[FromBody] Product product)
        {

            int responseMessage = await dataAccess.Add(token, product);
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

        [HttpPut]
        [Route("product/edit")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Put(string token, [FromBody] Product product)
        {

            int responseMessage = await dataAccess.Edit(token, product);
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
        [Route("product/delete")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Delete(string token, string Id)
        {

            int responseMessage = await dataAccess.Delete(token, Id);
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

        // GET: api/Products
        // - Get Products -
        [HttpGet]
        [Route("products/getTrending")]
        [Produces("application/json")]
        public async Task<ActionResult> GetTrending()
        {
            List<Product> products = await dataAccess.GetTrending();
            return Ok(products);
        }

        [HttpGet]
        [Route("products/getFeatured")]
        [Produces("application/json")]
        public async Task<ActionResult> GetFeatured()
        {
            List<Product> products = await dataAccess.GetFeatured();
            return Ok(products);
        }

        [HttpPut]
        [Route("products/addStock")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> AddStock(string token, int productId, int quantity)
        {
            int responseMessage = await dataAccess.AddStock(token, productId, quantity);
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
