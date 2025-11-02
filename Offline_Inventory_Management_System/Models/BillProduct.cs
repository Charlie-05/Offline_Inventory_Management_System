using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Models
{
    public class BillProduct
    {
        public int BillProductID { get; set; }

        public int BillId { get; set; }
        public int ProductID { get; set; }

        public decimal BillQuantity { get; set; }
        public Bill? Bill { get; set; }

        public Product? Product { get; set; }
    }
}
