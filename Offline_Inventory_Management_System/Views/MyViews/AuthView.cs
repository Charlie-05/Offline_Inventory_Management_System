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
    public partial class AuthView : Form
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Password = textBox1.Text;
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    throw new Exception("Please Enter a Valid password");
                }
                else
                {
                    Password = Password.Trim();
                    if (Password == "12345")
                    {
                        MessageBox.Show("Login Success");
                        MainForm mainForm = new MainForm();
                        this.Hide();
                        mainForm.Show();
                    }
                    else
                    {
                        throw new Exception("Invalid password");
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
