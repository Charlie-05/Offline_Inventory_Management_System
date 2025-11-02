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
        }
    }
}
