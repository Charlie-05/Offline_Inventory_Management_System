using Offline_Inventory_Management_System.Models;
using Offline_Inventory_Management_System.Views.MyPages;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Router.Initialize(panel2);

            // Load the default route
            Router.Navigate(new ProductPage());

            // Wire button clicks
            button1.Click += (s, e) => Router.Navigate(new ProductPage());
            button2.Click += (s, e) => Router.Navigate(new OrderPage());
            button3.Click += (s, e) => Router.Navigate(new BillPage());
            button4.Click += (s, e) => Router.Navigate(new UserPage());

            ApplyRolePermissions();
        }

        private void ApplyRolePermissions()
        {
            if (AppUser.CurrentUser == null)
            {
                MessageBox.Show("User not logged in.");
                return;
            }

            switch ((Role)AppUser.CurrentUser.UserRoleID)
            {
                case Role.Admin:
                    // Admin can access all buttons
                    button1.Enabled = true;  // Product
                    button2.Enabled = true;  // Order
                    button3.Enabled = true;  // Bill
                    button4.Enabled = true;  // User
                    break;

                case Role.Manager:
                    // Manager can access Product, Order, Bill
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = false; // cannot access Users
                    break;

                case Role.Cashier:
                    // Cashier can only access Bill
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = false;
                    break;

                default:
                    // default — deny everything if role is unknown
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    break;
            }
        }

    }
}
