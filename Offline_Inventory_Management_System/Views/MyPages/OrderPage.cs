using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Offline_Inventory_Management_System.Views
{
    public partial class OrderPage : UserControl
    {
        public OrderPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            Label lbl = new Label
            {
                Text = "Order Page",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(lbl);
        }
    }
}
