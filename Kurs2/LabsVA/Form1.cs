using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Labs_VA_2_8
{
    public partial class Form1 : Form
    {
        public Form1()=> InitializeComponent();

        private void Form1_Load(object sender, EventArgs e) { }


        private void button1_Click(object sender, EventArgs e)
        {
            LB1_VA.Lab1 Lb = new LB1_VA.Lab1();
            Lb.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LB2_VA.Form1 LB2 = new LB2_VA.Form1();
            LB2.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LB3_VA.Lab3 LB3 = new LB3_VA.Lab3();
            LB3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LB4_VA.Form1 LB45 = new LB4_VA.Form1();
            LB45.Show();
            this.Hide();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            LB6_VA.LB7 LB7 = new LB6_VA.LB7();
            LB7.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LB8_VA.LB8 LB8 = new LB8_VA.LB8();
            LB8.Show();
            this.Hide();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new VA6.MainForm().Show();
            this.Hide();
        }
        private void button8_Click(object sender, EventArgs e) => Application.Exit();

        private void Form1_Load_1(object sender, EventArgs e) => FormBorderStyle = FormBorderStyle.SizableToolWindow;
    }
}
