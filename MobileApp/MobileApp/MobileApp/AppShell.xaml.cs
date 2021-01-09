using MobileApp.Models;
using MobileApp.ViewModels;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
        void RegisterRoutes()
        {
            //routes.Add("productdetails", typeof(ProductPage));
            Routing.RegisterRoute("productdetails", typeof(ProductPage));
            Routing.RegisterRoute("userdetailspage", typeof(UserDetailsPage));
            Routing.RegisterRoute("checkoutpage", typeof(CheckoutPage));



            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }


        void OnNavigating(object sender, ShellNavigatingEventArgs e)
        {
            // Cancel any back navigation
            //if (e.Source == ShellNavigationSource.Pop)
            //{
            //    e.Cancel();
            //}
        }

        void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("userdetailspage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void ClickedBasketMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("checkoutpage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
