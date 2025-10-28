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
    public class ProductCategoryRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;
        public void AddProductCategory(ProductCategory productCategory)
        {
          

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO ProductCategories(Name) values(@Name)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", productCategory.Name);
                 
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Product category successfully added");
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
