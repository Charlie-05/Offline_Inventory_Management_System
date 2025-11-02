using Offline_Inventory_Management_System.Models;
using Offline_Inventory_Management_System.Repositories;
using Offline_Inventory_Management_System.Views.MyViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Offline_Inventory_Management_System.Views
{
    public partial class ProductPage : UserControl
    {
        private BindingList<Product> productBindingList;
        private DataGridViewRow editingRow = null; // currently edited row

        public ProductPage()
        {
            InitializeComponent();
            this.Load += onView_Load;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            // Use RowHeaderMouseClick for editing
            dataGridView1.RowHeaderMouseClick -= dataGridView1_RowHeaderMouseClick;
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
        }

        private void onView_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadLowStockProducts();
        }

        // Load main products grid
        private void LoadProducts()
        {
            var products = GetAllProducts();
            products.ForEach(p => p.ProductCategoryName = p.ProductCategory?.Name ?? "(No Category)");

            productBindingList = new BindingList<Product>(products);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Add normal columns
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "ProductName", Name = "ProductName", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", Name = "Quantity", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price", Name = "Price", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock Alert On", DataPropertyName = "StockAlertOn", Name = "StockAlertOn", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category", DataPropertyName = "ProductCategoryName", Name = "ProductCategory", ReadOnly = true });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", Name = "ProductId", Visible = false });

            dataGridView1.DataSource = productBindingList;


            dataGridView1.CellClick -= dataGridView1_CellClick;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        // Triggered when row selection changes
        // Row header clicked -> enable editing
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            // Remove Save button from previous row
            if (editingRow != null && editingRow != row)
            {
                MakeCellsEditable(editingRow, false);
                RemoveSaveButton();
            }

            // Make current row editable
            MakeCellsEditable(row, true);
            AddSaveButton();
            editingRow = row;
        }

        // Handle Save button click
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Only handle Save button click
            if (dataGridView1.Columns[e.ColumnIndex].Name == "SaveBtn")
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var product = row.DataBoundItem as Product;

                MakeCellsEditable(row, false);
                RemoveSaveButton();

                EditProduct(product);
                dataGridView1.Refresh();
            }
        }

        private void EditProduct(Product product)
        {
            ProductRepo repo = new ProductRepo();
            repo.UpdateProduct(product);
            MessageBox.Show($"{product.ProductName} updated successfully!");
        }

        private void MakeCellsEditable(DataGridViewRow row, bool editable)
        {
            row.Cells["ProductName"].ReadOnly = !editable;
            row.Cells["Quantity"].ReadOnly = !editable;
            row.Cells["Price"].ReadOnly = !editable;
            row.Cells["StockAlertOn"].ReadOnly = !editable;
        }

        private void AddSaveButton()
        {
            if (!dataGridView1.Columns.Contains("SaveBtn"))
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Action",
                    Name = "SaveBtn",
                    Text = "Save",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                };
                dataGridView1.Columns.Add(btnColumn);
            }
        }

        private void RemoveSaveButton()
        {
            if (dataGridView1.Columns.Contains("SaveBtn"))
            {
                dataGridView1.Columns.Remove("SaveBtn");
                editingRow = null;
            }
        }

        // Load low-stock products grid
        private void LoadLowStockProducts()
        {
            var lowStockProducts = GetLowStockProducts();
            lowStockProducts.ForEach(p => p.ProductCategoryName = p.ProductCategory?.Name ?? "(No Category)");

            var lowStockList = new BindingList<Product>(lowStockProducts);

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "ProductName", Name = "ProductName", ReadOnly = true });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", Name = "Quantity", ReadOnly = true });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price", Name = "Price", ReadOnly = true });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock Alert On", DataPropertyName = "StockAlertOn", Name = "StockAlertOn", ReadOnly = true });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", Name = "ProductId", Visible = false });

            dataGridView2.DataSource = lowStockList;
        }

        private List<Product> GetAllProducts()
        {
            ProductRepo repo = new ProductRepo();
            return repo.ReadAllProducs();
        }

        private List<Product> GetLowStockProducts()
        {
            ProductRepo repo = new ProductRepo();
            return repo.GetLowStockProducts();
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            AddCategoryView addCategoryView = new AddCategoryView();
            addCategoryView.Show();
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            using (AddProductView addProductView = new AddProductView())
            {
                addProductView.ShowDialog();
            }
            LoadProducts(); // Refresh after adding product
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim().ToLower();
            List<Product> filtered = string.IsNullOrEmpty(searchText)
                ? GetAllProducts()
                : GetAllProducts().Where(p => p.ProductName.ToLower().Contains(searchText)).ToList();

            filtered.ForEach(p => p.ProductCategoryName = p.ProductCategory?.Name ?? "(No Category)");

            productBindingList = new BindingList<Product>(filtered);
            dataGridView1.DataSource = productBindingList;
        }
    }
}
