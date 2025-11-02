namespace Offline_Inventory_Management_System.Views.MyViews
{
    partial class AddBillView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Designer code

        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            label1 = new Label();
            textBoxQty = new TextBox();
            label2 = new Label();
            buttonAdd = new Button();
            dataGridView1 = new DataGridView();
            labelTotal = new Label();
            textBoxTotal = new TextBox();
            buttonSave = new Button();
            buttonBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(100, 100);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(150, 28);
            comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(130, 75);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 8;
            label1.Text = "Product";
            // 
            // textBoxQty
            // 
            textBoxQty.Location = new Point(280, 100);
            textBoxQty.Name = "textBoxQty";
            textBoxQty.Size = new Size(100, 27);
            textBoxQty.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(290, 75);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 7;
            label2.Text = "Quantity";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(418, 101);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(30, 27);
            buttonAdd.TabIndex = 5;
            buttonAdd.Text = "+";
            buttonAdd.Click += buttonAdd_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Location = new Point(60, 150);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(600, 200);
            dataGridView1.TabIndex = 4;
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Location = new Point(240, 380);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(102, 20);
            labelTotal.TabIndex = 3;
            labelTotal.Text = "Total Amount:";
           
            // 
            // textBoxTotal
            // 
            textBoxTotal.Location = new Point(360, 377);
            textBoxTotal.Name = "textBoxTotal";
            textBoxTotal.ReadOnly = true;
            textBoxTotal.Size = new Size(120, 27);
            textBoxTotal.TabIndex = 2;
            textBoxTotal.ReadOnly = true;
          
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(530, 376);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(100, 30);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "Save Bill";
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonBack
            // 
            buttonBack.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            buttonBack.Location = new Point(30, 30);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(70, 30);
            buttonBack.TabIndex = 0;
            buttonBack.Text = "<--";
            buttonBack.Click += buttonBack_Click;
            // 
            // AddBillView
            // 
            Controls.Add(buttonBack);
            Controls.Add(buttonSave);
            Controls.Add(textBoxTotal);
            Controls.Add(labelTotal);
            Controls.Add(dataGridView1);
            Controls.Add(buttonAdd);
            Controls.Add(textBoxQty);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Name = "AddBillView";
            Size = new Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private TextBox textBoxQty;
        private Label label2;
        private Button buttonAdd;
        private DataGridView dataGridView1;
        private Label labelTotal;
        private TextBox textBoxTotal;
        private Button buttonSave;
        private Button buttonBack;
    }
}
