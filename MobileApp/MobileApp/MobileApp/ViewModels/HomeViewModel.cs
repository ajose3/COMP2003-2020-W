using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MobileApp.Data;
using MobileApp.Models;
using MobileApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel() 
        {
            Featured = ProductData.GetFeatured();
            OnPropertyChanged("Featured");
            TrendingToday = ProductData.GetTrending();
            OnPropertyChanged("TrendingToday");
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Shell.Current.DisplayAlert("Clicked Search", null, "OK");
        });
        public Product TrendingToday { get; set; }

        public Product Featured { get; set; }

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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}