using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Models
{
    public class OrderProduct
    {
        public int OrderProductId { get; set; }

        public int ProductId { get; set; }

        public decimal OrderQuantity { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}
