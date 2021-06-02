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
    public class WebDataService
    {
        HttpClient httpClient;

        HttpClient Client => httpClient ?? (httpClient = new HttpClient());
        public async Task<string> GetValidateCustomer(User user)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/validate?email={user.email}&password={user.password}"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            returnedJson = returnedJson.Replace("/","").Replace("\"","");
            TokenData.value = returnedJson;
            return returnedJson;

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

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/changePassword?token={token}&newPassword={newPassword}"),
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
            };
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            User user = JsonConvert.DeserializeObject<User>(returnedJson);
            return user;

        }

        public async Task<string> PostAddOrder(int productId, int quantity, DateTime deliveryDate)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/orders/add?token={TokenData.value}&productId={productId}&quantity={quantity}&DeliveryDate={deliveryDate}"),
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
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            orders = JsonConvert.DeserializeObject<List<Order>>(returnedJson);

            //foreach (var order in orders)
            //{
            //    order.DeliveryDate = order.GetDeliveryDate();
            //}
            
            return orders;
        }

        public async Task<string> DeleteOrder(int orderId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/orders/delete?token={TokenData.value}&orderID={orderId}"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            var a = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return a;
        }



        public async Task<Product> GetTrending()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/products/getTrending"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            return products[0];
        }

        public async Task<Product> GetFeatured()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/products/getFeatured"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            return products[0];

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/products/get"),
            };
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

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
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/recommendation/home?Token={TokenData.value}"),
            };

            //RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/customer/recommend?token={TokenData.value}"),


            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<Product> products;
            products = JsonConvert.DeserializeObject<List<Product>>(returnedJson);

            //List<Product> ps = new List<Product>();

            //for (int i = 0; i < 5; i++)
            //{
            //    ps.Add(products[i]);
            //}
            //return ps;

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
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

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
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

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
            //response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<string> DeleteReview(ReviewWName review)
        {
            var json = JsonConvert.SerializeObject(review);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/reviews/delete?token={TokenData.value}&ProductId={review.ProdID}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
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
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }




        public async Task<List<ReviewWName>> GetCustomersReviews()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://web.socem.plymouth.ac.uk/COMP2003/COMP2003_W/reviews/getCustomersProds?token={TokenData.value}"),
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<ReviewWName> reviews;
            reviews = JsonConvert.DeserializeObject<List<ReviewWName>>(returnedJson);

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

    }
}
