using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB2_VA
{
    public partial class Form1 : Form
    {
        const double PI = Math.PI;
        const int MAX_SIZE = 100000;

        public Form1() => InitializeComponent();

        private void button2_Click(object sender, EventArgs e) => Application.Exit();
        private (double,double) XandPow()
        {
            double X = Convert.ToDouble(textBox1.Text);
            double pow = Convert.ToDouble(numericUpDown1.Value);
            return (X, pow);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            label3.Text = " ";
        }

        private void button3_Click(object sender, EventArgs e)=> textBox1.Text += "1";

        private void button4_Click(object sender, EventArgs e)=> textBox1.Text += "2";

        private void button5_Click(object sender, EventArgs e) => textBox1.Text += "3";

        private void button6_Click(object sender, EventArgs e) => textBox1.Text += "4";

        private void button7_Click(object sender, EventArgs e)=> textBox1.Text += "5";

        private void button8_Click(object sender, EventArgs e) => textBox1.Text += "6";

        private void button9_Click(object sender, EventArgs e) =>  textBox1.Text += "7";

        private void button10_Click(object sender, EventArgs e)=> textBox1.Text += "8";

        private void button11_Click(object sender, EventArgs e)=>textBox1.Text += "9";
            

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                MessageBox.Show("Число не может начинаться с точки!");
                return;
            }
            if (textBox1.Text.Contains(","))
            {
                MessageBox.Show("Не может быть больше 1 запятой в числе!");
                return;
            }
            textBox1.Text += ",";
        }

        private void button13_Click(object sender, EventArgs e)=> textBox1.Text += "0";
        private void Error(double X,char name_oper)
        {
            if (name_oper == 's')
            {
                if (X < 0)
                    throw new Exception("Ошибка! X не должно быть отрицательным!");
            }
            else if (X <= 0)
                throw new Exception("Ошибка! X должно быть строго положительным!");
        }
        private void ErrorTrig(double X,char t)
        {
            if (t == 't' && (X % 90==0 && X!=0))
            {
                label3.Text = "∞";
            }
            else if (t=='c' && X%180==0)
            {
                label3.Text = "∞";
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            if (X == 0)
            {
                label3.Text = "0";
                return;
            }
            Error(X,'s');
            double pow = XandPow().Item2;
            double accurary = Math.Pow(10.0, pow);
            List<double> listX = new List<double>() { 1.0 };
            listX.Add((0.5 * (listX[0] + X / listX[0])));
            int i = 1;
            while (Math.Abs(listX[i] - listX[i - 1]) > accurary)
            {
                i++;
                listX.Add( 0.5 * (listX[i - 1] + X / listX[i - 1]));
            }
            label3.Text = "="+(Math.Round(listX[i], Math.Abs((int)pow))).ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            double pow = XandPow().Item2;
            double accurary = Math.Pow(10.0, pow);
            List<double> listU = new List<double>() { 1.0 };
            List<double> listS = new List<double>() { 1.0 };
            int i = 0;
            while (Math.Abs(listU[i]) > accurary)
            {
                i++;
                listU.Add( X / i * listU[i - 1]);
                listS.Add(listS[i - 1] + listU[i]);
            }
            label3.Text = "=" + (Math.Round(listS[i], Math.Abs((int)pow))).ToString();
        }
        private double LNX(double X)
        {
            Error(X, 'a');
            double pow = XandPow().Item2;
            double accurary = Math.Pow(10.0, pow);
            List<double> listU = new List<double>() { (1 - X) / (1 + X) };
            List<double> listS = new List<double>() { listU[0] };
            int j = 1;
            while (Math.Abs(listU[j - 1]) > accurary)
            {
                listU.Add(Math.Pow(listU[0], 2 * j + 1) / (2 * j + 1));
                listS.Add(listS[j - 1] + listU[j]);
                j++;
            }
            return listS[j - 1];
            
        }
        private void button16_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            LNX(X);
            label3.Text = "=" + (Math.Round(-1 * 2 * LNX(X), Math.Abs((int)XandPow().Item2))).ToString();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            Error(X,'b');
            double pow = XandPow().Item2;
            double precision = Math.Pow(10.0, pow);
            double low = 0.0;
            double high = X;
            while (high - low > precision)
            {
                double mid = (low + high) / 2.0;
                double midValue = Math.Pow(10.0, mid);
                if (midValue < X)
                    low = mid;
                else
                    high = mid;
            }
            double u = (low + high) / 2.0;
            //double u=LNX(X)/LNX(10);
            label3.Text = "=" + Math.Round(u, Math.Abs((int)pow)).ToString();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            int power = (int)XandPow().Item2;
            double rad = sinX(X * PI/180);
            double rez_radian = sinX(X%(2*PI));
            label3.Text = "Градусы: " + Math.Round(rad,-1*power).ToString() + " Радианы: " +Math.Round(rez_radian, -1*power).ToString();
        }
        private double sinX(double X)
        {
            double pow = XandPow().Item2;
            double accurary = Math.Pow(10.0, pow); 
            List<double> listU = new List<double>(); 
            List<double> listS = new List<double>(); 
            listU.Add(X); 
            listS.Add(X); 
            int j = 1;
            while (Math.Abs(listU[j - 1]) > accurary)
            {
                double uj = -X * X / ((2 * j) * (2 * j + 1)) * listU[j - 1];
                listU.Add(uj);
                listS.Add(listS[j - 1] + uj);
                j++;
            }
            return listS[j-1];
        }

        private void button22_Click(object sender, EventArgs e) => textBox1.Text += "-";

        private void button19_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            int power = -1 * ((int)XandPow().Item2);
            double rez_gr = CosX(X*PI/180);
            double rez_rad = CosX(X % (2 * PI));
            label3.Text = "Градусы: " + Math.Round(rez_gr, power) + " Радианы: " + Math.Round(rez_rad, power);
        }
        private double CosX(double X)
        {
            double pow = XandPow().Item2;
            double accurary = Math.Pow(10.0, pow);
            List<double> listU = new List<double>();
            List<double> listS = new List<double>();
            listU.Add(1.0);
            listS.Add(1.0);
            int j = 1;
            while (Math.Abs(listU[j - 1]) > accurary){
                listU.Add(-X * X / (2 * j * (2 * j - 1)) * listU[j - 1]);
                listS.Add(listS[j - 1] + listU[j]);
                j++;
            }
            return listS[j - 1];
        }

        private void button20_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            int power = -1 * ((int)XandPow().Item2);
            double rez_gr = (sinX(X * PI / 180) / CosX(X * PI / 180));
            double rez_rad = (sinX(X % (PI)) / CosX(X % PI));
            if (X!=0 && (X==90 || X%270==0))
            {
                label3.Text = "Градусы: бесконечность" + " Радианы: " + Math.Round(rez_rad, power).ToString();
            }
            else label3.Text =" Радианы: " + Math.Round(rez_rad,power).ToString();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            double X = XandPow().Item1;
            int power = -1 * ((int)XandPow().Item2);
            double grad = (CosX(X * PI / 180) / sinX(X * PI / 180));
            double radian = (CosX(X % PI) / sinX(X % PI));
            if (X==0 || X % 180 == 0)
            {
                label3.Text = "Градусы: бесконечность" + " Радианы: бесконечность";
            }
            else label3.Text = "Градусы: " + Math.Round(grad, power).ToString() + " Радианы: " + Math.Round(radian, power).ToString();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.Hide();
            Labs_VA_2_8.Form1 LB = new Labs_VA_2_8.Form1();
            LB.Show();
        }
    }
}
