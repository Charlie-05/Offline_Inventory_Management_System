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
    public partial class BillPage : UserControl
    {
        public BillPage()
        {
            InitializeComponent();
            this.Load += onView_Load;
        }

        private void onView_Load(object sender, EventArgs e)
        {
            LoadBills();
        }

        private void LoadBills()
        {
            var billRepo = new BillRepo();
            var bills = billRepo.ReadAllBills();

            // Bind list to DataGridView
            dataGridView1.DataSource = bills;

            // Optional: Customize columns
            dataGridView1.Columns["BillId"].HeaderText = "Bill ID";
            dataGridView1.Columns["BilledOn"].HeaderText = "Billed On";
            dataGridView1.Columns["BilledAmount"].HeaderText = "Amount";

            dataGridView1.Columns["BilledOn"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["BilledAmount"].DefaultCellStyle.Format = "C2"; // currency format
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddBillView addBillView = new AddBillView
            {
                Dock = DockStyle.Fill
            };

            // replace everything inside this UserControl
            this.Controls.Clear();
            this.Controls.Add(addBillView);
        }
    }

}
