using MobileApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class ProductSearchHandler : SearchHandler
    {
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = ProductData.Products
                    .Where(product => product.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<Product>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);
            //await DisplayAlert (Routing.GetRoute(this));

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            //error occurs here find out how to fix
            //await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"ProductPage?name={((Product)item).Name}");
            await Shell.Current.GoToAsync($"productdetails?name={((Product)item).Name}");
            //await Shell.Current.GoToAsync($"productdetails?name={((Product)item).Name}");

            //await Shell.Current.GoToAsync($"ProductPage?name={((Product)item).Name}");

            //await Shell.Current.GoToAsync($"Views\\ProductPage?name={((Product)item).Name}");

            //Views\\ProductPage.xaml
            //await Shell.Current.GoToAsync($"//animals/monkeys/productpage?name={((Product)item).Name}");
        }
    }
}
