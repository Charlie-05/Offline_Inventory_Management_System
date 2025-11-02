using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public string UserName  { get; set; }
        public string Password {  get; set; }

        public int UserRoleID { get; set; }

    }

    public enum Role
    {
        Admin = 1,
        Manager = 2,
        Cashier = 3
    }
}
