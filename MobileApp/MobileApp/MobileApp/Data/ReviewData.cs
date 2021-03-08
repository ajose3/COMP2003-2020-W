using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    public static class ReviewData
    {
        public static IList<Review> AllReviews { get; private set; }
        public static IList<Review> Reviews { get; private set; }

        //mock data store
        static ReviewData()
        {
            AllReviews = new List<Review>();

            AllReviews.Add(new Review
            {
                ProdID = 1,
                CustomerID = 1,
                Rating = 4,
                Title = "This it the title",
                Desciption = "This is the reviews description"
            });

            AllReviews.Add(new Review
            {
                ProdID = 1,
                CustomerID = 2,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
            AllReviews.Add(new Review
            {
                ProdID = 2,
                CustomerID = 5,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
            AllReviews.Add(new Review
            {
                ProdID = 3,
                CustomerID = 1,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
            AllReviews.Add(new Review
            {
                ProdID = 3,
                CustomerID = 2,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
            AllReviews.Add(new Review
            {
                ProdID = 4,
                CustomerID = 2,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
            AllReviews.Add(new Review
            {
                ProdID = 5,
                CustomerID = 2,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
        }
        public static List<Review> getReviews(int ProdId)
        {
            Reviews = new List<Review>();
            foreach (var review in AllReviews)
            {
                if (review.ProdID == ProdId)
                {
                    Reviews.Add(new Review
                    {
                        ProdID = review.ProdID,
                        CustomerID = review.CustomerID,
                        Rating = review.Rating,
                        Title = review.Title,
                        Desciption = review.Desciption
                    });
                }
            }
            if (Reviews.Count != 0)
            {
                foreach (var rev in Reviews)
                {
                    rev.StarColor1 = "Grey";
                    rev.StarColor2 = "Grey";
                    rev.StarColor3 = "Grey";
                    rev.StarColor4 = "Grey";
                    rev.StarColor5 = "Grey";
                    if (rev.Rating >= 1)
                    {
                        rev.StarColor1 = "Gold";
                    }
                    if (rev.Rating >= 2)
                    {
                        rev.StarColor2 = "Gold";
                    }
                    if (rev.Rating >= 3)
                    {
                        rev.StarColor3 = "Gold";
                    }
                    if (rev.Rating >= 4)
                    {
                        rev.StarColor4 = "Gold";
                    }
                    if (rev.Rating >= 5)
                    {
                        rev.StarColor5 = "Gold";
                    }
                }
            }
            return (List<Review>)Reviews;
        }
    }
}
