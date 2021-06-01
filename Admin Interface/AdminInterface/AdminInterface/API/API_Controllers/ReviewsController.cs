using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using AdminInterface.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// API controllers have to be in AdminInterface namespace to be initialised with context
namespace AdminInterface.API_Controllers
{
    public class ReviewsController : Controller
    {
        private readonly API_ReviewMethods dataAccess;

        public ReviewsController(COMP2003_WContext context)
        {
            dataAccess = new API_ReviewMethods(context);
        }


        // GET: api/reviews
        // - Get all reviews for a product -
        [HttpGet]
        [Route("reviews/get")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Get(int productId)
        {
            List<API_Review> reviews = await dataAccess.Get(productId);
            return Ok(reviews);
        }

        // POST: api/reviews
        // - Add Review -
        [HttpPost]
        [Route("reviews/add")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Post(string token, [FromBody] API_Review review)
        {

            int responseMessage = await dataAccess.Add(token, review);
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
        // - Edit Review -
        [HttpPut]
        [Route("reviews/edit")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Put(string token, [FromBody] API_Review review)
        {

            int responseMessage = await dataAccess.Edit(token, review);
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
        [Route("reviews/delete")]
        [Produces("application/json")]
        public async Task<ActionResult<int>> Delete(string token, int ProductId)
        {

            int responseMessage = await dataAccess.Delete(token, ProductId);
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

        // GET: api/reviews
        // - Get all reviews for a product -
        [HttpGet]
        [Route("reviews/getCustomersProds")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> ReviewsFromCustomer(string token)
        {
            List<API_ReviewsWithName> reviews = await dataAccess.GetCustomersReviews(token);
            return Ok(reviews);
        }

    }
}
