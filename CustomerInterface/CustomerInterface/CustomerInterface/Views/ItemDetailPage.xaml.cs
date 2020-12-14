using CustomerInterface.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CustomerInterface.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}