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
    public class OrderProductRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;
        public OrderProduct AddOrder(OrderProduct orderProduct)
        {

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO OrderProducts(OrderId , ProductId , OrderQuantity) " +
                    " values(@OrderId , @ProductId , @OrderQuantity)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderProduct.OrderId);
                    cmd.Parameters.AddWithValue("@ProductId", orderProduct.ProductId);
                    cmd.Parameters.AddWithValue("@OrderQuantity", orderProduct.OrderQuantity);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return orderProduct;
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
