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
            Routing.RegisterRoute("resetPwordPage", typeof(ResetPasswordPage));
            Routing.RegisterRoute("signUpPage", typeof(SignUpPage));
            Routing.RegisterRoute("loginPage", typeof(LoginPage));
            Routing.RegisterRoute("editDetailsPage", typeof(EditDetailsPage));
            Routing.RegisterRoute("editPasswordPage", typeof(EditPasswordPage));
            Routing.RegisterRoute("searchPage", typeof(SearchPage));
            Routing.RegisterRoute("ordersPage", typeof(OrdersPage));
            Routing.RegisterRoute("orderDetailsPage", typeof(OrderDetailsPage));
            Routing.RegisterRoute("allReviewsPage", typeof(AllReviewsPage));



            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        //private async void OnMenuItemClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("//LoginPage");
        //}


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

        private async void ClickedResetPasswordMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("resetPwordPage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void ClickedBasketMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("checkoutpage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void ClickedSignUpMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("signUpPage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void ClickedLoginMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("loginPage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }
        private async void ClickedOrdersMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ordersPage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void ClickedDetailsMenuItem(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("userdetailspage");
            // close the menu
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
