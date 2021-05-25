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
using MobileApp.Services;

namespace MobileApp.ViewModels
{
    [QueryProperty("ProductId", "id")]
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        WebDataService dataService = new WebDataService();
        public ProductDetailsViewModel()
        {
            SelectRatingCommand = new Command<string>(SelectRating);
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
                    //Reviews = ReviewData.getReviews(Id);
                    OnPropertyChanged("Id");
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Description");
                    OnPropertyChanged("Price");
                    OnPropertyChanged("ImageUrl");
                    OnPropertyChanged("Stock");
                    Task.Run(async () => await GetRecommended());
                    Task.Run(async () => await LoadReviews());
                }
            }
        }

        public async Task LoadReviews()
        {
            Reviews = await dataService.GetReviews(Id);
            OnPropertyChanged("Reviews");

            AverageRating();
            OnPropertyChanged("StarColor1");
            OnPropertyChanged("StarColor2");
            OnPropertyChanged("StarColor3");
            OnPropertyChanged("StarColor4");
            OnPropertyChanged("StarColor5");
        }

        public async Task GetRecommended()
        {
            recommended = await dataService.GetRelatedProduct(Id);
            OnPropertyChanged("recommended");
        }

        private void AverageRating()
        {
            int count = 0;
            int total = 0;
            float average = 0;
            StarColor1 = "Gray";
            StarColor2 = "Gray";
            StarColor3 = "Gray";
            StarColor4 = "Gray";
            StarColor5 = "Gray";
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
            OnPropertyChanged("StarColor1");
            OnPropertyChanged("StarColor2");
            OnPropertyChanged("StarColor3");
            OnPropertyChanged("StarColor4");
            OnPropertyChanged("StarColor5");
        }


        public List<Product> recommended { get; set; }
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

        public string RTitle { get; set; }
        public string RDescription { get; set; }
        public int RRating { get; set; }
        public string RStarColor1 { get; set; }
        public string RStarColor2 { get; set; }
        public string RStarColor3 { get; set; }
        public string RStarColor4 { get; set; }
        public string RStarColor5 { get; set; }

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

        public ICommand AddToBasketCmd => new Command(async () =>
        {
            Product product = CreateProductFromAttributes();
            //Basket.AddProduct(product);
            await BasketService.AddProduct(product);
            await Shell.Current.DisplayAlert("Product added to basket", null, "OK");
        });

        public ICommand BuyNowCmd => new Command(async () =>
        {
            Product product = CreateProductFromAttributes();
            //Basket.AddProduct(product);
            await BasketService.AddProduct(product);
            await Shell.Current.GoToAsync("checkoutpage");
        });

        public ICommand WriteReview => new Command(async () =>
        {
            if (TokenData.value.Length > 3)
            {
                //ReviewData.writeReview(Id, RRating, RTitle, RDescription);

                Review r = new Review(Id, RRating, RTitle, RDescription);

                string response = await dataService.PostAddReview(r);

                await Task.Run(async () => await LoadReviews());

                //Reviews = ReviewData.getReviews(Id);
                OnPropertyChanged("Reviews");
                clearReview();

                if (response == "208")
                {
                    await Shell.Current.DisplayAlert("Oops", "Your review couldn't be added. \n It appears you don't own this item.", "OK");
                }
            }
            else
            {
                await Shell.Current.GoToAsync("/loginPage");
            }


        });
        public ICommand ResetWriteCommand => new Command(() =>
        {
            clearReview();
        });

        public Command<string> SelectRatingCommand { get; }

        void SelectRating(string rating)
        {
            int Rating = Int32.Parse(rating);
            RRating = Int32.Parse(rating);

            RStarColor1 = "Gray";
            RStarColor2 = "Gray";
            RStarColor3 = "Gray";
            RStarColor4 = "Gray";
            RStarColor5 = "Gray";
            if (Rating >= 1)
            {
                RStarColor1 = "Gold";
            }
            if (Rating >= 2)
            {
                RStarColor1 = "Gold";
                RStarColor2 = "Gold";
            }
            if (Rating >= 3)
            {
                RStarColor1 = "Gold";
                RStarColor2 = "Gold";
                RStarColor3 = "Gold";
            }
            if (Rating >= 4)
            {
                RStarColor1 = "Gold";
                RStarColor2 = "Gold";
                RStarColor3 = "Gold";
                RStarColor4 = "Gold";
            }
            if (Rating >= 5)
            {
                RStarColor1 = "Gold";
                RStarColor2 = "Gold";
                RStarColor3 = "Gold";
                RStarColor4 = "Gold";
                RStarColor5 = "Gold";
            }
            OnPropertyChanged("RStarColor1");
            OnPropertyChanged("RStarColor2");
            OnPropertyChanged("RStarColor3");
            OnPropertyChanged("RStarColor4");
            OnPropertyChanged("RStarColor5");
        }
        public void clearReview()
        {
            RRating = 0;
            RTitle = "";
            RDescription = "";
            RStarColor1 = "Gray";
            RStarColor2 = "Gray";
            RStarColor3 = "Gray";
            RStarColor4 = "Gray";
            RStarColor5 = "Gray";

            OnPropertyChanged("RRating");
            OnPropertyChanged("RTitle");
            OnPropertyChanged("RDescription");
            OnPropertyChanged("RStarColor1");
            OnPropertyChanged("RStarColor2");
            OnPropertyChanged("RStarColor3");
            OnPropertyChanged("RStarColor4");
            OnPropertyChanged("RStarColor5");
        }

        public Review selectReview { get; set; }
        public Review SelectReview
        {
            get
            {
                return selectReview;
            }

            set
            {
                if (selectReview != value)
                {
                    if (value != null)
                    {
                        selectReview = value;
                        OnPropertyChanged("selectReview");
                    }
                }
            }
        }

    }
}
