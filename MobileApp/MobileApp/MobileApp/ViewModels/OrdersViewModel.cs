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
            //Orders = OrderData.LoadOrdersAsync();
            //Orders = (List<OrdersGroup>)load;
            OnPropertyChanged("Orders");
        }
        public List<OrdersGroup> Orders { get; set; }

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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
