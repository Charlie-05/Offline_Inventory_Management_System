using Microsoft.Data.SqlClient;
using Offline_Inventory_Management_System.DataBase;
using Offline_Inventory_Management_System.Models;
using Offline_Inventory_Management_System.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Offline_Inventory_Management_System.Models.Product;

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

        public List<Product> ReadAllProducs(string? name = null)
        {
            var products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT P.* , PC.UnitOfMeasurement , PC.Name FROM Products P INNER JOIN ProductCategories PC ON PC.ProductCategoryID = P.ProductCategoryID";

                if (name != null)
                {
                    query = "SELECT ProductID , ProductName FROM Products Where ProductName Like " + $"'%{name}%'";
                }
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (name != null)
                                {
                                    var product = new Product()
                                    {
                                        ProductId = Convert.ToInt32(reader["ProductId"]),
                                        ProductName = reader["ProductName"].ToString(),
                                    };

                                    products.Add(product);
                                }
                                else
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
                    }

                }
                catch (Exception ex)
                {
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

        public Product UpdateProductQuantity(int productID, decimal quantity, bool isAdded)
        {
            Product getProduct = this.ReadProductById(productID);
            decimal newQuantity;
            if (getProduct != null)
            {
                if (isAdded)
                {
                    newQuantity = getProduct.Quantity + quantity;
                }
                else
                {
                    bool availabiliy = this.CheckAvailability(getProduct.ProductId, quantity);
                    if (availabiliy)
                    {
                        newQuantity = getProduct.Quantity - quantity;
                    }
                    else
                    {
                        throw new Exception("Quantity Unavailable");
                    }
                }
                using (SqlConnection conn = new SqlConnection(DBConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE Products SET Quantity = @Quantity where ProductId = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                        cmd.Parameters.AddWithValue("@ProductId", getProduct.ProductId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return getProduct;
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

        public bool CheckAvailability(int productId, decimal quantity)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(DBConnectionString))
                {
                    Product product = new Product();
                    conn.Open();
                    string query = "SELECT Quantity FROM Products WHERE ProductID = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                            };
                        }
                    }
                    if (product.Quantity > quantity)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Product> GetLowStockProducts()
        {
            var res = new List<Product>();
            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Products Where Quantity < StockAlertOn";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var product = new Product()
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                StockAlertOn = Convert.ToInt32(reader["StockAlertOn"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            };

                            res.Add(product);
                        }
                    }
                    return res;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public List<Product> GetSearchResults(string searchText)
        {
            var res = new List<Product>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();

              
                string query = @"
            SELECT P.*, PC.UnitOfMeasurement, PC.Name 
            FROM Products P 
            INNER JOIN ProductCategories PC ON PC.ProductCategoryID = P.ProductCategoryID
            WHERE PC.Name LIKE @searchText
               OR P.ProductName LIKE @searchText
               OR P.ProductId  LIKE @searchText";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                      
                        cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                        SqlDataReader reader = cmd.ExecuteReader();
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
                            res.Add(product);
                        }
                    }

                    return res;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    return null;
                }
            }
        }

    }
}
