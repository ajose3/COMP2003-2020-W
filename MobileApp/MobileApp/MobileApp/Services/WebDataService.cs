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
            //var json = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/validate?email={user.email}&password={user.password}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
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
                RequestUri = new Uri("http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/register"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
            var a = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return a;

        }
        public async Task<string> PutUpdateCustomer(User user)
        {
            string token = TokenData.value;

            var json = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/edit?token={token}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        //public async Task<string> PutChangePassword(string password, string newPassword)
        public async Task<string> PutChangePassword(string newPassword)
        {
            string token = TokenData.value;

            //var json = JsonConvert.SerializeObject(user);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/changePassword?token={token}&newPassword={newPassword}"),
                //RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/changePassword?token={token}&oldPassword={password}&newPassword={newPassword}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
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
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/details?token={token}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //var a = response.JsonConvert.De
            
            User user = JsonConvert.DeserializeObject<User>(returnedJson);
            return user;

        }

        public async Task<string> PostAddOrder(int productId, int quantity)
        {
            //var json = JsonConvert.SerializeObject(product);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/orders/add?token={TokenData.value}&productId={productId}&quantity={quantity}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = new List<Order>();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/orders/get?token={TokenData.value}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //returnedJson = returnedJson.Replace("/", "").Replace("\"", "");
            //TokenData.value = returnedJson;

            orders = JsonConvert.DeserializeObject<List<Order>>(returnedJson);//fix
            return orders;
        }



        public async Task<Product> GetTrending()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/products/getTrending"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Product p = new Product();
            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            //Console.WriteLine(p);

            return products[0];

        }

        public async Task<Product> GetFeatured()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/products/getFeatured"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Product p = new Product();
            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            //Console.WriteLine(p);

            return products[0];

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/products/get"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Product p = new Product();
            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            //Console.WriteLine(p);

            return products;
        }

        public async Task<List<Product>> GetRecommended()
        {
            if (TokenData.value == null)
            {
                TokenData.value = "0";
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/recommend?token={TokenData.value}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Product p = new Product();
            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            //Console.WriteLine(p);

            return products;            
        }

        public async Task<List<Product>> GetRelatedProduct(int productId)
        {
            if (TokenData.value == null)
            {
                TokenData.value = "0";
            }
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/related?token={TokenData.value}&productId={productId}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Product p = new Product();
            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            return products;
        }

        public async Task<List<Review>> GetReviews(int productId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/reviews/get?productId={productId}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Product p = new Product();
            List<Review> reviews;
            reviews = JsonConvert.DeserializeObject<List<Review>>(returnedJson);

            if (reviews.Count != 0)
            {
                foreach (var rev in reviews)
                {
                    rev.StarColor1 = "Gray";
                    rev.StarColor2 = "Gray";
                    rev.StarColor3 = "Gray";
                    rev.StarColor4 = "Gray";
                    rev.StarColor5 = "Gray";
                    if (rev.Rating >= 1)
                    {
                        rev.StarColor1 = "Gold";
                    }
                    if (rev.Rating >= 2)
                    {
                        rev.StarColor2 = "Gold";
                    }
                    if (rev.Rating >= 3)
                    {
                        rev.StarColor3 = "Gold";
                    }
                    if (rev.Rating >= 4)
                    {
                        rev.StarColor4 = "Gold";
                    }
                    if (rev.Rating >= 5)
                    {
                        rev.StarColor5 = "Gold";
                    }
                }
            }
            return reviews;
        }

        public async Task<string> PostAddReview(Review review)
        {
            var json = JsonConvert.SerializeObject(review);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/reviews/add?token={TokenData.value}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        }

        public async Task<string> Logout()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/logOut?token={TokenData.value}"),
                //Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            TokenData.value = "0";

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

    }
}
