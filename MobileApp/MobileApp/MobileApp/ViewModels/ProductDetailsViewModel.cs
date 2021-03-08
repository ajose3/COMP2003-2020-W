using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    //[QueryProperty("ProductName", "name")]
    // use id rather than name as each id will need to be unique
    [QueryProperty("ProductId", "id")]
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        public ProductDetailsViewModel()
        {
            //ReviewData.getReviews(Id);
            //Reviews = ReviewData.getReviews(Id);
            
        }

        public int ProductId
        {
            set
            {
                Product product = ProductData.Products.FirstOrDefault(m => m.Id == value);

                if (product != null)
                {
                    Id = product.Id;
                    Name = product.Name;
                    Description = product.Description;
                    Price = product.Price;
                    ImageUrl = product.ImageUrl;
                    Stock = product.Stock;
                    //ReviewData.getReviews(Id);
                    Reviews = ReviewData.getReviews(Id);
                    StarColor1 = "Grey";
                    StarColor2 = "Grey";
                    StarColor3 = "Grey";
                    StarColor4 = "Grey";
                    StarColor5 = "Grey";
                    AverageRating();
                    OnPropertyChanged("Id");
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Description");
                    OnPropertyChanged("Price");
                    OnPropertyChanged("ImageUrl");
                    OnPropertyChanged("Stock");
                    OnPropertyChanged("Reviews");
                    OnPropertyChanged("StarColor1");
                    OnPropertyChanged("StarColor2");
                    OnPropertyChanged("StarColor3");
                    OnPropertyChanged("StarColor4");
                    OnPropertyChanged("StarColor5");
                }
            }
        }

        private void AverageRating()
        {
            int count = 0;
            int total = 0;
            float average = 0;
            foreach (var Review in Reviews)
            {
                total += Review.Rating;
                count++;
            }
            if (count != 0)
            {
                average = total / count;

                if (average >= 1)
                {
                    StarColor1 = "Gold";
                }
                if (average >= 2)
                {
                    StarColor1 = "Gold";
                    StarColor2 = "Gold";
                }
                if (average >= 3)
                {
                    StarColor1 = "Gold";
                    StarColor2 = "Gold";
                    StarColor3 = "Gold";
                }
                if (average >= 4)
                {
                    StarColor1 = "Gold";
                    StarColor2 = "Gold";
                    StarColor3 = "Gold";
                    StarColor4 = "Gold";
                }
                if (average >= 5)
                {
                    StarColor1 = "Gold";
                    StarColor2 = "Gold";
                    StarColor3 = "Gold";
                    StarColor4 = "Gold";
                    StarColor5 = "Gold";
                }
            }

            //OnPropertyChanged("StarColor1");
            //OnPropertyChanged("StarColor2");
            //OnPropertyChanged("StarColor3");
            //OnPropertyChanged("StarColor4");
            //OnPropertyChanged("StarColor5");
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; private set; }
        public float Price { get; private set; }
        public string ImageUrl { get; private set; }
        public int Stock { get; set; }
        public List<Review> Reviews { get; set; }
        public string StarColor1 { get; set; }
        public string StarColor2 { get; set; }
        public string StarColor3 { get; set; }
        public string StarColor4 { get; set; }
        public string StarColor5 { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private Product CreateProductFromAttributes()
        {
            return new Product(Id, Name, Description, Price, ImageUrl, Stock);
        }

        public ICommand AddToBasketCmd => new Command(() =>
        {
            Product product = CreateProductFromAttributes();
            Basket.AddProduct(product);
            Shell.Current.DisplayAlert("Product added to basket", null, "OK");
        });

        public ICommand BuyNowCmd => new Command(() =>
        {
            Product product = CreateProductFromAttributes();
            Basket.AddProduct(product);
            Shell.Current.GoToAsync("checkoutpage");
        });

    }
}
