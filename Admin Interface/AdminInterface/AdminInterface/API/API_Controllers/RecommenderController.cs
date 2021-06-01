using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using AdminInterface.Models;

// API controllers have to be in AdminInterface namespace to be initialised with context
namespace AdminInterface.API_Controllers
{
    public class RecommenderController : Controller
    {
        private readonly API_RecommenderMethods dataAccess;

        public RecommenderController(COMP2003_WContext context)
        {
            dataAccess = new API_RecommenderMethods(context);
        }

        [HttpGet]
        [Route("customer/related")]
        [Produces("application/json")]
        public async Task<ActionResult<List<API_Product>>> GetRelated(string token, int productId)
        {
            List<API_Product> products = await dataAccess.GetRelatedProducts(token, productId);
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
        public async Task<ActionResult<List<API_Product>>> Get(string token)
        {
            List<API_Product> products = await dataAccess.GetMostRecommended(token);
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

            List<API_Product> products = await dataAccess.RecommendedForHome(Token);
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
