using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderedOn { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
