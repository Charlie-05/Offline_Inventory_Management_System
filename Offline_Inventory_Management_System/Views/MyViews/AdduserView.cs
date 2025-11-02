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
    public partial class AdduserView : Form
    {
        public AdduserView()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Role))
                 .Cast<Role>()
                 .Select(r => new { UserRoleId = (int)r, Label = r.ToString() })
                 .ToList();
            comboBox1.DisplayMember = "Label";
            comboBox1.ValueMember = "UserRoleId";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                User newUser = new User
                {
                    UserName = textBox1.Text.Trim(), // Username
                    Password = textBox2.Text.Trim(), // Password
                    Name = textBox3.Text.Trim(),     // Name
                    UserRoleID = Convert.ToInt32(comboBox1.SelectedValue) // UserRoleId from ComboBox
                };

                var userRepo = new UserRepo();
                User addedUser = userRepo.AddUser(newUser);

                MessageBox.Show($"User '{addedUser.Name}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

       
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
