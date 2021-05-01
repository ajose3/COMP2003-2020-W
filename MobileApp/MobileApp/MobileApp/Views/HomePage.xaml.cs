using MobileApp.Interfaces;
using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
            //SearchBarTextChanged += HandleSearchBarTextChanged; //Another approach to search (unfinished)
        }       


        //collection view select
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((CollectionView)sender).SelectedItem = null;
        }

        //handle carousel view tap
        async void CarouselItem_Tapped(object sender, EventArgs e)
        {
            var i = sender as StackLayout;
            var productId2 = i.FindByName<Label>("ProductID").Text;
            await Shell.Current.GoToAsync($"productdetails?id={productId2}");
        }
    }
}