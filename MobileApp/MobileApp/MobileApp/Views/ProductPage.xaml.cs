﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;
using MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
            BindingContext = new ProductDetailsViewModel();
        }
        uint duration = 100;
        double openY = (Device.RuntimePlatform == "Android") ? 20 : 60;
        double lastPanY = 0;
        bool isBackdropTapEnabled = true;

        //handle carousel view tap
        async void CarouselItem_Tapped(object sender, EventArgs e)
        {
            var i = sender as StackLayout;
            var productId2 = i.FindByName<Label>("ProductID").Text;
            await Shell.Current.GoToAsync($"productdetails?id={productId2}");
        }

        async void WriteReview_Clicked(object sender, EventArgs e)
        {
            if (Backdrop.Opacity == 0)
            {
                await OpenDrawer();
            }
            else
            {
                await CloseDrawer();
            }
        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isBackdropTapEnabled)
            {
                await CloseDrawer();
            }
        }

        async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                isBackdropTapEnabled = false;
                lastPanY = e.TotalY;
                Debug.WriteLine($"Running: {e.TotalY}");
                if (e.TotalY > 0)
                {
                    BottomToolbar.TranslationY = openY + e.TotalY;
                }
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                if (lastPanY < 110)
                {
                    await OpenDrawer();
                }
                else
                {
                    await CloseDrawer();
                }
                isBackdropTapEnabled = true;
            }
        }
        async Task OpenDrawer()
        {
            await Task.WhenAll
            (
                Backdrop.FadeTo(1,length: duration),
                BottomToolbar.TranslateTo(0,openY, length: duration, easing: Easing.SinIn)
            );
            Backdrop.InputTransparent = false;
        }

        async Task CloseDrawer()
        {
            await Task.WhenAll
            (
                Backdrop.FadeTo(0, length: duration),
                BottomToolbar.TranslateTo(0, 540, length: duration, easing: Easing.SinIn)
            );
            Backdrop.InputTransparent = true;
        }

        async void DoneReview_Clicked(object sender, EventArgs e)
        {
            await CloseDrawer();
        }
    }
}