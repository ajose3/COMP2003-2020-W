using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest1
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        public void LoginTestUI()
        {
            //Arrange
            app.Tap("usernameTextbox");
            app.EnterText("Admin1");
            app.DismissKeyboard();
            app.Tap("passwordTextbox");
            app.EnterText("password1");
            app.DismissKeyboard();


            //Act
            app.Tap("loginButton");
            app.WaitForElement("logoutButton");

            //Assert
            bool result = app.Query(e => e.Marked("logoutButton")).Any();

            Assert.IsTrue(result);

        }
    }
}
