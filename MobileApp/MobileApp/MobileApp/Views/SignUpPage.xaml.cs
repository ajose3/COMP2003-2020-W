using MobileApp.ViewModels;
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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel();

            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };
            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordConfirmEntry.Focus();
            };
            passwordConfirmEntry.Completed += (object sender, EventArgs e) =>
            {
                firstNameEntry.Focus();
            };
            firstNameEntry.Completed += (object sender, EventArgs e) =>
            {
                lastNameEntry.Focus();
            };
            lastNameEntry.Completed += (object sender, EventArgs e) =>
            {
                ageEntry.Focus();
            };
            //ageEntry.Completed += (object sender, EventArgs e) =>
            //{
            //    gender.Focus();
            //};
            gender.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                addressEntry.Focus();
            };
            addressEntry.Completed += (object sender, EventArgs e) =>
            {
                phoneNumberEntry.Focus();
            };
            phoneNumberEntry.Completed += (object sender, EventArgs e) =>
            {
            };
        }
    }
}