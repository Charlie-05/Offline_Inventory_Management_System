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
        public decimal Quantity { get; set; }
        public decimal StockAlertOn { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public string? ProductCategoryName { get; set; }
    }


}
