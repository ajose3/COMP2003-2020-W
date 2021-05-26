﻿using MobileApp.ViewModels;
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
    public partial class AllReviewsPage : ContentPage
    {
        public AllReviewsPage()
        {
            InitializeComponent();
            BindingContext = new CustomerDetailsViewModel();
        }
    }
}