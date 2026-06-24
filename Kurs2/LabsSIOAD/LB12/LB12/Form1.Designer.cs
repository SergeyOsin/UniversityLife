namespace LB12
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            checkbox = new DataGridViewCheckBoxColumn();
            Сравнения = new DataGridViewTextBoxColumn();
            вв = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            com = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Symbol", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(347, -1);
            label1.Name = "label1";
            label1.Size = new Size(315, 30);
            label1.TabIndex = 1;
            label1.Text = "Лабораторная работа №21";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(597, 352);
            numericUpDown1.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(160, 31);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 1000000, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Symbol", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(394, 350);
            label2.Name = "label2";
            label2.Size = new Size(197, 30);
            label2.TabIndex = 4;
            label2.Text = "Размер массива";
            // 
            // button1
            // 
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(953, 345);
            button1.Name = "button1";
            button1.Size = new Size(104, 42);
            button1.TabIndex = 5;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(2, 345);
            button2.Name = "button2";
            button2.Size = new Size(157, 42);
            button2.TabIndex = 6;
            button2.Text = "Сортировать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { checkbox, Сравнения, вв, Column1, Column2, com });
            dataGridView1.Location = new Point(2, 32);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1055, 299);
            dataGridView1.TabIndex = 7;
            // 
            // checkbox
            // 
            checkbox.HeaderText = "";
            checkbox.MinimumWidth = 8;
            checkbox.Name = "checkbox";
            checkbox.Resizable = DataGridViewTriState.True;
            checkbox.SortMode = DataGridViewColumnSortMode.Automatic;
            checkbox.Width = 150;
            // 
            // Сравнения
            // 
            Сравнения.HeaderText = "";
            Сравнения.MinimumWidth = 8;
            Сравнения.Name = "Сравнения";
            Сравнения.Width = 150;
            // 
            // вв
            // 
            вв.HeaderText = "Сравнения";
            вв.MinimumWidth = 8;
            вв.Name = "вв";
            вв.Width = 200;
            // 
            // Column1
            // 
            Column1.HeaderText = "Перестановки";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.Width = 175;
            // 
            // Column2
            // 
            Column2.FillWeight = 90F;
            Column2.HeaderText = "Время";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.Width = 165;
            // 
            // com
            // 
            com.HeaderText = "Отсортировано?";
            com.MinimumWidth = 8;
            com.Name = "com";
            com.Width = 150;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1061, 389);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Osin_LR12-17";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private DataGridViewCheckBoxColumn checkbox;
        private DataGridViewTextBoxColumn Сравнения;
        private DataGridViewTextBoxColumn вв;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn com;
    }
}
