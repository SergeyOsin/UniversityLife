using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LB8_VA
{
    public partial class LB8 : Form
    {
        private int SIze;
        private Solution sol;
        public LB8() => InitializeComponent();

        private void button3_Click(object sender, EventArgs e)=> Application.Exit();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button1.BackColor = Color.Indigo;
            button1.ForeColor = Color.Teal;
            Array.ForEach(new[] { dataGridView1 }, dt => dt.ReadOnly = true);
            Array.ForEach(new[] { dataGridView1 }, dt => dt.AllowUserToAddRows = false);
            Array.ForEach(new[] { dataGridView1  }, dt => dt.RowHeadersVisible = false);
            Array.ForEach(new[] {dataGridView1 }, dt => dt.ColumnHeadersVisible = false);
            dataGridView1.RowCount = 3;
            Array.ForEach(new[] { dataGridView1 }, dt => dt[0, 0].Value = "i");
            dataGridView1.Rows[1].Cells[0].Value = "x(i)";
            dataGridView1.Rows[2].Cells[0].Value = "y(i)";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            chart1.Titles.Add("График функции");
        }
        private double RoundNum(double num) => Math.Round(num, 4);
        private void Solve(DataGridView gr)
        {
            double Up = Convert.ToDouble(textBox1.Text);
            double Down = Convert.ToDouble(textBox3.Text);
            if (Down>Up) throw new Exception("Не может левая граница быть больше правой!");
            double step = Convert.ToDouble(textBox2.Text);
            SIze = Convert.ToInt32((Up - Down) / step);
            sol = new Solution(Up,Down,step,SIze);
            dataGridView1.ColumnCount = SIze + 1;
            double[] arrayX = new double[SIze];
            double[] arrayY = new double[SIze];
            (arrayX, arrayY) = sol.Solve_Eylor();
            for(int k = 0; k < SIze; k++)
            {
                gr[k + 1, 0].Value = k;
                gr[k + 1, 1].Value = RoundNum(arrayX[k]);
                gr[k + 1, 2].Value = RoundNum(arrayY[k]);
            }
        }

        private void BuildGraph()
        {
            chart1.ChartAreas[0].AxisX.Title = "X"; 
            chart1.ChartAreas[0].AxisY.Title = "Y"; 
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            Series series = new Series("График значений y(x)");
            series.ChartType = SeriesChartType.Line; 
            series.Color = Color.Blue; 
            series.BorderWidth = 2;
            series.Points.AddXY(2, -1);
            for (int i = 1; i < SIze; i++)
                series.Points.AddXY(dataGridView1[i, 1].Value, dataGridView1[i, 2].Value);
            chart1.Series.Clear();
            chart1.Series.Add(series);
        }
        private void button1_Click(object sender, EventArgs e) => Solve(dataGridView1);
        private void button2_Click(object sender, EventArgs e) => BuildGraph();

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Labs_VA_2_8.Form1().Show();
        }
    }
}
