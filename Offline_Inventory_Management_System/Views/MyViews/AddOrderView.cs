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
using Timer = System.Windows.Forms.Timer;

namespace Offline_Inventory_Management_System.Views.MyViews
{
    public partial class AddOrderView : UserControl
    {

        private Timer _typingTimer;
        public AddOrderView()
        {
            InitializeComponent();
            SetupComboBox();
            SetupTypingTimer();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            Label lbl = new Label
            {
                Text = "Order Page",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            _typingTimer?.Stop();
            _typingTimer.Start();
        }

        private async Task LoadSearchProducts(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                return;
            }
            try
            {
                ProductRepo productRepo = new ProductRepo();
                List<Product> products = productRepo.ReadAllProducs(productName);
                comboBox1.BeginUpdate();
                comboBox1.Items.Clear();
                foreach (Product product in products)
                {
                    comboBox1.Items.Add(product);
                }
                comboBox1.DroppedDown = true;
                comboBox1.SelectionStart = comboBox1.Text.Length;
                comboBox1.SelectionLength = 0;
                comboBox1.EndUpdate();

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // ProductId (hidden)
                var colId = new DataGridViewTextBoxColumn
                {
                    Name = "ProductId",
                    HeaderText = "Product ID",
                    Visible = false
                };
                dataGridView1.Columns.Add(colId);

                // ProductName
                var colName = new DataGridViewTextBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Product Name",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                dataGridView1.Columns.Add(colName);

                // Quantity
                var colQty = new DataGridViewTextBoxColumn
                {
                    Name = "Quantity",
                    HeaderText = "Quantity"
                };
                dataGridView1.Columns.Add(colQty);
            }

            // Validation
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            if (!int.TryParse(textBox1.Text, out int qty) || qty <= 0)
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

        
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.IsNewRow) continue;

                if (Convert.ToInt32(r.Cells["ProductId"].Value) == selectedProduct.ProductId)
                {
                    MessageBox.Show("This product is already added.");
                    return;
                }
            }

         
            int newIndex = dataGridView1.Rows.Add();
            DataGridViewRow newRow = dataGridView1.Rows[newIndex];

            newRow.Cells["ProductId"].Value = selectedProduct.ProductId;
            newRow.Cells["ProductName"].Value = selectedProduct.ProductName;
            newRow.Cells["Quantity"].Value = qty;

            // Clear fields
            comboBox1.Text = "";
            textBox1.Text = "";
            comboBox1.Focus();
        }



        private List<OrderProduct> CollectOrderItems()
        {
            var items = new List<OrderProduct>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                items.Add(new OrderProduct
                {
                    ProductId = Convert.ToInt32(row.Cells["ProductId"].Value),
                    OrderQuantity = Convert.ToInt32(row.Cells["Quantity"].Value)
                });
            }

            return items;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal orderAmount = int.Parse(textBox2.Text);
            DateTime currentTime = DateTime.Now;
            Order order = new Order();
            order.OrderAmount = orderAmount;
            order.OrderedOn = currentTime;

            OrderRepo orderRepo = new OrderRepo();

            var res = orderRepo.AddOrder(order);
            List<OrderProduct> orderProductsRes = new List<OrderProduct>();
            if (res != null)
            {
                OrderProductRepo orderProductRepo = new OrderProductRepo();
                ProductRepo productRepo = new ProductRepo();
                var orderProductReq = this.CollectOrderItems();
                foreach (var orderProduct in orderProductReq)
                {
                    orderProduct.OrderId = res.OrderId;
                    var orderProductRes = orderProductRepo.AddOrder(orderProduct);
                    if (orderProductRes != null)
                    {
                        productRepo.UpdateProductQuantity(orderProductRes.ProductId, orderProductRes.OrderQuantity, true);
                    }
                    orderProductsRes.Add(orderProductRes);
                }

                if (orderProductsRes.Count > 0)
                {
                    MessageBox.Show("Order Added Succesful");
                }
            }
            else
            {
                throw new Exception();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        { 
            Router.Navigate(new OrderPage());
        }
    }
}
