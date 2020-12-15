using MobileApp.Models;
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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            var list = new List<string>
            {
                "Hey",
                "Did you check the",
                "The CarouselView",
                "In Xamarin.Forms?"
            };
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string productName = (e.CurrentSelection.FirstOrDefault() as Product).Name;
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"productdetails?name={productName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/monkeys/monkeydetails?name={monkeyName}");
        }


    }
}