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

namespace Offline_Inventory_Management_System.Views.MyViews
{
    public partial class AddCategoryView : Form
    {
        public AddCategoryView()
        {
            InitializeComponent();
            this.Load += onView_Load;
        }

        private void onView_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = this.GetUnits();
            comboBox1.DisplayMember = "Label";
            comboBox1.ValueMember = "Key";

        }

        public List<UnitOfMeasurement> GetUnits()
        {
            List<UnitOfMeasurement> units = new List<UnitOfMeasurement>
            {
                new UnitOfMeasurement { Label  = "Number", Key = "" },
                new UnitOfMeasurement { Label = "Kilograms", Key = "kg" },
                new UnitOfMeasurement { Label = "Litres", Key = "l" }
            };
            return units;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string categoryName = textBox1.Text;
            string unit = comboBox1.SelectedValue.ToString();
            ProductCategory category = new ProductCategory();
            category.Name = categoryName;
            category.UnitOfMeasurement = unit;

            ProductCategoryRepo repo = new ProductCategoryRepo();
            var res = repo.AddProductCategory(category);
            MessageBox.Show(res.Name);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
