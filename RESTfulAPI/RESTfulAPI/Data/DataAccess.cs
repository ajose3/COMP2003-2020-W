using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RESTfulAPI.Repositories
{
    public class DataAccess : DbContext
    {

        public static string dbConnection()
        {
            //CHANGE CONNECTION STRING
            string connStr = ConfigurationManager.ConnectionStrings["UsersDBConnection"].ConnectionString;
            return connStr;
        }
        public static string HashPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            hash = hash.Replace("'","");
            return hash;
        }
        public static string ValidateCustomer(User user)
        {
            SqlDataReader responseReader;
            string token = null;

            string hashedPassword = HashPassword(user.Password);

            var query = "DECLARE @Out as VARCHAR(25); EXEC ValidateCustomer @Email = '_email', @Password = '_password', @Token = @Out OUTPUT; SELECT @OUT AS 'OutputMessage'; ";

            query = query.Replace("_email", user.Email).Replace("_password", hashedPassword);

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                token = responseReader.GetString(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return token;
            }
            catch (Exception)
            {
                return token;
            }
        }

        public static bool RegisterCustomer(User user)
        {
            SqlDataReader responseReader;
            bool responseMessage = false;

            string hashedPassword = HashPassword(user.Password);

            var query = "DECLARE @Out as BIT; EXEC RegisterCustomer @First_Name = '_fName', @Last_Name = '_lName', @Email = '_email', @Password = '_password', @Age = '_age', @Gender = '_gender', @Address = '_address', @Phone_Number = '_phoneNumber', @success = @Out OUTPUT; SELECT @OUT AS 'Outputmessage'; ";

            query = query.Replace("_fName", user.FirstName).Replace("_lName", user.LastName).Replace("_email", user.Email).Replace("_password", hashedPassword).Replace("_age", user.Age.ToString()).Replace("_gender",user.Gender.ToString()).Replace("_address", user.Address).Replace("_phoneNumber", user.PhoneNumber);

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                responseMessage = responseReader.GetBoolean(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return responseMessage;
            }
            catch (Exception)
            {
                return responseMessage;
            }
        }

        public static bool UpdateCustomer(string token, User user)
        {
            SqlDataReader responseReader;
            bool responseMessage = false;

            string hashedPassword = HashPassword(user.Password);

            var query = "DECLARE @Out as BIT; EXEC UpdateCustomer @Token = '_token', @FirstName = '_fName', @LastName = '_lName', @Email = '_email', @Password = '_password', @Age = '_age', @Gender = _gender, @Address = '_address', @PhoneNumber = '_phoneNumber', @Success = @Out OUTPUT; SELECT @Out AS 'OutputMessage'; ";

            query = query.Replace("_token",token).Replace("_fName", user.FirstName).Replace("_lName", user.LastName).Replace("_email", user.Email).Replace("_password", hashedPassword).Replace("_age", user.Age.ToString()).Replace("_gender",user.Gender.ToString()).Replace("_address", user.Address).Replace("_phoneNumber", user.PhoneNumber);

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                responseMessage = responseReader.GetBoolean(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return responseMessage;
            }
            catch (Exception)
            {
                return responseMessage;
            }
        }

        public static bool DeleteCustomer(string token)
        {
            SqlDataReader responseReader;
            bool responseMessage = false;

            var query = "DECLARE @Out as BIT; EXEC DeleteCustomer @Token = '_token', @Success = @Out OUTPUT; SELECT @Out AS 'OutputMessage'; ";

            query = query.Replace("_token", token);

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                responseMessage = responseReader.GetBoolean(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return responseMessage;
            }
            catch (Exception)
            {
                return responseMessage;
            }
        }
        //admin

        //temp
        public static bool RegisterAdmin(string token, User user)
        {
            SqlDataReader responseReader;
            bool responseMessage = false;

            string hashedPassword = HashPassword(user.Password);

            var query = "DECLARE @Out as BIT; EXEC RegisterAdmin @Token = '_token', @First_Name = '_fName', @Last_Name = '_lName', @Email = '_email', @Password = '_password', @Age = '_age', @Gender = '_gender', @Address = '_address', @Phone_Number = '_phoneNumber', @success = @Out OUTPUT; SELECT @OUT AS 'Outputmessage'; ";

            query = query.Replace("_token", token).Replace("_fName", user.FirstName).Replace("_lName", user.LastName).Replace("_email", user.Email).Replace("_password", hashedPassword).Replace("_age", user.Age.ToString()).Replace("_gender", user.Gender.ToString()).Replace("_address", user.Address).Replace("_phoneNumber", user.PhoneNumber);

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                responseMessage = responseReader.GetBoolean(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return responseMessage;
            }
            catch (Exception)
            {
                return responseMessage;
            }
        }
        //temp ^^


        public static string ValidateAdmin(User user)
        {
            SqlDataReader responseReader;
            string token = null;

            string hashedPassword = HashPassword(user.Password);

            var query = "DECLARE @Out as VARCHAR(25); EXEC ValidateAdmin @Email = '_email', @Password = '_password', @Token = @Out OUTPUT; SELECT @Out AS 'OutputMessage'; ";

            query = query.Replace("_email", user.Email).Replace("_password", hashedPassword);

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                token = responseReader.GetString(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return token;
            }
            catch (Exception)
            {
                return token;
            }
        }

        public static bool AdminRemoveCustomer(string token, int Customer_Deleting_ID)
        {
            SqlDataReader responseReader;
            bool responseMessage = false;

            var query = "DECLARE @Out as BIT; EXEC AdminRemoveCustomer @Token = '_token', @Customer_Deleting_ID = _delID, @Success = @Out OUTPUT; SELECT @Out AS 'OutputMessage'; ";

            query = query.Replace("_token", token).Replace("_delID", Customer_Deleting_ID.ToString());

            SqlConnection connection = new SqlConnection(dbConnection());

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                responseReader = command.ExecuteReader();

                responseReader.Read();
                responseMessage = responseReader.GetBoolean(responseReader.GetOrdinal("OutputMessage"));
                responseReader.Close();

                command.Dispose();
                connection.Close();
                return responseMessage;
            }
            catch (Exception)
            {
                return responseMessage;
            }
        }

    }
}