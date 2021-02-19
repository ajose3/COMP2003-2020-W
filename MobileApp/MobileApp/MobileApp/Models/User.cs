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
        public string username;
        public string Username 
        { 
            get { return username; } 
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            } 
        }
        public string password;
        public string Password 
        {
            get
            { return password;}
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { get; set; }

        public User()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public void OnSubmit()
        {
            //Move to view model
            if (!string.IsNullOrEmpty(username))
            {
                bool isFound = UserData.searchUser(username,password);
                //Contact API


                if (isFound)
                {
                    //for testing
                    TokenData.value = "tokenGoesHere";
                    MessagingCenter.Send(this, "LoginAlert", "User found");
                    /////
                }
                else
                {
                    MessagingCenter.Send(this, "LoginAlert", "User not found");
                }

            }
        }
    }
}
