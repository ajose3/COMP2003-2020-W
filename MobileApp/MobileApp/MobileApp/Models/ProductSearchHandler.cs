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
            //await Task.Delay(1000);

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            //await Shell.Current.GoToAsync($"productdetails?id={((Product)item).Id}");

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"productdetails?id={((Product)item).Id}");


        }
    }
}
