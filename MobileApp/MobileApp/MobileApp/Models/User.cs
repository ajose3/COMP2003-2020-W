using MobileApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string email;
        public string password;
        public string FirstName;
        public string LastName;
        public int Age;
        public string Address;
        public string PhoneNumber;
        public bool Gender;

        //public string Username 
        //{ 
        //    get { return username; } 
        //    set
        //    {
        //        username = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("Username"));
        //    } 
        //}

        //public string Password 
        //{
        //    get
        //    { return password;}
        //    set
        //    {
        //        password = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("Password"));
        //    }
        //}
        //public ICommand SubmitCommand { get; set; }

        public User()
        {
            //SubmitCommand = new Command(OnSubmit);
        }
        public User(string username, string password)
        {
            //SubmitCommand = new Command(OnSubmit);
            this.email = username;
            this.password = password;
        }

        public User(string username, string password, string firstName, string lastName, int age, string address, string phoneNumber, bool gender)
        {
            //SubmitCommand = new Command(OnSubmit);
            this.email = username;
            this.password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
        }

        //    public string OnSubmit()
        //    {
        //        //Move to view model
        //        if (!string.IsNullOrEmpty(username))
        //        {
        //            bool isFound = UserData.searchUser(username,password);
        //            //Contact API


        //            if (isFound)
        //            {
        //                //for testing
        //                TokenData.value = "tokenGoesHere";
        //                //MessagingCenter.Send(this, "LoginAlert", "User found");

        //                return "User Found";
        //            }
        //            else
        //            {
        //                //MessagingCenter.Send(this, "LoginAlert", "User not found");
        //                return "not found";
        //            }

        //        }
        //        return null;
        //    }
    }
}
