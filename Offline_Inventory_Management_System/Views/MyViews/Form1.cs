using Offline_Inventory_Management_System.DataBase;

namespace Offline_Inventory_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show(DbConfig.ConnectionString);
            MessageBox.Show(textBox1.Text);

        }
    }
}
