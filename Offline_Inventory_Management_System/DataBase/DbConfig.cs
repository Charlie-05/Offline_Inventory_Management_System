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
        private static readonly string ServerConnection = "server=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=master;";
        public static readonly string ConnectionString =
        "server=(localdb)\\MSSQLLocalDB;Database=Inventory_System_Db;Integrated Security=True;";

        /// <summary>
        /// Tests connection and optionally creates required tables if they don’t exist.
        /// </summary>

        public static void InitializeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ServerConnection))
                {
                    conn.Open();

                    string createDbSql = @"
                    IF NOT EXISTS (
                        SELECT name FROM sys.databases WHERE name = N'Inventory_System_Db'
                    )
                    BEGIN
                        CREATE DATABASE Inventory_System_Db;
                    END;
                ";

                    using (SqlCommand cmd = new SqlCommand(createDbSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

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
                            Password NVARCHAR(100),
                            UserRoleID INT NOT NULL
                        );
                        
                        IF NOT EXISTS (SELECT * FROM Users WHERE UserRoleId = 1)
                        BEGIN
                            INSERT INTO Users (Name, UserName, Password, UserRoleID)
                            VALUES ('Admin', 'admin', '12345', 1);
                            
                        END

                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductCategories')
                        CREATE TABLE ProductCategories (
                            ProductCategoryId INT IDENTITY(1,1) PRIMARY KEY,
                            Name NVARCHAR(100) NOT NULL UNIQUE,
                            UnitOfMeasurement NVARCHAR(150) NOT NULL
                           
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
                          );

                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bills')
                        CREATE TABLE Bills (
                            BillId INT IDENTITY(1,1) PRIMARY KEY,
                            BilledOn DateTime NOT NULL ,
                            BilledAmount INT NOT NULL
                           
                        ); 
                        
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
                        CREATE TABLE Orders (
                            OrderID INT IDENTITY(1,1) PRIMARY KEY,
                            OrderedOn DateTime NOT NULL ,
                            OrderAmount INT NOT NULL
                           
                        );

                        IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'BillProducts')
                        CREATE TABLE BillProducts(
                          BillProductID INT IDENTITY(1, 1) PRIMARY KEY,
                          ProductId INT NOT NULL,
                          BillId INT NOT NULL,
                         BillQuantity DECIMAL(18, 2) NOT NULL,
                          FOREIGN KEY (ProductId) REFERENCES Products(ProductID),
                          FOREIGN KEY (BillId) REFERENCES Bills(BillId)
                          );   
                        
                        IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'OrderProducts')
                        CREATE TABLE OrderProducts(
                          OrderProductID INT IDENTITY(1, 1) PRIMARY KEY,
                          ProductId INT NOT NULL,
                          OrderId INT NOT NULL,
                             OrderQuantity DECIMAL(18, 2) NOT NULL,
                          FOREIGN KEY (ProductId) REFERENCES Products(ProductID),
                          FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
                          );   

";




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
