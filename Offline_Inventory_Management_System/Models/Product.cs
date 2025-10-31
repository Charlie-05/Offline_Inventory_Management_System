using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockAlertOn { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
