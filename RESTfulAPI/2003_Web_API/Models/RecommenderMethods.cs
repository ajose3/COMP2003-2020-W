using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _2003_Web_API.Models
{
    public class RecommenderMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public RecommenderMethods(COMP2003_WContext context)
        {
            dataAccess = context;
        }

        public async Task<List<Product>> GetMostRecommended(string token)
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


            List<Product> productList = new List<Product>();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    Product product = new Product();
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


        public async Task<List<Product>> GetRelatedProducts(string token, int productId)
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

            List<Product> productList = new List<Product>();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    Product product = new Product();
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

    }
}
