using Offline_Inventory_Management_System.Models;
using Offline_Inventory_Management_System.Repositories;
using Offline_Inventory_Management_System.Views.MyViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Offline_Inventory_Management_System.Views.MyPages
{
    public partial class UserPage : UserControl
    {
        public UserPage()
        {
            InitializeComponent();
            this.Load += OnViewLoad;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdduserView adduserView = new AdduserView();
            adduserView.Show();
        }

        private void OnViewLoad(object sender, EventArgs e)
        {
            var userRepo = new UserRepo();
            dataGridView1.DataSource = userRepo.GetAllUsers().Select(u => new
            {
                u.UserId,
                u.Name,
                u.UserName,
                Role = Enum.GetName(typeof(Role), u.UserRoleID)
            }).ToList();
        }
    }
}
