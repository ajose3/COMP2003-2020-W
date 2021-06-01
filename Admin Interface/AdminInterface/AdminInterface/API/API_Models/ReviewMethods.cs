using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AdminInterface.Models;

namespace _2003_Web_API.Models
{
    public class ReviewMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public ReviewMethods(COMP2003_WContext context)
        {
            dataAccess = context;
        }

        public async Task<List<Review>> Get(int productId)
        {
            string queryString = $"SELECT * FROM ProductReviews WHERE ProductID = {productId}";

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = queryString;
            cmd.CommandType = CommandType.Text;

            List<Review> reviewList = new List<Review>();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    Review review = new Review();
                    review.ProductID = Convert.ToInt32(reader[0]);
                    review.CustomerId = Convert.ToInt32(reader[1]);
                    review.Rating = Convert.ToInt32(reader[2]);
                    review.Title = reader[3].ToString();
                    review.Description = reader[4].ToString();
                    reviewList.Add(review);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get reviews: error creating review from response data");
                }

            }

            return reviewList;
        }

        public async Task<int> Add(string token, Review review)
        {
            int response = 400;

            /*
            200 - success
            400 - method call failed
            401 - customer does not exist
            402 - customer not logged in
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "WriteReview";
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
                new SqlParameter("@Rating", review.Rating),
                new SqlParameter("@ProductID", review.ProductID),
                new SqlParameter("@Title", review.Title),
                new SqlParameter("@Description", review.Description),
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

        public async Task<int> Edit(string token, Review review)
        {
            int response = 400;

            /*
            200 - success
            400 - method call failed
            401 - customer does not exist
            402 - customer not logged in
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "EditReview";
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
                new SqlParameter("@Rating", review.Rating),
                new SqlParameter("@ProductID", review.ProductID),
                new SqlParameter("@Title", review.Title),
                new SqlParameter("@Description", review.Description),
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

        public async Task<int> Delete(string token, int ProductId)
        {
            int response = 400;

            /*
            200 - success
            400 - method call failed
            401 - customer does not exist
            402 - customer not logged in
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "DeleteReview";
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
                new SqlParameter("@ProductID", ProductId),

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
                System.Diagnostics.Debug.WriteLine("Delete: error getting output value from sql call");
            }

            return response;
        }


        public async Task<List<ReviewsWithName>> GetCustomersReviews(string token)
        {
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

            List<ReviewsWithName> reviewList = new List<ReviewsWithName>();

            // retrieve the data
            DbDataReader reader2 = await cmd2.ExecuteReaderAsync();
            while (reader2.Read())
            {
                try
                {
                    ReviewsWithName review = new ReviewsWithName();
                    review.ProductID = Convert.ToInt32(reader2[0]);
                    review.CustomerId = Convert.ToInt32(reader2[1]);
                    review.Rating = Convert.ToInt32(reader2[2]);
                    review.Title = reader2[3].ToString();
                    review.Description = reader2[4].ToString();
                    review.ProductName = reader2[5].ToString();
                    reviewList.Add(review);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get reviews: error creating review from response data");
                }

            }

            reader2.Close();

            return reviewList;
        }

    }
}
