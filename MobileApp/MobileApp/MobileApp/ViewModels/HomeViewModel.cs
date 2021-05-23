using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Services;
using MobileApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        WebDataService dataService = new WebDataService();
        public HomeViewModel() 
        {
            Task.Run(async () => await GetTrending());
            Task.Run(async () => await GetFeatured());
            Task.Run(async () => await GetAllProducts()); 
            Task.Run(async () => await GetRecommended());
            //OnPropertyChanged("Products");
            //TrendingToday = dataService.GetTrending();
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public async Task GetRecommended()
        {
            recommended = await dataService.GetRecommended();
            OnPropertyChanged("recommended");
        }
        public async Task GetTrending()
        {
            TrendingToday = await dataService.GetTrending();
            OnPropertyChanged("TrendingToday");
        }
        public async Task GetFeatured()
        {
            Featured = await dataService.GetFeatured();
            OnPropertyChanged("Featured");
        }
        public async Task GetAllProducts()
        {
            products = await dataService.GetAllProducts();
            OnPropertyChanged("products");
            ProductData.Products = products;
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Shell.Current.DisplayAlert("Clicked Search", null, "OK");
        });
        public Product TrendingToday { get; set; }

        public Product Featured { get; set; }

        public List<Product> products { get; private set; }
        public List<Product> recommended { get; private set; }


        private Product selectItem;
        public Product SelectItem
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
                    Shell.Current.GoToAsync($"productdetails?id={productId}");
                    selectItem = null;
                }
            }
        }

        public ICommand GoToProduct => new Command<int>((int productId) =>
        {
            Shell.Current.GoToAsync($"productdetails?id={productId}");
        });


        public ICommand Logout => new Command(async () =>
        {
            //Shell.Current.DisplayAlert("LO","Logging out","OK");
            await dataService.Logout();
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/loginPage");
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