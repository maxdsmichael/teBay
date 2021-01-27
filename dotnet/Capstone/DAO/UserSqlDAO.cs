using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly string connectionString;

        public UserSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }


        public User AddUser(RegisterUser user)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(user.Password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, name, address, phone_number, contact_times, password_hash, salt, user_role) VALUES (@username, @name, @address, @phone_number, @contact_times, @password_hash, @salt, @user_role)", conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@phone_number", user.Phone_Number);
                    cmd.Parameters.AddWithValue("@contact_times", user.Contact_Times);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(user.Username);
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                //TODO: these items are commented so login works
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
               // Name = Convert.ToString(reader["name"]),
               // Address = Convert.ToString(reader["address"]),
               // Phone_Number = Convert.ToString(reader["phone_number"]),
               // Contact_Times = Convert.ToString(reader["contact_times"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["user_role"]),
            };

            return u;
        }
    }
}
