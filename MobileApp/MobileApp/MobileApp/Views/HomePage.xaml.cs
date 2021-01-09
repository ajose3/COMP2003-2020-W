﻿using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string productName = (e.CurrentSelection.FirstOrDefault() as Product).Name;
            // This works because route names are unique in this application.
            //await Shell.Current.GoToAsync($"productdetails?name={productName}");

            int productId = (e.CurrentSelection.FirstOrDefault() as Product).Id;
            // query product id as all products will need a unqiue id
            await Shell.Current.GoToAsync($"productdetails?id={productId}");
        }


    }
}