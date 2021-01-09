using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MobileApp.Data;
using MobileApp.Models;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class CheckoutViewModel
    {
        public int NumItems => Basket.Products.Count;
        public float TotalPrice => Basket.GetTotalPrice();

        public CheckoutViewModel()
        {
        }

        public void checkoutFunc()
        {

        }

        public ICommand ClickedPayBtnCmd => new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Clicked Pay", null, "OK");
            // for each product in basket
            foreach (var Product in Basket.Products)
            {
                // decrease stock for product in ProductData
                ProductData.Products.Where(i => i.Id == Product.Id).FirstOrDefault().Stock -= 1;
                //Shell.Current.DisplayAlert("Updated stock", string.Format("{0}", ProductData.Products.Where(i => i.Id == Product.Id).FirstOrDefault().Stock), "OK");
            }

            // clear basket
            Basket.Clear();

            await Shell.Current.Navigation.PopToRootAsync();
        });

    }
}
