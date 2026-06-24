using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB3_VA
{
    
    public partial class Lab3 : Form
    {
        private const int COUNT = 2;
        private const double EXP = 2.71;
        private double left;
        private double right;
        private double second_ratio;
        private double third_ratio;
        private double accurary;
        public Lab3()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double acc = 10.0;
            for (int i = 2; i < 10; i++)
                comboBox1.Items.Add(Math.Pow(acc, -1*i));
            comboBox1.SelectedIndex = 0;
            textBox2.Text = 1.ToString();
            textBox3.Text = 1.7.ToString();
            Table();
        }
        
        private void Table()
        {
            string[] names_method = { "Метод бисекции", "Метод Ньютона", "Метод секущих", "Метод итераций" };
            dataGridView1.RowCount = COUNT + COUNT;
            dataGridView1.ColumnCount = COUNT;
            for (int i = 0; i < COUNT+COUNT; i++)
                dataGridView1.Rows[i].Cells[0].Value = names_method[i];
            dataGridView1.RowHeadersWidth = 4;
        }
        private void button1_Click(object sender, EventArgs e) => Application.Exit();

        private double func(double x) =>  Math.Pow(EXP, x) - second_ratio * x * x + third_ratio;
        private double diff_func1(double x) =>  Math.Pow(EXP, x) - second_ratio * 2 * x;
        private double diff_func2(double x) =>  Math.Pow(EXP, x) - second_ratio * 2;
        private void ReadTextBoxes()
        {
            try
            {
                second_ratio = Convert.ToDouble(textBox2.Text);
                third_ratio = Convert.ToDouble(textBox3.Text);
                accurary = Convert.ToDouble(comboBox1.SelectedItem);
                left = Convert.ToDouble(numericUpDown1.Value);
                right = Convert.ToDouble(numericUpDown2.Value);
            }
            catch (FormatException)
            {
                throw new Exception("Введите корректные значения!");
            }
        }
        private void Analytic()
        {
            double count = Convert.ToDouble(numericUpDown3.Value);
            List<double> list = new List<double>();
            list.Add(left);
            int index = 1;
            double middle = (right - left) / count;
            list.Add(left + middle);
            while (index < count && func(list[index]) * func(list[index - 1]) > 0)
            {
                index++;
                middle = (right - left) / count;
                list.Add(left + index * middle);
            }
            if (index < count)
            {
                left = list[index - 1]; 
                right = list[index];    
            }
            else
            {
                MessageBox.Show("Не удалось найти корень на заданном интервале.");
                return; 
            }
        }

        private double bisection(double a, double b)
        {
            double aCopy = a, bCopy = b;
            int maxIterations = 100;
            int iteration = 0;
            double middle = (aCopy + bCopy) / 2;
            while (Math.Abs(bCopy - aCopy) > accurary && iteration < maxIterations)
            {
                if (Math.Abs(func(middle)) <= accurary)
                    return middle;
                if (func(aCopy) * func(middle) < 0)
                    bCopy = middle;
                else
                    aCopy = middle;
                middle = (aCopy + bCopy) / 2;
                iteration++;
            }
            return middle;
        }

        private double Method_Newton(double a, double b)
        {
            double aCopy = a, bCopy = b;
            List<double> valueK = new List<double>();
            if (func(aCopy) * diff_func2(aCopy) > 0)
                valueK.Add(aCopy);
            else
                valueK.Add(bCopy);
            valueK.Add(valueK[0] - func(valueK[0]) / diff_func1(valueK[0]));
            int index = 1;
            while (Math.Abs(valueK[index] - valueK[index - 1]) > accurary && index < 100)  
            {
                valueK.Add(valueK[index] - func(valueK[index]) / diff_func1(valueK[index]));
                index++;
            }
            return valueK[index];
        }

        private double Method_Secant(double a, double b)
        {
            double x0 = a; 
            double x1 = b; 
            while (Math.Abs(x1 - x0) > accurary)
            {
                double x2 = x1 - ((x1 - x0) / (func(x1) - func(x0))) * func(x1);
                x0 = x1; 
                x1 = x2; 
            }
            return x1;
        }
        private double Iteration(double a, double b)
        {
            double x0 = a;
            double x1 = phi(x0); 
            while (Math.Abs(x1 - x0) > accurary)
            {
                x0 = x1;
                x1 = phi(x0);
            }
            return x1;
        }
        private double phi(double x)
        {
            return Math.Sqrt(Math.Exp(x) + third_ratio);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReadTextBoxes();
            Analytic();
            int digits = comboBox1.SelectedIndex + 2;
            dataGridView1.Rows[0].Cells[1].Value = Math.Round(bisection(left,right), digits);
            dataGridView1.Rows[1].Cells[1].Value = Math.Round(Method_Newton(left,right), digits);
            dataGridView1.Rows[2].Cells[1].Value = Math.Round(Method_Secant(left,right), digits);
            dataGridView1.Rows[3].Cells[1].Value = Iteration(left, right);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Labs_VA_2_8.Form1().Show();
        }
    }
}
