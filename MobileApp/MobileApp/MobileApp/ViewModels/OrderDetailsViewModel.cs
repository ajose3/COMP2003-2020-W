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
    [QueryProperty("OrderDate", "date")]
    class OrderDetailsViewModel : INotifyPropertyChanged
    {
        public OrderDetailsViewModel()
        {
            RemoveFromOrder = new Command<Order>(RemoveOrder);
        }
        public Command<Order> RemoveFromOrder { get; }


        public DateTime orderDate { get; set; }

        public List<Order> OrdersInGroup { get; set; }

        //public ICommand RemoveFromOrder => new Command<Order>((Order order) =>
        //{
        //    OrderData.removeOrder(order);

        //    OrdersInGroup = OrderData.loadOrdersByDate(orderDate);
        //    OnPropertyChanged("OrdersInGroup");
        //});
        public void RemoveOrder(Order order)
        {
            OrderData.removeOrder(order);

            OrdersInGroup = OrderData.loadOrdersByDate(orderDate);
            OnPropertyChanged("OrdersInGroup");
        }
        public string OrderDate
        {
            //get => orderDate;
            set
            {
                orderDate = Convert.ToDateTime(value);
                OnPropertyChanged("orderDate");

                OrdersInGroup = OrderData.loadOrdersByDate(orderDate);
                OnPropertyChanged("OrdersInGroup");
            }
        }

        private Order selectItem;
        public Order SelectItem
        {
            get
            {
                return selectItem;
            }

            set
            {
                if (selectItem != value)
                {
                    selectItem = value;
                    OrderData.toggleBtn(selectItem);

                    OrdersInGroup = OrderData.loadOrdersByDate(orderDate);
                    OnPropertyChanged("OrdersInGroup");
                }
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
