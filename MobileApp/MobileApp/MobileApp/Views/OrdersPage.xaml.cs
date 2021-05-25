using MobileApp.Data;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        
        public OrdersPage()
        {
            InitializeComponent();
            BindingContext = new OrdersViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string tokenValues = TokenData.value;
            if (tokenValues == null)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}/loginPage");
            }
            else if (tokenValues.Length <= 3)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}/loginPage");
            }
        }
    }
}