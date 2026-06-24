namespace L1_V1
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            label4 = new Label();
            textBox4 = new TextBox();
            label5 = new Label();
            textBox5 = new TextBox();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(62, 25);
            label1.TabIndex = 0;
            label1.Text = "Длина";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 56);
            label2.Name = "label2";
            label2.Size = new Size(79, 25);
            label2.TabIndex = 1;
            label2.Text = "Ширина";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Location = new Point(4, 93);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 2;
            label3.Text = "Высота";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(92, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(81, 31);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(90, 59);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(83, 31);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(90, 93);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(81, 31);
            textBox3.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(177, 148);
            button1.Name = "button1";
            button1.Size = new Size(144, 50);
            button1.TabIndex = 6;
            button1.Text = "Вычислить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(-7, 130);
            panel1.Name = "panel1";
            panel1.Size = new Size(561, 12);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(-7, 204);
            panel2.Name = "panel2";
            panel2.Size = new Size(560, 12);
            panel2.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ControlLightLight;
            label4.Cursor = Cursors.No;
            label4.Location = new Point(4, 237);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 9;
            label4.Text = "Площадь";
            label4.Click += label4_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(114, 237);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(95, 31);
            textBox4.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ControlLightLight;
            label5.Cursor = Cursors.No;
            label5.Location = new Point(4, 289);
            label5.Name = "label5";
            label5.Size = new Size(69, 25);
            label5.TabIndex = 11;
            label5.Text = "Объём";
            label5.Click += label5_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(114, 289);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(95, 31);
            textBox5.TabIndex = 12;
            // 
            // button2
            // 
            button2.Location = new Point(448, 312);
            button2.Name = "button2";
            button2.Size = new Size(89, 39);
            button2.TabIndex = 13;
            button2.Text = "Выход";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 363);
            Controls.Add(button2);
            Controls.Add(textBox5);
            Controls.Add(label5);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Panel panel1;
        private Panel panel2;
        private Label label4;
        private TextBox textBox4;
        private Label label5;
        private TextBox textBox5;
        private Button button2;
    }
}
