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
            string username = textBox2.Text.Trim();
            string password = textBox1.Text.Trim();

            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(username))
                    throw new Exception("Please enter a username.");
                if (string.IsNullOrEmpty(password))
                    throw new Exception("Please enter a password.");

                var userRepo = new UserRepo();
                User user = userRepo.GetUserByUserName(username);

                if (user != null)
                {
                    // Compare entered password with stored password
                    if (user.Password == password)
                    {
                        MessageBox.Show("Login successful!", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AppUser.CurrentUser = user;
                        // Redirect to MainForm
                        MainForm mainForm = new MainForm();
                       
                        this.Hide();
                        mainForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid password. Please try again.",
                                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("User not found. Please check your username.",
                                    "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
