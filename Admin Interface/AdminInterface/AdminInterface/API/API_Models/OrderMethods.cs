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

namespace Web_API.Models
{
    public class API_OrderMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public API_OrderMethods(COMP2003_WContext context)
        {
            dataAccess = context;
        }

        public async Task<List<API_Order>> Get(string token)
        {

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

            return orderList;
        }

        public async Task<int> Add(string token, int productId, int quantity, DateTime deliveryDate)
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
            cmd.CommandText = "AddOrder";
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
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@DeliveryDate", deliveryDate),
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

        public async Task<int> Delete(string token, int orderId)
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
            cmd.CommandText = "CancelOrder";
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
                new SqlParameter("@OrderID", orderId),
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
                System.Diagnostics.Debug.WriteLine("Cancel: error getting output value from sql call");
            }

            return response;
        }


        public async Task<List<API_Product>> GetLowStock(string token)
        {

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "GetLowStock";
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
                    System.Diagnostics.Debug.WriteLine("Get LowStock: error creating product from response data");
                }
            }

            return productList;
        }



        public async Task<List<API_Order>> GetOrdersbyId(string token,int productId)
        {

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "GetOrdersbyId";
            cmd.CommandType = CommandType.StoredProcedure;

            //// setup output parameter
            //SqlParameter outputParam = new SqlParameter("@ResponseMessage", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};

            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Token", token),
                new SqlParameter("@ProductID", productId),
                //outputParam
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

                    orderList.Add(order);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get OrdersbyId: error creating product from response data");
                }

            }

            return orderList;
        }
    }
}
