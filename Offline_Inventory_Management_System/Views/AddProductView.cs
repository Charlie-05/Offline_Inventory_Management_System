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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var categoryName = textBox1.Text;
            try
            {
                if (string.IsNullOrEmpty(categoryName))
                {
                    throw new Exception("Enetr a valid category");
                }
                else
                {
                    ProductCategoryRepo repo = new ProductCategoryRepo();
                    ProductCategory category = new ProductCategory() { Name = categoryName };
                    repo.AddProductCategory(category);
                }
            }
            catch (Exception ex) {

            }
            
        }
    }
}
