using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

#nullable disable

namespace _2003_Web_API.Models
{
    public class CustomerMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public CustomerMethods(COMP2003_WContext context)
        {
            dataAccess = context;
        }

        public async Task<string> Validate(string email, string password)
        {
            string token = "400";

            /*
            208 - call successful but user not found
            500 - connection success but unknown error
            400 - method call failed
            */

            // hash password
            string hashedPassword = COMP2003_WContext.HashPassword(password);

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "ValidateCustomer";
            cmd.CommandType = CommandType.StoredProcedure;

            // setup output parameter
            SqlParameter outputParam = new SqlParameter("@Token", SqlDbType.VarChar)
            {
                Direction = ParameterDirection.Output,
                Size = 25
            };


            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", hashedPassword),
                outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            // execute command
            await cmd.ExecuteNonQueryAsync();



            try
            {
                // get output value from command
                token = cmd.Parameters["@Token"].Value.ToString();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Validate: error getting output value from sql call");
            }

            return token;
        }

        public async Task<int> Register(Customer customer)
        {
            int response = 400;

            /*
            200 - success
            208 - call success but email exists
            500 - unknown error
            400 - call failed
            */

            // hash password
            string hashedPassword = COMP2003_WContext.HashPassword(customer.Password);

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "RegisterCustomer";
            cmd.CommandType = CommandType.StoredProcedure;

            // setup output parameter
            SqlParameter outputParam = new SqlParameter("@ResponseMessage", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };


            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@Email", customer.EmailAddress),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@Age", customer.Age),
                new SqlParameter("@Gender", customer.Gender),
                new SqlParameter("@Address", customer.Address),
                new SqlParameter("@PhoneNumber", customer.PhoneNumber),
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

        public async Task<int> Edit(string token, Customer customer)
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
            cmd.CommandText = "EditCustomer";
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
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@Email", customer.EmailAddress),
                new SqlParameter("@Age", customer.Age),
                new SqlParameter("@Gender", customer.Gender),
                new SqlParameter("@Address", customer.Address),
                new SqlParameter("@PhoneNumber", customer.PhoneNumber),
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
                System.Diagnostics.Debug.WriteLine("Edit: error getting output value from sql call");
            }

            return response;
        }

        public async Task<int> ChangePassword(string token, string newPassword)
        {
            int response = 400;

            /*
            200 - success
            401 - call success but user does not exist
            500 - unknown error
            400 - call failed
            */

            // hash password
            string hashedPassword = COMP2003_WContext.HashPassword(newPassword);

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "ChangePassword";
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
                new SqlParameter("@NewPassword", hashedPassword),
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
                System.Diagnostics.Debug.WriteLine("ChangePassword: error getting output value from sql call");
            }

            return response;
        }

        public async Task<int> Delete(string token)
        {
            int response = 400;

            /*
            200 - success
            401 - call successfull but user does not exist or is an admin
            402 - call successfull but user is not logged in
            400 - call failed
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "DeleteCustomer";
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

        public async Task<Customer> GetDetails(string token)
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
            cmd.CommandText = "GetCustomerDetails";
            cmd.CommandType = CommandType.StoredProcedure;

            // setup output parameter
            //SqlParameter outputParam = new SqlParameter("@ResponseMessage", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};


            // setup procedure parameters
            SqlParameter[] @params =
            {
                new SqlParameter("@Token", token)
                //outputParam
            };

            // add each parameter to command
            foreach (var param in @params) cmd.Parameters.Add(param);

            // execute command
            await cmd.ExecuteNonQueryAsync();

            Customer customer = new Customer();

            // retrieve the data
            DbDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    customer.CustomerId = Convert.ToInt32(reader[0]);
                    customer.FirstName =  reader[1].ToString();
                    customer.LastName = reader[2].ToString();
                    customer.EmailAddress = reader[3].ToString();
                    customer.Age = Convert.ToInt32(reader[5]);
                    customer.Gender = Convert.ToBoolean(reader[6]);
                    customer.Address = reader[7].ToString();
                    customer.PhoneNumber = reader[8].ToString();
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Get reviews: error creating review from response data");
                }

            }

            return customer;



            //try
            //{
            //    // get output value from command
            //    response = Convert.ToInt32(cmd.Parameters["@ResponseMessage"].Value);
            //}
            //catch (Exception)
            //{
            //    System.Diagnostics.Debug.WriteLine("Get Details: error getting output value from sql call");
            //}

            //return response;
        }

    }
}
