using MobileApp.Interfaces;
using MobileApp.Models;
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
            //SearchBarTextChanged += HandleSearchBarTextChanged; //Another approach to search (unfinished)
        }

        /*//Another approach to Search handlers (unfinished)
        public event EventHandler<string> SearchBarTextChanged;

        void ISearchPage.OnSearchBarTextChanged(string text) => SearchBarTextChanged?.Invoke(this, text);

        void HandleSearchBarTextChanged(object sender, string searchBarText)
        {
            //Logic to handle updated search bar text
        } */

        


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