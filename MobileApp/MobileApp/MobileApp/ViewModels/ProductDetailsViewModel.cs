using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Windows.Input;

namespace MobileApp.ViewModels
{
    //[QueryProperty("ProductName", "name")]
    // use id rather than name as each id will need to be unique
    [QueryProperty("ProductId", "id")]
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {

        public int ProductId
        {
            set
            {
                Product product = ProductData.Products.FirstOrDefault(m => m.Id == value);

                if (product != null)
                {
                    Id = product.Id;
                    Name = product.Name;
                    Description = product.Description;
                    Price = product.Price;
                    ImageUrl = product.ImageUrl;
                    Stock = product.Stock;
                    OnPropertyChanged("Id");
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Description");
                    OnPropertyChanged("Price");
                    OnPropertyChanged("ImageUrl");
                    OnPropertyChanged("Stock");
                }

            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; private set; }
        public float Price { get; private set; }
        public string ImageUrl { get; private set; }
        public int Stock { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private Product CreateProductFromAttributes()
        {
            return new Product(Id, Name, Description, Price, ImageUrl, Stock);
        }

        public ICommand AddToBasketCmd => new Command(() =>
        {
            Product product = CreateProductFromAttributes();
            Basket.AddProduct(product);
            Shell.Current.DisplayAlert("Product added to basket", null, "OK");
        });

        public ICommand BuyNowCmd => new Command(() =>
        {
            Product product = CreateProductFromAttributes();
            Basket.AddProduct(product);
            Shell.Current.GoToAsync("checkoutpage");
        });

    }
}
