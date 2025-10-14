using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;

namespace Offline_Inventory_Management_System.DataBase
{
    public static class DbConfig
    {
        public static readonly string ConnectionString =
        "server=(localdb)\\MSSQLLocalDB;Database=Inventory_System_Db;Integrated Security=True;";

        /// <summary>
        /// Tests connection and optionally creates required tables if they don’t exist.
        /// </summary>

        public static void InitializeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // Example: Create a table if it doesn’t exist
                    string createTableSql = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                        CREATE TABLE Users (
                            UserID INT IDENTITY(1,1) PRIMARY KEY,
                            Name NVARCHAR(100) NOT NULL,
                            UserName NVARCHAR(100) NOT NULL UNIQUE,
                            Password NVARCHAR(100)
                        );

                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductCategories')
                        CREATE TABLE ProductCategories (
                            ProductCategoryId INT IDENTITY(1,1) PRIMARY KEY,
                            Name NVARCHAR(100) NOT NULL UNIQUE,
                           
                        );  

                        IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Products')
                        CREATE TABLE Products(
                          ProductID INT IDENTITY(1, 1) PRIMARY KEY,
                          ProductName NVARCHAR(100) NOT NULL,
                          Price INT NOT NULL,
                          Quantity INT NOT NULL,
                          ProductCategoryId INT,
                          StockAlertOn INT NOT NULL,
                          FOREIGN KEY (ProductCategoryId) REFERENCES ProductCategories(ProductCategoryId)
                          );";




                    using (SqlCommand cmd = new SqlCommand(createTableSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                  
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Database initialization failed:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Simple helper to get a new SQL connection.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
