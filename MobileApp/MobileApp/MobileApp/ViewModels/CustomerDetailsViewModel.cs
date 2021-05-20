using MobileApp.Models;
using MobileApp.Services;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class CustomerDetailsViewModel : INotifyPropertyChanged
    {
        WebDataService dataService = new WebDataService();
        public CustomerDetailsViewModel()
        {
            Task.Run(async () => await LoadDetails());
        }
        //User user { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
        public string newPasswordConfirm;
        public string NewPasswordConfirm
        {
            get { return newPasswordConfirm; }
            set
            {
                newPasswordConfirm = value;
                OnPropertyChanged("NewPasswordConfirm");
            }
        }
        public string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }
        public string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public async Task LoadDetails()
        {
            WebDataService webDataService = new WebDataService();
            User user = await webDataService.GetCustomerDetails();
            OnPropertyChanged("user");
            Email = user.email;
            //Password = (user.password).ToString();
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
        }

        public User buildUser() 
        {
            bool gen = false;
            if(gender == "Male")
            {
                gen = true;
            }
            User user = new User(email, Password, FirstName, LastName, Age, Address, PhoneNumber, gen);

            return user;
        }

        public ICommand GoToEditPage => new Command(() =>
        {
            Shell.Current.GoToAsync("editDetailsPage");
        });

        public ICommand GoToEditPasswordPage => new Command(() =>
        {
            Shell.Current.GoToAsync("editPasswordPage");
        });

        public ICommand updateUser => new Command(async () =>
        {
            await dataService.PutUpdateCustomer(buildUser());
            await Shell.Current.DisplayAlert("Changed", "Changed Details", "Ok");
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        });

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ChangePassword => new Command(async () =>
        {
            if (newPassword == newPasswordConfirm)
            {
                //dataService.PutChangePassword(password, newPassword);
                await dataService.PutChangePassword(newPassword);
            }
            else
            {
                await Shell.Current.DisplayAlert("Oops", "Your new passwords don't match", "Ok");
            }
        });

    }
}
