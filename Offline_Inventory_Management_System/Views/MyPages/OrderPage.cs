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
using Timer = System.Windows.Forms.Timer;

namespace Offline_Inventory_Management_System.Views
{
    public partial class OrderPage : UserControl
    {

        public OrderPage()
        {
            InitializeComponent();
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            AddOrderView addOrderView = new AddOrderView();
            addOrderView.Dock = DockStyle.Fill;

            // Option 1: replace everything inside OrderPage
            this.Controls.Clear();
            this.Controls.Add(addOrderView);
        }
    }
}
