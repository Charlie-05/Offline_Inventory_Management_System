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
    public class ProductRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;
        public Product AddProduct(Product product)
        {

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Products(ProductName , ProductCategoryId , Price , Quantity , StockAlertOn) " +
                    " values(@ProductName , @ProductCategoryId , @Price , @Quantity , @StockAlertOn)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductCategoryId", product.ProductCategoryId);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@StockAlertOn", product.StockAlertOn);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return product;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }

        public List<Product> ReadAllProducs()
        {
            var products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT P.* , PC.UnitOfMeasurement , PC.Name FROM Products P INNER JOIN ProductCategories PC ON PC.ProductCategoryID = P.ProductCategoryID";

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                StockAlertOn = Convert.ToInt32(reader["StockAlertOn"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]),
                                ProductCategory = new ProductCategory
                                {
                                    Name = reader["Name"].ToString(),
                                    UnitOfMeasurement = reader["UnitOfMeasurement"].ToString()
                                }
                            };

                            products.Add(product);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // You can log the exception here if needed
                    Console.WriteLine(ex.Message);
                }
            }

            return products;
        }

        public Product ReadProductById(int productId)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(DBConnectionString))
                {
                    Product product = new Product();
                    conn.Open();
                    string query = "SELECT P.* , PC.UnitOfMeasurement , PC.Name FROM Products P INNER JOIN ProductCategories PC ON PC.ProductCategoryID = P.ProductCategoryID WHERE P.ProductID = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                StockAlertOn = Convert.ToInt32(reader["StockAlertOn"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]),
                                ProductCategory = new ProductCategory
                                {
                                    Name = reader["Name"].ToString(),
                                    UnitOfMeasurement = reader["UnitOfMeasurement"].ToString()
                                }
                            };
                        }
                    }
                    return (product);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Product UpdateProduct(Product product)
        {
            Product getProduct = this.ReadProductById(product.ProductId);

            if (getProduct != null)
            {

                using (SqlConnection conn = new SqlConnection(DBConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE Products SET ProductName=@ProductName , Quantity = @Quantity , StockAlertOn = @StockAlertOn , Price = @Price   where ProductId = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                        cmd.Parameters.AddWithValue("@StockAlertOn", product.StockAlertOn);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return product;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public Product UpdateProductQuantity(int productID , int quantity)
        {
            Product getProduct = this.ReadProductById(productID);

            if (getProduct != null)
            {

                //using (SqlConnection conn = new SqlConnection(DBConnectionString))
                //{
                //    conn.Open();
                //    string query = "UPDATE Products SET ProductName=@ProductName , Quantity = @Quantity , StockAlertOn = @StockAlertOn , Price = @Price   where ProductId = @ProductId";
                //    using (SqlCommand cmd = new SqlCommand(query, conn))
                //    {
                //        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                //        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                //        cmd.Parameters.AddWithValue("@StockAlertOn", product.StockAlertOn);
                //        cmd.Parameters.AddWithValue("@Price", product.Price);
                //        cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                //        int rowsAffected = cmd.ExecuteNonQuery();
                //        if (rowsAffected > 0)
                //        {
                //            return product;
                //        }
                //        else
                //        {
                //            throw new Exception();
                //        }
                //    }
                //}
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
