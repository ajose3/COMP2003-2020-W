using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Services;
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

        public ICommand LoginCmd => new Command(async () =>
        {
            User user = new User(username,password);
            //user.OnSubmit();

            WebDataService webDataService = new WebDataService();
            await webDataService.GetValidateCustomer(user);
            //var result = await webDataService.PutUpdateCustomer(user);
            //Shell.Current.DisplayAlert("LoginResult",user.OnSubmit(),"OK");
            if (TokenData.value !=null || TokenData.value != "0")
            {
                await Shell.Current.GoToAsync("successPage");
                await Shell.Current.DisplayAlert("Token", TokenData.value, "Ok");
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
