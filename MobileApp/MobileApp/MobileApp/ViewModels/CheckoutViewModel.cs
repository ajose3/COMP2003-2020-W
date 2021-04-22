using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MobileApp.Data;
using MobileApp.Models;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        //public int NumItems => Basket.Products.Count;
        //public float TotalPrice => Basket.GetTotalPrice();
        public CheckoutViewModel()
        {
            RemoveProductCommand = new Command<Product>(RemoveProductTask);
            RefreshCommand = new Command(ExecuteRefreshCommand);
            loadBasket();
            OnPropertyChanged("basket");
        }
        //public List<Product> basket { get; set; }
        public List<CheckOutProduct> basket { get; set; }

        public int NumItems { get; set; }
        public float TotalPrice { get; set; }

        public Command<Product> RemoveProductCommand { get; }

        public void loadBasket()
        {
            //basket = Basket.loadBasket();
            basket = Basket.loadNewBasket();
            //NumItems = Basket.Products.Count;
            NumItems = Basket.GetNumItem();
            TotalPrice = Basket.GetTotalPrice();
            OnPropertyChanged("basket");
            OnPropertyChanged("NumItems");
            OnPropertyChanged("TotalPrice");
        }
        void RemoveProductTask(Product product)
        {
            if (product == null)
            {
                return;
            }
            Basket.RemoveProduct(product);
            loadBasket();
            //Tasks = TaskMockData.LoadTasks();
            //OnPropertyChanged("Tasks");
        }

        public void checkoutFunc()
        {

        }
        public ICommand RefreshCommand { get; }
        public ICommand ClickedPayBtnCmd => new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Clicked Pay", null, "OK");
            // for each product in basket
            //foreach (var Product in Basket.Products)
            //{
            //    // decrease stock for product in ProductData
            //    ProductData.Products.Where(i => i.Id == Product.Id).FirstOrDefault().Stock -= 1;
            //    //Shell.Current.DisplayAlert("Updated stock", string.Format("{0}", ProductData.Products.Where(i => i.Id == Product.Id).FirstOrDefault().Stock), "OK");
            //    //  add product to orders
            //    OrderData.AddToOrder(Product);
            //    //OrderData.LoadOrders();
            //}
            foreach (var Product in Basket.CheckProds)
            {
                ProductData.Products.Where(i => i.Id == Product.Id).FirstOrDefault().Stock -= 1*Product.Quantity;

                Product product = new Product(Product);

                for (int i = 0; i < Product.Quantity; i++)
                {
                    OrderData.AddToOrder(Product);
                }
            }



            // clear basket
            Basket.Clear();

            await Shell.Current.Navigation.PopToRootAsync();
        });

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
        void ExecuteRefreshCommand()
        {
            //basket = Basket.loadBasket();
            basket = Basket.loadNewBasket();
            OnPropertyChanged("basket");

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
