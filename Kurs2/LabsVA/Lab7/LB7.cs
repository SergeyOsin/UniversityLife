using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB6_VA
{
    public partial class LB7 : Form
    {
        private LB7_FirstEX LB7_1;
        private LB7_SecondEx LB7_2;
        private Random rand = new Random();
        public LB7() => InitializeComponent();
        private void LB7_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Задание 1";
            tabPage2.Text = "Задание 2";
            textBox2.Text = rand.Next(-10, 10).ToString();
            Array.ForEach(new[] { textBox1, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9 }, tb => tb.ReadOnly = true);
            Array.ForEach(new[] { textBox10, textBox11, textBox12, textBox13, textBox14 }, tB => tB.ReadOnly = true);
            double val = 1;
            for (double i = 10.0; i < Math.Pow(10.0, 8); i *= 10.0)
            {
                comboBox1.Items.Add(val / i);
                comboBox2.Items.Add(val / i);
            }
            comboBox1.SelectedItem = 0.01;
            comboBox2.SelectedItem = 0.001;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        private void button1_Click(object sender, EventArgs e) => Application.Exit();

        private void button2_Click(object sender, EventArgs e) => SolveFirst();
        private void SolveFirst()
        {
            int CountNumbs = -1*(int)Math.Log10((double)comboBox2.SelectedItem);
            listBox1.Items.Clear();
            double point = Convert.ToDouble(textBox2.Text);
            LB7_1 = new LB7_FirstEX(Math.E, point,CountNumbs);
            double rez = Math.Round(LB7_1.ForwardDiff(), CountNumbs);
            textBox1.Text = rez.ToString();
            textBox3.Text = (Math.Round(LB7_1.DownDiff(), CountNumbs).ToString());
            textBox4.Text = (Math.Round(LB7_1.TwoDiff(), CountNumbs).ToString());
            string[] res_arr = LB7_1.StrDiff();
            foreach (var i in res_arr)
                listBox1.Items.Add(i);
        }
        private void button3_Click(object sender, EventArgs e) => SolveSecond();
        private void SolveSecond()
        {
            double n = Convert.ToDouble(numericUpDown1.Value);
            double toch = Convert.ToDouble(comboBox1.SelectedItem);
            LB7_2 = new LB7_SecondEx(n,toch);
            double[] arr = LB7_2.ResArr();
            int rez = 0;
            Array.ForEach(new[] { textBox5, textBox6, textBox7,textBox8,textBox9 }, tb => tb.Text = arr[rez++].ToString());
            rez = 0;
            string[] strArr = LB7_2.ResMethod();
            Array.ForEach(new[] { textBox10, textBox11, textBox12, textBox13, textBox14 }, TB => TB.Text = strArr[rez++]); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Labs_VA_2_8.Form1().Show();
        }
    }
}
