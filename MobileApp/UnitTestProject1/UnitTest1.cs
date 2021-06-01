/*using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Services;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        *//*[TestMethod]
        public async Task RetrieveValue_AsynchronousSuccess_Adds42()
        {
            var service = new Mock<IMyService>();
            service.Setup(x => x.GetAsync()).Returns(async () =>
            {
                await Task.Yield();
                return 5;
            });
            var system = new SystemUnderTest(service.Object);
            var result = await system.RetrieveValueAsync();
            Assert.AreEqual(47, result);
        }*/

        /*   public void buildUserTest()
           {

               CustomerDetailsViewModel testVariable = new CustomerDetailsViewModel();
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

               using (var sw = new StringWriter()) 
               {
                   var result = testVariable.buildUser();

                   Assert.AreEqual(testUser, result); 
               }

           }*/



        /*  BasketProduct testBasket = new BasketProduct()
          {
              Id = 1,
              Name = "",
              Description = "",
              Price = 20,
              ImageUrl = "",
              Stock = 10,
              Quantity = 5,
          };

          CheckoutViewModel testModel = new CheckoutViewModel();
          */
        /*  User registerUser = new User()
          {
              FirstName = "robbie",
              LastName = "savage",
              email = "thisisemail@mail.com",
              password = "google",
              Age = 44,
              Gender = true,
              Address = "address string 76",
              PhoneNumber = "8407484287",

          };
  *//*

        User testUserValidated = new User()
        {
            FirstName = "aaa",
            LastName = "sd",
            email = "Admin1",
            password = "password1",
            Age = 34,
            Gender = true,
            Address = "address string 5",
            PhoneNumber = "0145234",

        };

        WebDataService testDataService = new WebDataService();
       
        

        [TestMethod]
        public void TestTokenValue()
        {

            testDataService.GetValidateCustomer(testUserValidated);

            User mockUser = new User()
            {
                FirstName = "aaa",
                LastName = "sd",
                email = "Admin1",
                password = "password1",
                Age = 34,
                Gender = true,
                Address = "address string 5",
                PhoneNumber = "0145234",

            };

            //mock user and testUserValidated have exact same details but testUser has been validated so token value is therefore different object
            Assert.AreNotEqual(mockUser, testUserValidated);

        }

        [TestMethod]
        public void TestRegisterCustomer()
        {
            testDataService.GetValidateCustomer(testUserValidated);

            TokenData.value.Length;
        }

    }
}*/
