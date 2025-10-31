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
        public ProductCategory AddProductCategory(ProductCategory productCategory)
        {

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO ProductCategories(Name , UnitOfMeasurement) values(@Name , @UnitOfMeasurement)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", productCategory.Name);
                    cmd.Parameters.AddWithValue("@UnitOfMeasurement", productCategory.UnitOfMeasurement);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return productCategory;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }

        public List<ProductCategory> ReadAllCategories()
        {
            var categories = new List<ProductCategory>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT ProductCategoryId, Name FROM ProductCategories";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new ProductCategory
                            {
                                ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]),
                                Name = reader["Name"].ToString()
                            };

                            categories.Add(category);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // You can log the exception here if needed
                    Console.WriteLine(ex.Message);
                }
            }

            return categories;
        }

    }
}
