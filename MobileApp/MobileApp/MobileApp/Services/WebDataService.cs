using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Net.Http.Headers;
using RestSharp;

namespace MobileApp.Services
{
    class WebDataService : IDataService
    {
        HttpClient httpClient;

        HttpClient Client => httpClient ?? (httpClient = new HttpClient());
        public async Task GetValidateCustomer(User user)
        {
            var json = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/Validate?email={user.email}&password={user.password}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            returnedJson = returnedJson.Replace("/","").Replace("\"","");
            TokenData.value = returnedJson;

        }
        public async Task<string> PostRegisterCustomer(User user)
        {
            var json = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/Register"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        }
        public async Task<string> PutUpdateCustomer(User user)
        {
            string token = TokenData.value;
            //token = "DD-4663-A521-3A96D3BD4BBF";

            //user.Address = "Address";
            //user.Age = 1;
            //user.email = "email1@email.com";
            //user.FirstName = "changed name";
            //user.LastName = "lastName";
            //user.password = "password";
            //user.PhoneNumber = "12345";
            //user.Gender = false;

            var json = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/Update?token={token}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<User> GetCustomerDetails()
        {
            string token = TokenData.value;

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/GetDetails?token={token}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //var a = response.JsonConvert.De
            
            User user = JsonConvert.DeserializeObject<User>(returnedJson);
            return user;

        }

    }
}
