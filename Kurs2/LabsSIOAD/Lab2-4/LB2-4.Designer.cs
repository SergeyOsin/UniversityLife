using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
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
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label4 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            button1 = new Button();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label14 = new Label();
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            label6 = new Label();
            panel5 = new Panel();
            panel6 = new Panel();
            label11 = new Label();
            label12 = new Label();
            label18 = new Label();
            label5 = new Label();
            label7 = new Label();
            label16 = new Label();
            label13 = new Label();
            label15 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Historic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(145, 1);
            label1.Name = "label1";
            label1.Size = new Size(344, 32);
            label1.TabIndex = 0;
            label1.Text = "Лабораторная работа №4";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(334, 44);
            label2.Name = "label2";
            label2.Size = new Size(273, 30);
            label2.TabIndex = 1;
            label2.Text = "Упорядоченный массив";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Emoji", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(0, 47);
            label3.Name = "label3";
            label3.Size = new Size(276, 27);
            label3.TabIndex = 2;
            label3.Text = "Неупорядоченный массив";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(96, 87);
            numericUpDown1.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(104, 31);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 44454, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(432, 86);
            numericUpDown2.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(104, 31);
            numericUpDown2.TabIndex = 4;
            numericUpDown2.Value = new decimal(new int[] { 98859, 0, 0, 0 });
            // 
            // textBox1
            // 
            textBox1.Location = new Point(96, 171);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 31);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(96, 214);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 31);
            textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(432, 174);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 31);
            textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(432, 218);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 31);
            textBox4.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Symbol", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(0, 137);
            label4.Name = "label4";
            label4.Size = new Size(256, 28);
            label4.TabIndex = 9;
            label4.Text = "Неоптимальный поиск";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(96, 315);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 31);
            textBox5.TabIndex = 11;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(96, 352);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 31);
            textBox6.TabIndex = 12;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(432, 306);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 31);
            textBox7.TabIndex = 15;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(432, 352);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 31);
            textBox8.TabIndex = 16;
            // 
            // button1
            // 
            button1.Location = new Point(56, 405);
            button1.Name = "button1";
            button1.Size = new Size(95, 45);
            button1.TabIndex = 17;
            button1.Text = "Найти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Historic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(12, 90);
            label8.Name = "label8";
            label8.Size = new Size(69, 28);
            label8.TabIndex = 18;
            label8.Text = "Ключ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Emoji", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(334, 90);
            label9.Name = "label9";
            label9.Size = new Size(66, 27);
            label9.TabIndex = 19;
            label9.Text = "Ключ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label10.Location = new Point(4, 174);
            label10.Name = "label10";
            label10.Size = new Size(79, 30);
            label10.TabIndex = 20;
            label10.Text = "Время";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(4, 213);
            label14.Name = "label14";
            label14.Size = new Size(90, 30);
            label14.TabIndex = 24;
            label14.Text = "Индекс";
            // 
            // button2
            // 
            button2.Location = new Point(441, 405);
            button2.Name = "button2";
            button2.Size = new Size(95, 45);
            button2.TabIndex = 28;
            button2.Text = "Найти";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(257, 447);
            button3.Name = "button3";
            button3.Size = new Size(95, 45);
            button3.TabIndex = 29;
            button3.Text = "Выход";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(646, 10);
            panel1.TabIndex = 30;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(0, 74);
            panel2.Name = "panel2";
            panel2.Size = new Size(646, 10);
            panel2.TabIndex = 31;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(0, 124);
            panel3.Name = "panel3";
            panel3.Size = new Size(646, 10);
            panel3.TabIndex = 32;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaptionText;
            panel4.Location = new Point(0, 265);
            panel4.Name = "panel4";
            panel4.Size = new Size(646, 10);
            panel4.TabIndex = 33;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Symbol", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(334, 137);
            label6.Name = "label6";
            label6.Size = new Size(256, 28);
            label6.TabIndex = 34;
            label6.Text = "Неоптимальный поиск";
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ActiveCaptionText;
            panel5.Location = new Point(304, 47);
            panel5.Name = "panel5";
            panel5.Size = new Size(10, 348);
            panel5.TabIndex = 35;
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.ActiveCaptionText;
            panel6.Location = new Point(0, 389);
            panel6.Name = "panel6";
            panel6.Size = new Size(646, 10);
            panel6.TabIndex = 36;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label11.Location = new Point(337, 173);
            label11.Name = "label11";
            label11.Size = new Size(79, 30);
            label11.TabIndex = 37;
            label11.Text = "Время";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(334, 218);
            label12.Name = "label12";
            label12.Size = new Size(90, 30);
            label12.TabIndex = 38;
            label12.Text = "Индекс";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI Symbol", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(345, 275);
            label18.Name = "label18";
            label18.Size = new Size(231, 28);
            label18.TabIndex = 39;
            label18.Text = "Оптимальный поиск";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Symbol", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 278);
            label5.Name = "label5";
            label5.Size = new Size(231, 28);
            label5.TabIndex = 40;
            label5.Text = "Оптимальный поиск";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label7.Location = new Point(4, 314);
            label7.Name = "label7";
            label7.Size = new Size(79, 30);
            label7.TabIndex = 41;
            label7.Text = "Время";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label16.Location = new Point(337, 307);
            label16.Name = "label16";
            label16.Size = new Size(79, 30);
            label16.TabIndex = 42;
            label16.Text = "Время";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(0, 351);
            label13.Name = "label13";
            label13.Size = new Size(90, 30);
            label13.TabIndex = 43;
            label13.Text = "Индекс";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(337, 353);
            label15.Name = "label15";
            label15.Size = new Size(90, 30);
            label15.TabIndex = 44;
            label15.Text = "Индекс";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 495);
            Controls.Add(label15);
            Controls.Add(label13);
            Controls.Add(label16);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label18);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(label6);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label14);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(button1);
            Controls.Add(textBox8);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(label4);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Osin";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private Button button1;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label14;
        private Button button2;
        private Button button3;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label label6;
        private Panel panel5;
        private Panel panel6;
        private Label label11;
        private Label label12;
        private Label label18;
        private Label label5;
        private Label label7;
        private Label label16;
        private Label label13;
        private Label label15;
    }
}
