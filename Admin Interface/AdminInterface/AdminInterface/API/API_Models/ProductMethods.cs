using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AdminInterface.Models;

#nullable disable

namespace Web_API.Models
{
    public class API_ProductMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public API_ProductMethods(COMP2003_WContext context)
        {
            dataAccess = context;
        }

        public async Task<List<API_Product>> Get()
        {
            string queryString = "SELECT * FROM ProductsForCustomer";

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = queryString;
            cmd.CommandType = CommandType.Text;

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
                    product.Price = Convert.ToDecimal(reader[3]);
                    product.Description = reader[4].ToString();
                    product.Category = reader[5].ToString();
                    productList.Add(product);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get products: error creating product from response data");
                }

            }

            return productList;
        }

        public async Task<int> Add(string token, API_Product product)
        {
            int response = 400;

            /*
            200 - success
            208 - call success but email exists
            500 - unknown error
            400 - call failed
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "AddProduct";
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
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ImageUrl", product.ImageUrl),
                new SqlParameter("@Stock", product.Stock),
                new SqlParameter("@Category", product.Category),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Description", product.Description),
                outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            // execute command
            await cmd.ExecuteNonQueryAsync();

            try
            {
                // get output value from command
                response = Convert.ToInt32(cmd.Parameters["@ResponseMessage"].Value);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Add: error getting output value from sql call");
            }

            return response;
        }


        public async Task<int> Edit(string token, API_Product product)
        {
            int response = 400;

            /*
            200 - success
            208 - call success but email exists
            500 - unknown error
            400 - call failed
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "EditProduct";
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
                new SqlParameter("@ProductID", product.ProductId),
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ImageUrl", product.ImageUrl),
                new SqlParameter("@Stock", product.Stock),
                new SqlParameter("@Category", product.Category),
                new SqlParameter("@TotalSold", product.TotalSold),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Description", product.Description),
                outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            // execute command
            await cmd.ExecuteNonQueryAsync();

            try
            {
                // get output value from command
                response = Convert.ToInt32(cmd.Parameters["@ResponseMessage"].Value);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Register: error getting output value from sql call");
            }

            return response;
        }

        public async Task<int> Delete(string token, string Id)
        {
            int response = 400;

            /*
            200 - success
            208 - call success but email exists
            500 - unknown error
            400 - call failed
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "DeleteProduct";
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
                new SqlParameter("@ProductID", Id),
                outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            // execute command
            await cmd.ExecuteNonQueryAsync();

            try
            {
                // get output value from command
                response = Convert.ToInt32(cmd.Parameters["@ResponseMessage"].Value);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Register: error getting output value from sql call");
            }

            return response;
        }

        public async Task<List<API_Product>> GetTrending()
        {
            string queryString = "SELECT * FROM TrendingProduct";

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = queryString;
            cmd.CommandType = CommandType.Text;

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
                    product.Price = Convert.ToDecimal(reader[3]);
                    product.Description = reader[4].ToString();
                    product.Category = reader[5].ToString();
                    productList.Add(product);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get products: error creating product from response data");
                }

            }

            return productList;
        }


        public async Task<List<API_Product>> GetFeatured()
        {
            string queryString = "SELECT * FROM FeaturedProduct";

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = queryString;
            cmd.CommandType = CommandType.Text;

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
                    product.Price = Convert.ToDecimal(reader[3]);
                    product.Description = reader[4].ToString();
                    product.Category = reader[5].ToString();
                    productList.Add(product);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get products: error creating product from response data");
                }

            }

            return productList;
        }

        public async Task<int> AddStock(string adminToken, int productId, int stock)
        {
            int response = 400;

            /*
            200 - success
            208 - product does not exist
            500 - unknown error
            400 - call failed
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "AddStock";
            cmd.CommandType = CommandType.StoredProcedure;

            // setup output parameter
            SqlParameter outputParam = new SqlParameter("@ResponseMessage", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };


            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Token", adminToken),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@Stock", stock),
                outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            // execute command
            await cmd.ExecuteNonQueryAsync();

            try
            {
                // get output value from command
                response = Convert.ToInt32(cmd.Parameters["@ResponseMessage"].Value);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("EditCustomer: error getting output value from sql call");
            }

            return response;
        }

    }
}
