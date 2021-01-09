using System;
using System.Collections.Generic;
using MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        public CheckoutPage()
        {
            InitializeComponent();
            BindingContext = new CheckoutViewModel();
        }

        public void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
