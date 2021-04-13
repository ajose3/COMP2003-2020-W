using MobileApp.Data;
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



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
