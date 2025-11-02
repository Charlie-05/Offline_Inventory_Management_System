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
                string query = "INSERT INTO Orders(OrderedOn , OrderAmount) " +
                    " values(@OrderedOn , @OrderAmount)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderedOn", order.OrderedOn);
                    cmd.Parameters.AddWithValue("@OrderAmount", order.OrderAmount);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return order;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }
    }
}
