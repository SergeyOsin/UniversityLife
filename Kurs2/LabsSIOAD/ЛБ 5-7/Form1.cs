using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Lab5
{

    public partial class Form1 : Form
    {
        private const int SIZE = 500000000;
        private const int C = 1000000;
        private const int Z = 100;
        private int[] unsortedarray = new int[SIZE + 1];
        private Random rand = new Random();
        private int key;
        private long index0;
        private void Form1_Load(object sender, EventArgs e)
        {
            unsortedarray[0] = rand.Next(0, 4);
            for (int i = 1; i < SIZE; i++) unsortedarray[i] = unsortedarray[i - 1] + rand.Next(1, 3);
        }
        private void InitialKey() => key = Convert.ToInt32(numericUpDown1.Value);
        public Form1() => InitializeComponent();

        private void button2_Click(object sender, EventArgs e) => Application.Exit();
        private void InOptimalBP()
        {
            index0 = -1;
            int StartTime = Environment.TickCount;
            {
                for (int i = 0; i < C; i++)
                {
                    int left = 0, right = SIZE - 1;
                    while (left <= right)
                    {
                        int middle = (left + right) / 2;
                        if (unsortedarray[middle] == key)
                        {
                            index0 = middle;
                            break;
                        }
                        else if (key > unsortedarray[middle]) left = middle + 1;
                        else right = middle - 1;
                    }
                }
            }
            textBox1.Text = (Environment.TickCount - StartTime).ToString();
            textBox2.Text = index0.ToString();
        }
        private void OptimalBP()
        {
            index0 = -1;
            int StartTime2 = Environment.TickCount;
            {
                for (int i = 0; i < C; i++)
                {
                    int left0 = 0, right0 = SIZE - 1;
                    while (left0 < right0)
                    {
                        int middle = (left0 + right0) / 2;
                        if (key <= unsortedarray[middle]) right0 = middle;
                        else left0 = middle + 1;
                    }
                    if (unsortedarray[right0] == key) index0 = right0;
                }
            }
            textBox4.Text = (Environment.TickCount - StartTime2).ToString();
            textBox5.Text = index0.ToString();
        }
        private void InterpBP()
        {
            index0 = -1;
            int StartTime3 = Environment.TickCount;
            for (int j = 0; j < C; j++)
            {
                long left = 0, right = SIZE - 1;
                long I = 0;
                while (key > unsortedarray[left] && key < unsortedarray[right])
                {
                    I = left + (key - unsortedarray[left]) * (right - left) / (unsortedarray[right] - unsortedarray[left]);
                    if (unsortedarray[I] == key) break;
                    else if (key > unsortedarray[I]) left = I + 1;
                    else right = I - 1;
                }
                if (unsortedarray[left] == key) I = left;
                else if (unsortedarray[right] == key) I = right;
                index0 = (unsortedarray[I] == key) ? I : -1;
            }
            textBox6.Text = (Environment.TickCount - StartTime3).ToString();
            textBox7.Text = index0.ToString();
        }
        private void StepBP()
        {
            index0 = -1;
            int StartTime4 = Environment.TickCount;
            for (int i = 0; i < C; i++)
            {
                int start = 0;
                int jump = SIZE / 2;
                while (jump > 0)
                {
                    while (start + jump < SIZE && unsortedarray[start + jump] <= key)
                        start += jump;
                    jump /= 2;
                }
                index0 = (unsortedarray[start] == key) ? start : -1;
            }
            textBox8.Text = (Environment.TickCount - StartTime4).ToString();
            textBox9.Text = index0.ToString();
        }

        private void OptimalLineFind()
        {
            index0 = -1;
            unsortedarray[SIZE] = key + 1;
            int StartTime5 = Environment.TickCount;
            {
                int q;
                for (int i = 0; i < Z; i++)
                {
                    for (q = 0; key > unsortedarray[q]; q++) ;
                    index0 = (unsortedarray[q] == key) ? q : -1;
                }
            }
            textBox10.Text = ((Environment.TickCount - StartTime5) * C / Z).ToString();
            textBox11.Text = index0.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InitialKey();
            InOptimalBP();
            OptimalBP();
            InterpBP();
            StepBP();
            OptimalLineFind();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

