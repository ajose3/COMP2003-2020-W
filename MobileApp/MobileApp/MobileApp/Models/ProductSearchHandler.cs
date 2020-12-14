using MobileApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Models
{
    class ProductSearchHandler : SearchHandler
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

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            //error occurs here find out how to fix
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"productpage?name={((Product)item).Name}");
        }
    }
}
