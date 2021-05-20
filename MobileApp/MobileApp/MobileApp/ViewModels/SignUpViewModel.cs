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
    public class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel()
        {
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        public ICommand RegisterCmd => new Command(async () =>
        {
            if (Email != null || Password != null || FirstName != null || LastName != null || Age != 0 || Address != null || PhoneNumber != null)
            {
                if (Password == PasswordConfirm)
                {
                    bool genderBool;
                    if (Gender == "Male")
                    {
                        genderBool = true;
                    }
                    else
                    {
                        genderBool = false;
                    }

                    User user = new User(Email, Password, FirstName, LastName, Age, Address, PhoneNumber, genderBool);

                    WebDataService webDataService = new WebDataService();
                    string returnValue = await webDataService.PostRegisterCustomer(user);
                    if (returnValue == "200")
                    {
                        //await Shell.Current.GoToAsync("successPage");
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}/loginPage");
                    }
                    else if(returnValue == "208")
                    {
                        await Shell.Current.DisplayAlert("Opps", "An account already exists with this email address", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Opps", "Something Went wrong please try again", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Opps", "Sorry your passwords don't match", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Opps", "Please fill all the fields", "OK");
            }            
        });

    }
}
