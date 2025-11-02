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
    public class OrderRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;
        public Order AddOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = @"
            INSERT INTO Orders (OrderedOn, OrderAmount) 
            VALUES (@OrderedOn, @OrderAmount);
            SELECT CAST(SCOPE_IDENTITY() AS INT);"; // Get the generated OrderId

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderedOn", order.OrderedOn);
                    cmd.Parameters.AddWithValue("@OrderAmount", order.OrderAmount);

                    int orderId = (int)cmd.ExecuteScalar();
                    order.OrderId = orderId; // Set the generated ID
                    return order;
                }
            }
        }

        public List<Order> ReadAllOrders()
        {
            var orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT * FROM Orders";


                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                var order = new Order
                                {
                                    OrderedOn = Convert.ToDateTime(reader["OrderedOn"]),
                                    OrderAmount = Convert.ToDecimal(reader["OrderAmount"]),
                                    OrderId = Convert.ToInt32(reader["OrderId"])

                                };
                                orders.Add(order);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return orders;
        }

    }
}
