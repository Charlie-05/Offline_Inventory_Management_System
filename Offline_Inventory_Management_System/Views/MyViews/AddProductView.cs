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
            try
            {
                // Validate empty fields
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Product name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Please select a product category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Price, Quantity, and Stock Alert fields cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate numeric fields
                if (!decimal.TryParse(textBox2.Text, out decimal price))
                {
                    MessageBox.Show("Invalid price. Please enter a valid decimal number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBox3.Text, out decimal quantity))
                {
                    MessageBox.Show("Invalid quantity. Please enter a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBox4.Text, out decimal stockAlertOn))
                {
                    MessageBox.Show("Invalid stock alert value. Please enter a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Category ID validation
                int categoryId;
                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out categoryId))
                {
                    MessageBox.Show("Invalid category selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create product object
                Product product = new Product()
                {
                    ProductName = textBox1.Text.Trim(),
                    ProductCategoryId = categoryId,
                    Price = price,
                    Quantity = quantity,
                    StockAlertOn = stockAlertOn
                };

                // Add product to repository
                ProductRepo productRepo = new ProductRepo();
                var res = productRepo.AddProduct(product);

                MessageBox.Show($"Product '{res.ProductName}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally clear form fields
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
