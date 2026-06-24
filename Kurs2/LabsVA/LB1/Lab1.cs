using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LB1_VA
{
    public partial class Lab1 : Form
    {
        public Lab1()
        {
            InitializeComponent();
        }
        private string FractoBin(double numb,int pres)
        {
            string tobin = "";
            while (numb>0 && tobin.Length < pres)
            {
                numb *= 2;
                int bit = (int)numb;
                tobin += bit.ToString();
                numb -= bit;
            }
            return tobin;
        }

        private string IntToBin(int numb)
        {
            if (numb == 0)
                return "0";

            string binary = "";
            while (numb > 0)
            {
                int remainder = numb % 2;
                binary = remainder.ToString() + binary;
                numb /= 2;
            }
            return binary;
        }
        private void Count()
        {
            double value = double.Parse(textBox1.Text.Replace(".", ","));
            int numb = (int)value;
            double frac = Math.Round(Math.Abs(value) - numb, 5);
            dataGridView1.Rows[0].Cells[0].Value = (value > 0) ? "0" : "1";
            string binary_numb = IntToBin(numb);
            string binary_frac = FractoBin(frac, 52);
            string binary = binary_numb + '.' + binary_frac;
            label2.Text = (binary_frac.Length > 0) ? $"Число в бинарном виде: {binary}" : $"Число в бинарном виде:{binary_numb+".0"}";
            int index_one = binary.IndexOf('1');
            int index_point = binary.IndexOf('.');
            int mant = index_point - index_one;
            int exp =127 + mant;
            string binary_exp = IntToBin(exp);
            int len = binary_exp.Length;
            while (len < 8)
            {
                dataGridView1.Rows[0].Cells[1].Value += "0";
                len++;
            }
            dataGridView1.Rows[0].Cells[1].Value +=binary_exp;
            string binary_frac1 = binary.Substring(index_one + 1);
            int find_point = binary_frac1.IndexOf('.');
            if (find_point!=-1) binary_frac1 = binary_frac1.Remove(find_point,1);
            int len5 = binary_frac1.Length;
            dataGridView1.Rows[0].Cells[2].Value = (len5 > 23) ? binary_frac1.Substring(0, 22) : binary_frac1;
            int len_frac = binary_frac1.Length;
            while (len_frac < 23)
            {
                dataGridView1.Rows[0].Cells[2].Value += "0";
                len_frac++;
            }
            int Z1 = mant + (int)Math.Pow(2, 11 - 1) - 1;
            string str = Convert.ToString(Z1, 2);
            dataGridView2.Rows[0].Cells[0].Value = (value > 0) ? "0" : "1";
            int len3 = str.Length;
            while (len3 < 11)
            {
                dataGridView2.Rows[0].Cells[1].Value += "0";
                len3++;
            }
            dataGridView2.Rows[0].Cells[1].Value+=str;
            dataGridView2.Rows[0].Cells[2].Value = binary_frac1;
            int len_frac1 = binary_frac1.Length;
            while (len_frac1 < 52)
            {
                dataGridView2.Rows[0].Cells[2].Value += "0";
                len_frac1++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = "";
                dataGridView2.Rows[0].Cells[i].Value = "";
            }
            Count();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
           
        }
    }
}
