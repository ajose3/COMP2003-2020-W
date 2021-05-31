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

            // Note: strings will be URL encoded for navigation. Therefore, decode at the receiver.
            //await Shell.Current.GoToAsync($"productdetails?id={((Product)item).Id}");

            await Shell.Current.GoToAsync($"productdetails?id={((Product)item).Id}");
        }

        protected override async void OnQueryConfirmed()
        {
            //await Shell.Current.DisplayAlert("Query Confirmed", null, "OK");
            // take to search page
            await Shell.Current.GoToAsync($"searchPage?query={Query}");
        }
    }
}
