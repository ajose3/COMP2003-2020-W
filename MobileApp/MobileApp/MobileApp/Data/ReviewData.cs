using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Data
{
    public static class ReviewData
    {
        public static IList<Review> Reviews { get; private set; }

        //mock data store
        static ReviewData()
        {
            Reviews = new List<Review>();

            Reviews.Add(new Review
            {
                Id = 1,
                ProdID = 1,
                Rating = 4,
                Title = "This it the title",
                Desciption = "This is the reviews description"
            });

            Reviews.Add(new Review
            {
                Id = 2,
                ProdID = 1,
                Rating = 2,
                Title = "This it the title2",
                Desciption = "This is the reviews description"
            });
        }
    }
}
