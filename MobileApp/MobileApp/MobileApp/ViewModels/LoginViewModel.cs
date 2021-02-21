using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
        }

        public string username { get; set; }
        public string password { get; set; }

        public ICommand LoginCmd => new Command(() =>
        {
            User user = new User(username,password);
            user.OnSubmit();
            Shell.Current.DisplayAlert("LoginResult",user.OnSubmit(),"OK");
            if (TokenData.value !=null)
            {
                Shell.Current.DisplayAlert("Token", TokenData.value, "Ok");
            }
        });
        public ICommand GoSignUpPage => new Command(() =>
        {
            Shell.Current.GoToAsync("signUpPage");
        });
        public ICommand GoResetPage => new Command(() =>
        {
            Shell.Current.GoToAsync("resetPwordPage");
        });

    }
}
