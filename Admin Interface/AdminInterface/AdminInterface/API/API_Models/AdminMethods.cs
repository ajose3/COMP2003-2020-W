using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AdminInterface.Models;

#nullable disable

namespace Web_API.Models
{
    public partial class API_AdminMethods
    {
        private readonly COMP2003_WContext dataAccess;

        public API_AdminMethods(COMP2003_WContext context)
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
            cmd.CommandText = "ValidateAdmin";
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

        public async Task<int> Register(string token, API_Customer customer)
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
            cmd.CommandText = "RegisterAdmin";
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

        public async Task<int> EditAdmin(string adminToken, API_Customer customer)
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
            cmd.CommandText = "EditAdmin";
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
                System.Diagnostics.Debug.WriteLine("EditCustomer: error getting output value from sql call");
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
            cmd.CommandText = "ChangeAdminPassword";
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

        public async Task<int> EditCustomer(string adminToken, API_Customer customer)
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
            cmd.CommandText = "AdminEditCustomer";
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
                new SqlParameter("@CustomerID", customer.CustomerId),
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
                System.Diagnostics.Debug.WriteLine("EditCustomer: error getting output value from sql call");
            }

            return response;
        }
        public async Task<int> DeleteCustomer(string adminToken, int customerID)
        {
            int response = 400;

            /*
            200 - success
            400 - Admin not logged in
            401 - Customer does not exist /is admin
            500 - unknown error
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "AdminDeleteCustomer";
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
                new SqlParameter("@CustomerDeletingID", customerID),
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
                System.Diagnostics.Debug.WriteLine("DeleteCustomer: error getting output value from sql call");
            }

            return response;
        }

        public async Task<int> CancelOrderAdmin(string adminToken, int orderId, int customerId)
        {
            int response = 400;

            /*
            200 - success
            400 - Admin not logged in
            401 - Order does not exist
            500 - unknown error
            */

            dataAccess.Database.OpenConnection();
            DbCommand cmd = dataAccess.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "CancelOrderAdmin";
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
                new SqlParameter("@OrderID", orderId),
                new SqlParameter("@CustomerID", customerId),
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
                System.Diagnostics.Debug.WriteLine("Cancel Order: error getting output value from sql call");
            }

            return response;
        }



    }
}
