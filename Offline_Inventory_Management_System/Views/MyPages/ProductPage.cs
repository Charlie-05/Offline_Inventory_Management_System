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

namespace Offline_Inventory_Management_System.Views
{
    public partial class ProductPage : UserControl
    {
        public ProductPage()
        {
            InitializeComponent();
            this.Load += onView_Load;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            Label lbl = new Label
            {
                Text = "Product Page",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(lbl);
        }
        private void onView_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            var products = this.GetAllProducts();

            // Create a view-friendly list with the needed columns
            var displayList = products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Quantity,
                p.Price,
                p.StockAlertOn,
                ProductCategory = p.ProductCategory?.Name ?? "(No Category)"
            }).ToList();

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = displayList;

            // Optional: Rename header text
            dataGridView1.Columns["ProductCategory"].HeaderText = "Category Name";

            // Optional: Hide ProductId column if not needed
            dataGridView1.Columns["ProductId"].Visible = false;
            dataGridView1.Refresh();

        }

        private List<Product> GetAllProducts()
        {
            ProductRepo repo = new ProductRepo();
            var res = repo.ReadAllProducs();
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCategoryView addCategoryView = new AddCategoryView();
            addCategoryView.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddProductView addProductView = new AddProductView();
            addProductView.Show();
        }
    }
}
