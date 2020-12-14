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
    class ProductDetailsViewModel : INotifyPropertyChanged
    {
        public string ProductName
        {
            set
            {
                Product product = ProductData.Products.FirstOrDefault(m => m.Name == Uri.UnescapeDataString(value));

                if (product != null)
                {
                    Name = product.Name;
                    Description = product.Description;
                    Price = product.Price;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Description");
                    OnPropertyChanged("Price");
                }
            }
        }
        public string Name { get; set; }
        public string Description { get; private set; }
        public int Price { get; private set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
