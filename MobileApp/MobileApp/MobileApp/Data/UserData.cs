﻿using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    public static class UserData
    {
        public static IList<User> Users { get; private set; }
        static UserData()
        {
            Users = new List<User>();
            Users.Add(new User
            {
                email = "Bob",
                password = "password"
            });
            Users.Add(new User
            {
                email = "user",
                password = "password"
            });
            Users.Add(new User
            {
                email = "1",
                password = "1"
            });
            Users.Add(new User
            {
                email = "bill",
                password = "password"
            });


        }
        public static bool searchUser(string username, string password)
        {
            foreach (User user in Users)
            {
                if ((user.email == username) && (user.password == password))
                {
                    return true;
                }
            }

            return false;
        }
    }
}