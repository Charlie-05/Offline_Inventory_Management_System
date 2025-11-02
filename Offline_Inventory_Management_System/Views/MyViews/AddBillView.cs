using Offline_Inventory_Management_System.Models;
using Offline_Inventory_Management_System.Repositories;
using Offline_Inventory_Management_System.Views.MyPages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Offline_Inventory_Management_System.Views.MyViews
{
    public partial class AddBillView : UserControl
    {
        private Timer _typingTimer;

        public AddBillView()
        {
            InitializeComponent();
            SetupComboBox();
            SetupTypingTimer();

            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // Make total textbox readonly so user can’t edit it manually
            textBoxTotal.ReadOnly = true;

            Label lbl = new Label
            {
                Text = "Billing Page",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 50
            };

            this.Controls.Add(lbl);
        }

        private void SetupComboBox()
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.None;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.TextChanged += comboBox1_TextChanged;
            comboBox1.DisplayMember = "ProductName";
            comboBox1.ValueMember = "ProductId";
        }

        private void SetupTypingTimer()
        {
            _typingTimer = new Timer();
            _typingTimer.Interval = 400;
            _typingTimer.Tick += async (s, e) =>
            {
                _typingTimer.Stop();
                await LoadSearchProducts(comboBox1.Text);
            };
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            _typingTimer?.Stop();
            _typingTimer.Start();
        }

        private async Task LoadSearchProducts(string productName)
        {
            if (string.IsNullOrEmpty(productName))
                return;

            try
            {
                ProductRepo productRepo = new ProductRepo();
                List<Product> products = productRepo.ReadAllProducs(productName);

                comboBox1.BeginUpdate();
                comboBox1.Items.Clear();
                foreach (Product product in products)
                    comboBox1.Items.Add(product);

                comboBox1.DroppedDown = true;
                comboBox1.SelectionStart = comboBox1.Text.Length;
                comboBox1.SelectionLength = 0;
                comboBox1.EndUpdate();
            }
            catch
            {
                MessageBox.Show("Error loading products.");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Ensure columns exist
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ProductId",
                    HeaderText = "Product ID",
                    Visible = false
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Product Name"
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Quantity",
                    HeaderText = "Quantity"
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Price",
                    HeaderText = "Price"
                });
            }

            // Validate selection and quantity
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            if (!decimal.TryParse(textBoxQty.Text, out decimal qty) || qty <= 0)
            {
                MessageBox.Show("Enter a valid quantity.");
                return;
            }

            Product selectedProduct = comboBox1.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Invalid product selection.");
                return;
            }

            // You can re-fetch full product details here:
            ProductRepo productRepo = new ProductRepo();
            Product productFromDb = productRepo.ReadProductById(selectedProduct.ProductId);

            if (productFromDb == null)
            {
                MessageBox.Show("Product not found in database.");
                return;
            }

            // Check duplicates
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.IsNewRow) continue;
                if ((int)r.Cells["ProductId"].Value == productFromDb.ProductId)
                {
                    MessageBox.Show("This product is already added to the bill.");
                    return;
                }
            }

            // Add to grid
            dataGridView1.Rows.Add(
                productFromDb.ProductId,
                productFromDb.ProductName,
                qty,
                productFromDb.Price
            );

            // Update total
            CalculateTotalAmount();

            // Clear UI
            comboBox1.Text = "";
            textBoxQty.Text = "";
            comboBox1.Focus();
        }



        private void CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Ensure both Quantity and Price are valid
                if (decimal.TryParse(row.Cells["Quantity"].Value?.ToString(), out decimal qty) &&
                    decimal.TryParse(row.Cells["Price"].Value?.ToString(), out decimal price))
                {
                    total += qty * price;
                }
            }

            textBoxTotal.Text = total.ToString("0.00");
        }

        private List<BillProduct> CollectBillItems()
        {
            var items = new List<BillProduct>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                items.Add(new BillProduct
                {
                    ProductID = Convert.ToInt32(row.Cells["ProductId"].Value),
                    BillQuantity = Convert.ToDecimal(row.Cells["Quantity"].Value)
                });
            }

            return items;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBoxTotal.Text, out decimal totalAmount) || totalAmount <= 0)
            {
                MessageBox.Show("No valid bill items found.");
                return;
            }

            Bill bill = new Bill
            {
                BilledAmount = totalAmount,
                BilledOn = DateTime.Now
            };

            BillRepo billRepo = new BillRepo();
            var res = billRepo.AddBill(bill);

            if (res != null)
            {
                BillProductRepo billProductRepo = new BillProductRepo();
                ProductRepo productRepo = new ProductRepo();

                var billItems = CollectBillItems();
                foreach (var item in billItems)
                {
                    item.BillId = res.BillId;
                    var bpRes = billProductRepo.AddBillProduct(item);
                    if (bpRes != null)
                    {
                        var product = productRepo.ReadProductById(bpRes.ProductID);
                        if (product.Quantity < bpRes.BillQuantity)
                        {
                            MessageBox.Show($"Not enough stock for {product.ProductName}. Available: {product.Quantity}");
                            continue; // or return to cancel the bill
                        }
                        productRepo.UpdateProductQuantity(bpRes.ProductID, bpRes.BillQuantity, false);
                    }
                }

                MessageBox.Show("Bill added successfully.");
                dataGridView1.Rows.Clear();
                textBoxTotal.Text = "0.00";
            }
            else
            {
                MessageBox.Show("Error while saving bill.");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Router.Navigate(new BillPage());
        }
    }
}
