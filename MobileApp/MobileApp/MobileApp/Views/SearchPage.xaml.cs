using MobileApp.Data;
using MobileApp.Models;
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
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();
        }

        //collection view select
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // This works because route names are unique in this application.
            int productId = (e.CurrentSelection.FirstOrDefault() as Product).Id;
            // query product id as all products will need a unqiue id
            await Shell.Current.GoToAsync($"productdetails?id={productId}");
        }
    }
}