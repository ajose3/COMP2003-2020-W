using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;


namespace MobileApp.ViewModels
{
    [QueryProperty("ProductName", "name")]
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        public string ProductName
        {
            set
            {
                Product product = ProductData.Products.FirstOrDefault(m => m.Name == Uri.UnescapeDataString(value));

                if (product != null)
                {
                    Id = product.Id;
                    Name = product.Name;
                    Description = product.Description;
                    Price = product.Price;
                    ImageUrl = product.ImageUrl;
                    OnPropertyChanged("Id");
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Description");
                    OnPropertyChanged("Price");
                    OnPropertyChanged("ImageUrl");
                }
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; private set; }
        public string Price { get; private set; }
        public string ImageUrl { get; private set; }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
