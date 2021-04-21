﻿using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        public OrdersViewModel()
        {
            Orders = OrderData.LoadOrders();
            OnPropertyChanged("Orders");

        }
        //public void load()
        //{
        //    OrderData.LoadOrders();
        //}
        public List<OrdersGroup> Orders { get; set; }


        public ICommand load => new Command(() =>
        {
            Orders = OrderData.LoadOrders();
            OnPropertyChanged("Orders");
        });
        //public ICommand GoToGroupedCommand => new Command(() =>
        //{
        //    //Go to orders details Page
        //    Shell.Current.GoToAsync("orderDetailsPage");

        //});

        OrdersGroup selectedOrder;
        public OrdersGroup SelectedOrder
        {
            get => selectedOrder;
            set
            {
                if (value != null)
                {
                    DateTime date = value.OrderDate;
                    string dateString = date.ToString();
                    //Application.Current.MainPage.DisplayAlert("Selected", value.OrderDate.ToString(), "Ok");
                    //Shell.Current.GoToAsync($"orderDetailsPage?date={date}");
                    Shell.Current.GoToAsync($"orderDetailsPage?date={dateString}");
                    value = null;
                }
                selectedOrder = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
