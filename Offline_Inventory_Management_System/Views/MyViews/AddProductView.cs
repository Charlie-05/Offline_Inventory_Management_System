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

namespace Offline_Inventory_Management_System.Views
{
    public partial class AddProductView : Form
    {


        public AddProductView()
        {
            InitializeComponent();
            this.Load += onForm_Load;
        }

        private void onForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = this.load_Categories();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ProductcategoryId";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private List<ProductCategory> load_Categories()
        {
            ProductCategoryRepo repo = new ProductCategoryRepo();
            var res = repo.ReadAllCategories();
            return res;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ProductRepo productRepo = new ProductRepo();

            string name = textBox1.Text;
            int categoryId = (int)comboBox1.SelectedValue;
            int price = int.Parse(textBox2.Text);
            int quantity = int.Parse(textBox3.Text);
            int stockAlertOn = int.Parse(textBox4.Text);

            Product product = new Product() { ProductName = name, ProductCategoryId= categoryId , Price = price , Quantity = quantity , StockAlertOn = stockAlertOn};
            
            var res = productRepo.AddProduct(product);
            MessageBox.Show(res.ProductName);

        }
    }
}
