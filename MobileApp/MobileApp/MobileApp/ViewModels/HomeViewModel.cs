using System;
using System.Windows.Input;
using MobileApp.Data;
using MobileApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Ebazon";

            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Shell.Current.DisplayAlert("Clicked Search", null, "OK");
        });

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
                    // query product id as all products will need a unqiue id
                    Shell.Current.GoToAsync($"productdetails?id={productId}");
                    //((CollectionView)sender).SelectedItem = null;
                    selectItem = null;
                }
            }
        }
    }
}