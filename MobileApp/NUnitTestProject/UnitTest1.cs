using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    public class Tests
    {
        //some tests must be ran seperatly to be successful
        [TestFixture]
        public class LoginViewModelTest
        {
            LoginViewModel _vm;
            //TokenData tokenData;
            [SetUp]
            public void Setup()
            {
                //tokenData = new TokenData();
                _vm = new LoginViewModel();
            }

            [Test]
            public async Task Login_UsernameIsSet()
            {
                _vm.username = "Test@email.com";

                Assert.IsNotNull(_vm.username, "Username is null after being initialised");
            }

            [Test]
            public async Task Login_PasswordIsSet()
            {
                _vm.password = "password1";

                Assert.IsNotNull(_vm.password, "Username is null after being initialised");
            }
        }
        [TestFixture]
        public class WebDataServiceTest
        {
            TokenData token;
            WebDataService _s;
            [SetUp]
            public void Setup()
            {
                _s = new WebDataService();
                //Task.Run(async () => await GetValidateCustomerSuccess());
                //string t = Task.Run(async () => await GetTokenString()).ToString();
                //token = new TokenData(t);

            }


            public async Task Validate()
            {
                User testLoginUser = new User
                {
                    email = "unitTest@spam.com",
                    password = "password"
                };

                await _s.GetValidateCustomer(testLoginUser);
            }

            [Test]
            public async Task GetValidateCustomerSuccess()
            {
                User testUser = new User
                {
                    email = "unitTest@spam.com",
                    password = "password",
                };

                string returnToken = await _s.GetValidateCustomer(testUser);

                Console.WriteLine(returnToken);


                if (returnToken.Length > 3)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            [Test]
            public async Task PostRegisterCustomerSuccess()
            {
                User testUser = new User
                {
                    Address = "25 london eye",
                    Age = 21,
                    email = "unitTest@spam.com",
                    FirstName = "Jack",
                    LastName = "Potter",
                    password = "password",
                    PhoneNumber = "084876537342"
                };

                string returnCode = await _s.PostRegisterCustomer(testUser);

                if (returnCode == "200")
                {
                    Assert.Pass();
                }
                if (returnCode == "208")
                {
                    Console.WriteLine("Success but user already exisits");
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task PutUpdateCustomerSuccess()
            {
                await Validate();

                User testUser = new User
                {
                    Address = "25 london eye",
                    Age = 21,
                    email = "unitTest@spam.com",
                    FirstName = "Jack",
                    LastName = "Potter",
                    password = "password",
                    PhoneNumber = "084876537342"
                };

                string returnCode = await _s.PutUpdateCustomer(testUser);


                if (returnCode == "200")
                {
                    // reset data for future testing

                    User testUser2 = new User
                    {
                        Address = "25 london eye",
                        Age = 21,
                        email = "unitTest@spam.com",
                        FirstName = "Jack",
                        LastName = "Potter",
                        password = "password",
                        PhoneNumber = "084876537342"
                    };

                    await _s.PutUpdateCustomer(testUser2);

                    Assert.Pass();
                }
                if (returnCode == "208")
                {
                    Console.WriteLine("Success but user another user exists with that email");
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            [Test]
            public async Task PutChangePasswordSuccess()
            {
                await Validate();


                string newPassword = "password1";
                //change password and get success code
                string returnCode = await _s.PutChangePassword(newPassword);


                if (returnCode == "200")
                {
                    newPassword = "password";
                    //change password back
                    await _s.PutChangePassword(newPassword);


                    Assert.Pass();
                }
                if (returnCode == "208")
                {
                    Console.WriteLine("Success but user another user exists with that email");
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            //[Test]
            //public async Task GetCustomerDetailsSuccess()
            //{
            //    TokenData.value == token;

            //    User returnUser = await _s.GetCustomerDetails();

            //    if (returnUser != null)
            //    {
            //        Assert.Pass();
            //    }
            //    else
            //    {
            //        Assert.Fail();
            //    }
            //}


            [Test]
            public async Task PostAddOrderSuccess()
            {
                await Validate();

                //place order
                int productId = 1;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                string returnCode = await _s.PostAddOrder(productId, quantity, deliveryDate);

                Console.WriteLine(returnCode);

                if (returnCode == "200")
                {
                    Assert.Pass();
                }
                if (returnCode == "208")
                {
                    Console.WriteLine("Success but user another user exists with that email");
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            [Test]
            public async Task GetOrdersSuccess()
            {
                await Validate();

                //place order
                int productId = 1;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                await _s.PostAddOrder(productId, quantity, deliveryDate);


                List<Order> returnOrders = await _s.GetOrders();

                Console.WriteLine(returnOrders.Count);

                if (returnOrders.Count > 0)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task DeleteOrderSuccess()
            {
                //login

                await Validate();

                //add order
                int productId = 1;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                await _s.PostAddOrder(productId, quantity, deliveryDate);

                //get all orders
                List<Order> returnOrders = await _s.GetOrders();

                Console.WriteLine(returnOrders.Count);

                //get first orders id
                int orderId = returnOrders[0].OrderID;

                string returnCode = await _s.DeleteOrder(orderId);

                if (returnCode == "200")
                {
                    Assert.Pass();
                }
                if (returnCode == "208")
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task GetTrendingSuccess()
            {
                Product returnProduct = await _s.GetTrending();

                if (returnProduct != null)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task GetFeaturedSuccess()
            {
                Product returnProduct = await _s.GetFeatured();

                if (returnProduct != null)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task GetAllProductsSuccess()
            {
                List<Product> returnProducts = await _s.GetAllProducts();

                if (returnProducts.Count > 0)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task GetRecommendedSuccess()
            {
                //await Validate();

                List<Product> returnProducts = await _s.GetRecommended();

                Console.WriteLine(returnProducts.Count);

                if (returnProducts.Count > 0)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            [Test]
            public async Task GetRelatedProductSuccess()
            {
                int productId = 1;
                List<Product> returnProducts = await _s.GetRelatedProduct(productId);

                if (returnProducts.Count > 0)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            [Test]
            public async Task PostAddReviewSuccess()
            {
                // lognin
                await Validate();

                //place order
                int productId = 1;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                await _s.PostAddOrder(productId, quantity, deliveryDate);

                
                //write review
                Review testReview = new Review
                {
                    ProdID = productId,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };

                string returnCode = await _s.PostAddReview(testReview);

                Console.WriteLine(returnCode);

                if (returnCode == "200")
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }


            [Test]
            public async Task GetReviews()
            {
                //ensure there is a review
                Review testReview = new Review
                {
                    ProdID = 2,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };

                string returnCode = await _s.PostAddReview(testReview);

                //get reviews

                int productId = 2;
                List<Review> returnReviews = await _s.GetReviews(productId);

                if (returnReviews.Count > 0)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }



            [Test]
            public async Task DeleteReviewSuccess()
            {
                // lognin
                await Validate();

                //place order
                int productId = 1;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                await _s.PostAddOrder(productId, quantity, deliveryDate);


                //write review
                Review testReview = new Review
                {
                    ProdID = productId,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };

                await _s.PostAddReview(testReview);



                //delete review
                ReviewWName testDeleteReview = new ReviewWName
                {
                    CustomerID = 1,
                    ProdID = productId,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };

                string returnCode = await _s.DeleteReview(testDeleteReview);

                if (returnCode == "200")
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }

            [Test]
            public async Task LogOutSuccess()
            {
                await Validate();

                string returnCode = await _s.Logout();


                Console.WriteLine(returnCode);
                //returnCode = returnCode.Replace("\"", "");
                //Console.WriteLine(returnCode);

                //string a = (string)returnCode;
                //Console.WriteLine(a.ToString());




                if (returnCode == "200")
                {
                    Assert.Pass();
                }
                else
                {
                    //Console.WriteLine(a);
                    Assert.Fail();
                }
            }


            [Test]
            public async Task GetCustomersReviewsSuccess()
            {
                // lognin
                await Validate();

                //place order
                int productId = 1;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                await _s.PostAddOrder(productId, quantity, deliveryDate);


                //write review
                Review testReview = new Review
                {
                    ProdID = productId,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };

                await _s.PostAddReview(testReview);

                //get all reviews

                List<ReviewWName> returnReviews = await _s.GetCustomersReviews();

                Console.WriteLine(returnReviews.Count);

                if (returnReviews.Count > 0)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }








        }

    }
}