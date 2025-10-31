using Offline_Inventory_Management_System.Models;
using Offline_Inventory_Management_System.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Offline_Inventory_Management_System.Views
{
    public partial class ProductView : Form
    {
        public ProductView()
        {
            InitializeComponent();
            this.Load += onView_Load;
        }

        private void onView_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.GetAllProducts();

        }

        private List<Product> GetAllProducts()
        {
            ProductRepo repo = new ProductRepo();
            var res = repo.ReadAllProducs();
            return res;
        }
    }
}
