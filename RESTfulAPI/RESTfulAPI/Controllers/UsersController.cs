﻿using RESTfulAPI.Models;
using RESTfulAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTfulAPI.Controllers
{
    public class UsersController : ApiController
    {
        /*
         * https://Domain/[RouteName]
         * 
         */


        //user requests
        [HttpGet]
        [Route("Validate")]
        public string ValidateCustomer(string email, string password)
        {
            string token = DataAccess.ValidateCustomer(email, password);
            if (token != "0")
            {
                return token;
            }
            return null;
        }

        // POST api/values
        [HttpPost]
        [Route("Register")]
        public bool RegisterCustomer(User user)
        {
            return DataAccess.RegisterCustomer(user);
        }

        // PUT api/values/
        [HttpPut]
        [Route("Update")]
        public bool UpdateCustomer(string token, User user)
        {
            return DataAccess.UpdateCustomer(token, user);
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("Delete")]
        public bool DeleteCustomer(string token)
        {
            return DataAccess.DeleteCustomer(token);
        }

        [HttpGet]
        [Route("GetDetails")]
        public User GetCustomerDetails(string token)
        {
            return DataAccess.GetCustomerDetails(token);
        }

        //Admin requests

        [HttpGet]
        [Route("Admin/Validate")]
        public string ValidateAdmin(string email, string password)
        {
            string token = DataAccess.ValidateAdmin(email, password);
            if (token != "0")
            {
                return token;
            }
            return null;
        }

        [HttpPost]
        [Route("Admin/Register")]
        public bool RegisterAdmin(string token, User user)
        {
            return DataAccess.RegisterAdmin(token ,user);
        }

        [HttpDelete]
        [Route("Admin/Delete")]
        public bool AdminRemoveCustomer(string token,int user_ID)
        {
            return DataAccess.AdminRemoveCustomer(token, user_ID);
        }

    }
}