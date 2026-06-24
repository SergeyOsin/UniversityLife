using System.Collections;
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
    public partial class Form1 : Form
    {
        const int size = 10000000;
        int[] unsortarray = new int[size+1];
        int[] sortedarray = new int[size + 1];
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        Random rd = new Random();
        private void button1_Click(object sender, EventArgs e)
        {

            decimal key0 = numericUpDown1.Value;
            unsortarray[size] = Convert.ToInt32(key0);
            int index0 = -1;
            int StartTime = Environment.TickCount;
            {
                for(int i = 0; i < size; i++)
                {
                    if (unsortarray[i] == key0)
                    {
                        index0 = i;
                        break;
                    }
                }
            }
            textBox1.Text = (Environment.TickCount - StartTime).ToString();
            textBox2.Text = index0.ToString();
            int index1;
            int StartTime0 = Environment.TickCount;
            {
                for (index1 = 0; unsortarray[index1] != key0; index1++) ;
                if (index1 == size)
                    index1 = -1;
            }
            textBox5.Text = (Environment.TickCount - StartTime0).ToString();
            textBox6.Text = index1.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal key1 = numericUpDown2.Value;
            sortedarray[size] = Convert.ToInt32(key1);
            int index2;
            int StartTime2 = Environment.TickCount;
            {
                for (index2 = 0; sortedarray[index2] != key1; index2++) ;
                if (index2 == size)
                    index2 = -1;
            }            
            textBox3.Text = (Environment.TickCount - StartTime2).ToString();
            textBox4.Text = index2.ToString();
            ++sortedarray[size];
            int index3 = -1;
            int i ;
            int StartTime3 = Environment.TickCount;
            {
                for (i = 0; key1 > sortedarray[i]; i++) ;
                index3 = (key1 == sortedarray[i]) ? i : -1;
            }
            textBox7.Text = (Environment.TickCount - StartTime3).ToString();
            textBox8.Text = index3.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < size; i++) unsortarray[i] = rd.Next(0, size);
            sortedarray[0] = rd.Next(0, 3);
            for (int i = 1; i < size; i++) sortedarray[i] =sortedarray[i-1]+ rd.Next(1, 3);
        }
    }
}
