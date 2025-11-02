using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public DateTime BilledOn { get; set; }
        public decimal BilledAmount { get; set; }

    }
}
