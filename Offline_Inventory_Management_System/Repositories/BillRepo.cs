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
    public class BillRepo
    {
        static string DBConnectionString = DbConfig.ConnectionString;
        public Bill AddBill(Bill bill)
        {
            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                conn.Open();
                string query = @"
        INSERT INTO Bills (BilledOn, BilledAmount)
        VALUES (@BilledOn, @BilledAmount);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";  // Returns generated BillId

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BilledOn", bill.BilledOn);
                    cmd.Parameters.AddWithValue("@BilledAmount", bill.BilledAmount);

                    int billId = (int)cmd.ExecuteScalar();
                    bill.BillId = billId; // Assign generated ID back to object
                    return bill;
                }
            }
        }

        public List<Bill> ReadAllBills()
        {
            var bills = new List<Bill>();

            using (SqlConnection conn = new SqlConnection(DBConnectionString))
            {
                string query = "SELECT BillId, BilledOn, BilledAmount FROM Bills";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bills.Add(new Bill
                                {
                                    BillId = reader.GetInt32(reader.GetOrdinal("BillId")),
                                    BilledOn = reader.GetDateTime(reader.GetOrdinal("BilledOn")),
                                    BilledAmount = reader.GetInt32(reader.GetOrdinal("BilledAmount"))
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception as needed
                        throw new Exception("Error reading bills from the database", ex);
                    }
                }
            }

            return bills;
        }
    }
}
