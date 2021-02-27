using MobileApp.Models;
using MobileApp.Services;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        public ICommand RegisterCmd => new Command(async () =>
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
            if (returnValue == "true")
            {
                Shell.Current.GoToAsync("successPage");
            }
        });

    }
}
