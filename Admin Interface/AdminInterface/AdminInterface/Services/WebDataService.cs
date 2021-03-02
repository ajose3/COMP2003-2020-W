using AdminInterface.Models;
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
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/comp2003/COMP2003_W/Admin/Validate?email={email}&password={password}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            returnedJson = returnedJson.Replace("/", "").Replace("\"", "");
            Token.value = returnedJson;
        }
    }
}
