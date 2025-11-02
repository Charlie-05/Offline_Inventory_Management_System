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
    public class BillProductRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;
        public BillProduct AddBillProduct(BillProduct billProduct)
        {
            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO BillProducts (BillId, ProductId, BillQuantity) " +
                               "VALUES (@BillId, @ProductId, @BillQuantity);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BillId", billProduct.BillId);
                    cmd.Parameters.AddWithValue("@ProductId", billProduct.ProductID);
                    cmd.Parameters.AddWithValue("@BillQuantity", billProduct.BillQuantity);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return billProduct;
                    }
                    else
                    {
                        throw new Exception("Failed to insert BillProduct record.");
                    }
                }
            }
        }
    }
}
