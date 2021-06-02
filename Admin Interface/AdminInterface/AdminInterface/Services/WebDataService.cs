﻿using AdminInterface.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminInterface.Services
{
    public class WebDataService : IDataService
    {
        HttpClient httpClient;

        HttpClient Client => httpClient ?? (httpClient = new HttpClient());
        public async Task GetValidateAdmin(string email, string password)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get, 
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/comp2003/COMP2003_W/admin/Validate?email={email}&password={password}"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            returnedJson = returnedJson.Replace("/", "").Replace("\"", "");
            Token.value = returnedJson;
        }

        public async Task DeleteCustomer(int id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/admin/DeleteCustomer?token={Token.value}&customerId={id}"),
            };
            var response = await Client.SendAsync(request).ConfigureAwait(false);
        }


        public async Task DeleteProduct(int id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/product/delete?token={Token.value}&Id={id}"),
            };
            var response = await Client.SendAsync(request).ConfigureAwait(false);
        }
    }
}
