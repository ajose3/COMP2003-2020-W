﻿using MobileApp.Models;
using MobileApp.Services;
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
        public CustomerDetailsViewModel()
        {
            Task.Run(async () => await LoadDetails());
        }

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
                email = value;
                OnPropertyChanged("Password");
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

        public ICommand GoToEditPage => new Command(() =>
        {
            Shell.Current.GoToAsync("editDetailsPage");
        });

        public ICommand GoToEditPasswordPage => new Command(() =>
        {
            Shell.Current.GoToAsync("editPasswordPage");
        });

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
