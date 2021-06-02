﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AdminInterface.Models;

namespace Web_API.Models
{
    public class API_RecommenderMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public API_RecommenderMethods(COMP2003_WContext context)
        {
            dataAccess = context;
        }

        public async Task<List<API_Product>> GetMostRecommended(string token)
        {

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "RecommendMostCategory";
            cmd.CommandType = CommandType.StoredProcedure;


            // setup output parameter
            SqlParameter outputParam = new SqlParameter("@ResponseMessage", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };


            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Token", token)
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);


            List<API_Product> productList = new List<API_Product>();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    API_Product product = new API_Product();
                    product.ProductId = Convert.ToInt32(reader[0]);
                    product.ProductName = reader[1].ToString();
                    product.ImageUrl = reader[2].ToString();
                    product.Price = Convert.ToDecimal(reader[5]);
                    product.Description = reader[6].ToString();
                    product.Category = reader[7].ToString();
                    productList.Add(product);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get RecommendMostCategory: error creating product from response data");
                }

            }

            return productList;

        }


        public async Task<List<API_Product>> GetRelatedProducts(string token, int productId)
        {
            //get all customer orders
            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Recommending";
            cmd.CommandType = CommandType.StoredProcedure;

            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Token", token),
                new SqlParameter("@ProductID", productId)
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            List<API_Product> productList = new List<API_Product>();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    API_Product product = new API_Product();
                    product.ProductId = Convert.ToInt32(reader[0]);
                    product.ProductName = reader[1].ToString();
                    product.ImageUrl = reader[2].ToString();
                    product.Price = Convert.ToDecimal(reader[5]);
                    product.Description = reader[6].ToString();
                    product.Category = reader[7].ToString();
                    productList.Add(product);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get products: error creating product from response data");
                }

            }

            return productList;
        }

        //public as



        public async Task<List<API_Product>> RecommendedForHome(string token)
        {
            //get orders
            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "GetOrders";
            cmd.CommandType = CommandType.StoredProcedure;

            // setup output parameter
            SqlParameter outputParam = new SqlParameter("@ResponseMessage", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Token", token),
                outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            List<API_Order> orderList = new List<API_Order>();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    API_Order order = new API_Order();
                    order.OrderId = Convert.ToInt32(reader[0]);
                    order.TimeOrdered = Convert.ToDateTime(reader[1]);
                    order.Quantity = Convert.ToInt32(reader[2]);
                    order.ProductId = Convert.ToInt32(reader[3]);
                    order.DeliveryDate = Convert.ToDateTime(reader[5]);
                    order.ProductName = reader[7].ToString();
                    order.ImageUrl = reader[8].ToString();
                    order.Price = Convert.ToDecimal(reader[11]);
                    order.Category = reader[13].ToString();

                    orderList.Add(order);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get products: error creating product from response data");
                }

            }
            reader.Close();

            //get reviews

            dataAccess.Database.OpenConnection();
            DbCommand cmd2 = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd2.CommandText = "CustomersReviews";
            cmd2.CommandType = CommandType.StoredProcedure;

            // setup procedure parameters
            SqlParameter[] @params2 =
            {
                new SqlParameter("@Token", token)
            };

            // add each parameter to command
            foreach (var param2 in @params2) cmd2.Parameters.Add(param2);

            List<API_Review> reviewList = new List<API_Review>();

            // retrieve the data
            DbDataReader reader2 = await cmd2.ExecuteReaderAsync();
            while (reader2.Read())
            {
                try
                {
                    API_Review review = new API_Review();
                    review.ProductID = Convert.ToInt32(reader2[0]);
                    review.CustomerId = Convert.ToInt32(reader2[1]);
                    review.Rating = Convert.ToInt32(reader2[2]);
                    review.Title = reader2[3].ToString();
                    review.Description = reader2[4].ToString();
                    reviewList.Add(review);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get reviews: error creating review from response data");
                }

            }

            reader2.Close();

            bool reviewed;
            bool found;
            List <API_OrderReviewProduct> ORPList = new List<API_OrderReviewProduct>();
            foreach (var o in orderList)
            {
                found = false;
                API_OrderReviewProduct ORP = new API_OrderReviewProduct();
                reviewed = false;
                //ORP.productId = o.ProductId;
                ORP.category = o.Category;
                foreach (var r in reviewList)
                {
                    if (o.ProductId == r.ProductID)//find related review
                    {
                        ORP.weight = o.Quantity * r.Rating;
                        reviewed = true;
                        break;
                    }
                }
                if (reviewed == false)
                {
                    ORP.weight = o.Quantity * 3; // no review assume neutral
                }

                foreach (var orp in ORPList)
                {
                    if (orp.category == ORP.category)
                    {
                        found = true;
                        orp.weight += ORP.weight;
                    }
                }
                if (found == false)
                {
                    //add to list of weights
                    ORPList.Add(ORP);
                }
            }



            //find category with largest weight in list

            string favouriteCat = "";
            string favouriteCat2 = "";

            if (ORPList.Count() > 1)
            {
                List<API_OrderReviewProduct> SortedList = ORPList.OrderByDescending(o => o.weight).ToList();
                favouriteCat = SortedList[0].category;
                favouriteCat2 = SortedList[1].category;
            }

            //List<OrderReviewProduct> SortedList = ORPList.OrderByDescending(o => o.weight).ToList();
            //string favouriteCat = SortedList[0].category;
            //string favouriteCat2 = SortedList[1].category;

            //Get products from the category

            dataAccess.Database.OpenConnection();
            DbCommand cmd3 = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd3.CommandText = "GetTopFromCat";
            cmd3.CommandType = CommandType.StoredProcedure;


            // setup procedure parameters
            SqlParameter[] @params3 =
            {
                new SqlParameter("@Token", token),
                new SqlParameter("@TopCat", favouriteCat),
                new SqlParameter("@TopCat2", favouriteCat2)
            };

            // add each parameter to command
            foreach (var param in @params3) cmd3.Parameters.Add(param);

            List<API_Product> productList = new List<API_Product>();

            // retrieve the data
            DbDataReader reader3 = await cmd3.ExecuteReaderAsync();
            while (reader3.Read())
            {
                try
                {
                    API_Product product = new API_Product();
                    product.ProductId = Convert.ToInt32(reader3[0]);
                    product.ProductName = reader3[1].ToString();
                    product.ImageUrl = reader3[2].ToString();
                    product.Price = Convert.ToDecimal(reader3[5]);
                    product.Description = reader3[6].ToString();
                    product.Category = reader3[7].ToString();
                    productList.Add(product);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get recomendationsHome: error creating product from response data");
                }

            }
            reader3.Close();
            return productList;

        }

    }
}