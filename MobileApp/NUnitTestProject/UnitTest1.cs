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

            //[Test]
            //public async Task Login_LoginSuccess()
            //{
            //    _vm.username = "test@email.com";
            //    _vm.password = "password1";
            //    if (_vm.LoginCmd.CanExecute(null))
            //    {
            //        //Console.WriteLine("Can execute");
            //        _vm.LoginCmd.Execute(null);
            //    }
            //    else
            //    {
            //        //Console.WriteLine("Cannot execute");
            //    }

            //    Console.WriteLine(TokenData.value);

            //    if (TokenData.value == "")
            //    {
            //        Assert.Pass();
            //    }
            //    Assert.Fail();
            //}
        }
        [TestFixture]
        public class WebDataServiceTest
        {
            WebDataService _s;
            [SetUp]
            public void Setup()
            {
                _s = new WebDataService();
            }


            [Test]
            public async Task PostRegisterCustomerSuccess()
            {
                User testUser = new User
                {
                    Address = "25 london eye",
                    Age = 21,
                    email = "randomemail@spam.com",
                    FirstName = "Jack",
                    LastName = "Potter",
                    password = "password1234",
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
                User testUser = new User
                {
                    Address = "28 london eye",
                    Age = 12,
                    email = "randomemail2@spam.com",
                    FirstName = "Jack",
                    LastName = "Potter",
                    password = "password1234",
                    PhoneNumber = "084876537342"
                };

                string returnCode = await _s.PutUpdateCustomer(testUser);

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
            public async Task PutChangePasswordSuccess()
            {
                string newPassword = "password1";

                string returnCode = await _s.PutChangePassword(newPassword);

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
                int productId = 10;
                int quantity = 1;
                var rand = new Random();
                DateTime deliveryDate = DateTime.Now.AddDays(rand.Next(31));

                string returnCode = await _s.PostAddOrder(productId, quantity, deliveryDate);

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
                List<Order> returnOrders = await _s.GetOrders();

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
                int orderId = 1;
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
                List<Product> returnProducts = await _s.GetRecommended();

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
                Review testReview = new Review
                {
                    CustomerID = 1,
                    ProdID = 1,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5

                };

                string returnCode = await _s.PostAddReview(testReview);

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
                ReviewWName testReview = new ReviewWName
                {
                    CustomerID = 1,
                    ProdID = 1,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };

                string returnCode = await _s.DeleteReview(testReview);

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
                string returnCode = await _s.Logout();

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
            public async Task GetCustomersReviewsSuccess()
            {
                //ensure customer has review
                Review testReview = new Review
                {
                    ProdID = 2,
                    Title = "TestReview",
                    Desciption = "Test Description",
                    Rating = 5
                };


                List<ReviewWName> returnReviews = await _s.GetCustomersReviews();

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