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
        private BindingList<Order> orderBindingList;
        public OrderPage()
        {
            InitializeComponent();
            this.Load += onView_Load;
        }


        private void onView_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddOrderView addOrderView = new AddOrderView();
            addOrderView.Dock = DockStyle.Fill;

            // replace everything inside OrderPage
            this.Controls.Clear();
            this.Controls.Add(addOrderView);
        }

        private void LoadOrders()
        {
            var orders = GetAllOrders();

            orderBindingList = new BindingList<Order>(orders);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Add normal columns
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Order date", DataPropertyName = "OrderedOn", Name = "OrderedOn", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Order Amount", DataPropertyName = "OrderAmount", Name = "OrderAmount", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OrderId", Name = "OrderId", Visible = false });

            dataGridView1.DataSource = orderBindingList;
        }

        private List<Order> GetAllOrders()
        {
            OrderRepo orderRepo = new OrderRepo();
            var res = orderRepo.ReadAllOrders();
            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.LoadOrders();
        }
    }
}
