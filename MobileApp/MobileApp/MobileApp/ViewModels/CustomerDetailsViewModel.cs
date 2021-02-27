using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class CustomerDetailsViewModel : INotifyPropertyChanged
    {

        public string Email { get; set; }
        //public string Email
        //{
        //    get { return email; }
        //    set
        //    {
        //        email = value;
        //        OnPropertyChanged(nameof(Email));
        //    }
        //}
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }


        public ICommand PopulateDetails => new Command(async () =>
        {
            WebDataService webDataService = new WebDataService();
            User user = await webDataService.GetCustomerDetails();
            Email = user.email;
            Password = user.password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Age = user.Age;
            Address = user.Address;
            PhoneNumber = user.PhoneNumber;
            if (user.Gender == true)
            {
                Gender = "Male";
            }
            else
            {
                Gender = "Female";
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
