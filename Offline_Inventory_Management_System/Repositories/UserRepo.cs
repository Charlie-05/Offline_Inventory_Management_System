using Microsoft.Data.SqlClient;
using Offline_Inventory_Management_System.DataBase;
using Offline_Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Repositories
{
    public class UserRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;

        public User AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users(Name, UserName, Password, UserRoleID) " +
                               "VALUES(@Name, @UserName, @Password, @UserRoleID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@UserRoleID", user.UserRoleID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return user;
                    }
                    else
                    {
                        throw new Exception("Failed to insert the user into the database.");
                    }
                }
            }
        }
        public User GetUserByUserName(string userName)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT UserId, Name, UserName, Password, UserRoleID FROM Users WHERE UserName = @UserName";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Name = reader["Name"].ToString(),
                                    UserName = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    UserRoleID = Convert.ToInt32(reader["UserRoleID"])
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching user: " + ex.Message);
                }
            }

            return user;
        }
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT UserId, Name, UserName, Password, UserRoleID FROM Users";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Name = reader["Name"].ToString(),
                                    UserName = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    UserRoleID = Convert.ToInt32(reader["UserRoleID"])
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading users: " + ex.Message);
                }
            }

            return users;
        }



    }
}
