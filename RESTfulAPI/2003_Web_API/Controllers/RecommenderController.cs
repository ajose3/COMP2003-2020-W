using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _2003_Web_API.Models;

namespace _2003_Web_API.Controllers
{
    public class RecommenderController : Controller
    {
        private readonly RecommenderMethods dataAccess;

        public RecommenderController(COMP2003_WContext context)
        {
            dataAccess = new RecommenderMethods(context);
        }

        [HttpGet]
        [Route("customer/related")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Product>>> GetRelated(string token, int productId)
        {
            List<Product> products = await dataAccess.GetRelatedProducts(token, productId);
            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return BadRequest(products);
            }
        }

        [HttpGet]
        [Route("customer/recommend")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Product>>> Get(string token)
        {
            List<Product> products = await dataAccess.GetMostRecommended(token);
            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return BadRequest(products);
            }
        }

        [HttpGet]
        [Route("recommendation/home")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> GetRecommendedForHome(string Token)
        {

            List<Product> products = await dataAccess.RecommendedForHome(Token);
            return Ok(products);
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
    }
}
