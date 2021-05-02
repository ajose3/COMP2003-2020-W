using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Services;
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
        public List<BasketProduct> basket { get; set; }
        public int NumItems { get; set; }
        public float TotalPrice { get; set; }
        public Command<Product> RemoveProductCommand { get; }

        public async void loadBasket()
        {
            var abasket = await BasketService.GetProducts();
            basket = (List<BasketProduct>)abasket;
            NumItems = GetNumItem();
            TotalPrice = GetTotalPrice();
            OnPropertyChanged("basket");
            OnPropertyChanged("NumItems");
            OnPropertyChanged("TotalPrice");
        }
        async void RemoveProductTask(Product product)
        {
            if (product == null)
            {
                return;
            }
            await BasketService.RemoveProduct(product);
            loadBasket();
        }

        public ICommand RefreshCommand { get; }
        public ICommand ClickedPayBtnCmd => new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Clicked Pay", null, "OK");
            // for each product in basket
            loadBasket();
            foreach (var Product in basket)
            {
                ProductData.Products.Where(i => i.Id == Product.Id).FirstOrDefault().Stock -= Product.Quantity;

                Product product = new Product(Product);

                for (int i = 0; i < Product.Quantity; i++)
                {
                    OrderData.AddToOrder(Product);
                }
            }

            await BasketService.ClearProducts();

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
            loadBasket();
            // Stop refreshing
            IsRefreshing = false;
        }

        private BasketProduct selectItem;
        public BasketProduct SelectItem
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

                    int productId = selectItem.Id;
                    // query product id as all products will need a unqiue id
                    Shell.Current.GoToAsync($"productdetails?id={productId}");
                    selectItem = null;
                }
            }
        }

        public float GetTotalPrice()
        {
            float totalPrice = 0;

            foreach (var Product in basket)
            {
                if (Product.Quantity > 1)
                {
                    totalPrice += Product.Price * Product.Quantity;
                }
                else
                {
                    totalPrice += Product.Price;
                }
            }
            return totalPrice;
        }

        public int GetNumItem()
        {
            int num = 0;

            foreach (var Product in basket)
            {
                if (Product.Quantity > 1)
                {
                    num += Product.Quantity;
                }
                else
                {
                    num += 1;
                }
            }
            return num;
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
