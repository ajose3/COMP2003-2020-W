using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    [QueryProperty("OrderDate", "date")]
    class OrderDetailsViewModel : INotifyPropertyChanged
    {
        //public int OrderDate;
        public DateTime orderDate { get; set; }


        //public string test { get; set; }
        public List<Order> OrdersInGroup { get; set; }

        public string OrderDate
        {
            //get => orderDate;
            set
            {
                orderDate = Convert.ToDateTime(value);
                //orderDate = value.OrderDate;
                OnPropertyChanged("orderDate");

                OrdersInGroup = OrderData.loadOrdersByDate(orderDate);
                OnPropertyChanged("OrdersInGroup");
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
