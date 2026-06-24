
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 15000;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}%";
            chart1.ChartAreas[0].AxisY.Maximum = 1000;
        }
        private bool isSorted(List<int>list)
        {
            for (int i = 1; i < list.Count(); i++)
                if (list[i] < list[i - 1])
                    return false;
            return true;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            int SIZE_1 = Convert.ToInt32(numericUpDown1.Value);
            int[] array = new int[SIZE_1];
            Random rd = new Random();
            for (int i = 0; i < SIZE_1; i++) array[i] = rd.Next(0, SIZE_1);
            chart1.Series["Рекурсия"].Points.Clear();
            BTSrec(array);
            BTSiter(array);
        }
        private void BTSrec(int[]arr)
        {
            chart1.Series["Рекурсия"].Points.AddXY(0, 0);
            for (int proz = 10; proz <= 100; proz += 10)
            {
                int StartTime = Environment.TickCount;
                BinarySearchTree BST = new BinarySearchTree(arr[0]);
                List<int> sortedList = BST.SortArrayUsingBST(arr);
                int EndTime = (Environment.TickCount - StartTime);
                textBox1.Text = (isSorted(sortedList)) ? "Да" : "Нет";
                chart1.Series["Рекурсия"].Points.AddXY(proz, EndTime);
            }
        }
        private void BTSiter(int[] arr)
        {
            chart1.Series["Итерация"].Points.AddXY(0, 0);
            for(int pro = 10; pro <= 100; pro += 10)
            {
                Queue<int> que = new Queue<int>();
            }
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
