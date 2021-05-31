﻿using MobileApp.Data;
using MobileApp.Models;
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
    public class OrdersViewModel : INotifyPropertyChanged
    {
        public OrdersViewModel()
        {
            //Orders = OrderData.LoadOrdersAsync();
            //Orders = (List<OrdersGroup>)load;
            RefreshCommand = new Command(ExecuteRefreshCommand);
            Task.Run(async () => await LoadDetails());
            OnPropertyChanged("Orders");
        }
        public List<OrdersGroup> Orders { get; set; }

        public async Task LoadDetails()
        {
            Orders = await OrderData.LoadOrdersAsync();
            OnPropertyChanged("Orders");
        }
        public ICommand RefreshCommand { get; }


        public ICommand load => new Command(async () =>
        {
            Orders = await OrderData.LoadOrdersAsync();
            OnPropertyChanged("Orders");
        });

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
                    Shell.Current.GoToAsync($"orderDetailsPage?date={dateString}");
                    value = null;
                }
                selectedOrder = value;
                OnPropertyChanged();
            }
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        async void ExecuteRefreshCommand()
        {
            await LoadDetails();
            // Stop refreshing
            IsRefreshing = false;
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
