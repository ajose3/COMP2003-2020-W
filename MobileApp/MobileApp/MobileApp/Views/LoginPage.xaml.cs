using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public User loginModel;
        public LoginPage()
        {
            InitializeComponent();
            loginModel = new User();
            MessagingCenter.Subscribe<User, string>(this, "LoginAlert", (sender, username) =>
            {
                DisplayAlert("LoginResponse", username, "Ok");
                if (TokenData.value !=null)
                {
                    DisplayAlert("Token", TokenData.value, "Ok");
                }
            });
            this.BindingContext = loginModel;

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };
            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                loginModel.SubmitCommand.Execute(null);
            };
        }

        //move to viewModel
        private void GoToRegisterPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("signUpPage");
        }

        private void GoToResetPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("resetPwordPage");
        }
    }
}