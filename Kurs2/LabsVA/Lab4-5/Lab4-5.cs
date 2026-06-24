using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LB4_VA
{
    public partial class Form1 : Form
    {
        private int order;
        private double left;
        private double right;
        private double[,] matrix;
        private Random rand = new Random();
        private const char X = 'x';

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) => e.Control.KeyPress += NumericTextBox_KeyPress;

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != ',')
                e.Handled = true;
            if (e.KeyChar == '-' && ((sender as TextBox)?.SelectionStart != 0))
                e.Handled = true;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.Value != null && double.TryParse(cell.Value.ToString(), out double value))
                        matrix[e.RowIndex, e.ColumnIndex] = value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void InitializeMatrix()=> matrix = new double[order, order + 1];

        private void button1_Click(object sender, EventArgs e) => Application.Exit();

        private void Initialnumer()
        {
            order = Convert.ToInt32(numericUpDown1.Value);
            left = Convert.ToDouble(numericUpDown2.Value);
            right = Convert.ToDouble(numericUpDown3.Value);
            InitializeMatrix();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            new DataGridView[] { dataGridView1, dataGridView2,dataGridView3 }.ToList().ForEach(data =>
            {
                data.AllowUserToAddRows = false;
                data.RowHeadersVisible = false;
            });

            numericUpDown1.Value = 3;
            numericUpDown1.Maximum = 9;
            numericUpDown1.Minimum = 2;
            label10.Text = "Итерации";
            numericUpDown2.Minimum = -1000;
            numericUpDown2.Maximum = 10;
            numericUpDown3.Minimum = 11;
            numericUpDown3.Maximum = 1000;
            numericUpDown3.Value = 15;
            numericUpDown2.Value = -15;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                new Control[] { numericUpDown2, numericUpDown3, label3, label4, label5 }.ToList().ForEach(c => c.Show());
            }
            else
            {
                new Control[] { numericUpDown2, numericUpDown3, label3, label4, label5 }.ToList().ForEach(c => c.Hide());
                InitializeMatrix();
                UpdateDataGridViewFromMatrix();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
            UpdateDataGridViewFromMatrix();
        }

        private void ToDoMatrix()
        {
            matrix[0, 0] = rand.Next((int)left, (int)right);
            matrix[0, 1] = rand.Next((int)left, (int)right);
            int i;
            for (i = 1; i < order - 1; i++)
            {
                matrix[i, i] = rand.Next((int)left, (int)right);
                matrix[i, i - 1] = rand.Next((int)left, (int)right);
                matrix[i, i + 1] = rand.Next((int)left, (int)right);
            }
            matrix[order - 1, order - 1] = rand.Next((int)left, (int)right);
            matrix[order - 1, order - 2] = rand.Next((int)left, (int)right);
            for (int j = 0; j < order; j++)
                matrix[j, order] = rand.Next((int)left, (int)right);
            UpdateDataGridViewFromMatrix();
        }

        private void BuildTable()
        {
            dataGridView1.Columns.Clear();
            for (int i = 0; i < order; i++)
            {
                dataGridView1.Columns.Add(X + (i + 1).ToString(), X + (i + 1).ToString());
            }
            dataGridView1.Columns.Add(X + "b", X + "b");
            dataGridView1.RowCount = order;
            UpdateDataGridViewFromMatrix();
        }

        private void UpdateDataGridViewFromMatrix()
        {
            for (int i = 0; i < order; i++)
                for (int j = 0; j <= order; j++)
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Initialnumer();
            if (checkBox1.Checked)
                ToDoMatrix();
            else
            {
                for (int i = 0; i < order; i++)
                    for (int j = 0; j <= order; j++)
                        matrix[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
            }
            Method_Runned();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Initialnumer();
            BuildTable();
        }

        private void ClearTable()
        {
            new DataGridView[] {dataGridView2, dataGridView3 }.ToList().ForEach(data =>
            {
                data.Rows.Clear();
                data.Columns.Clear();
            });
            iterationTabs.TabPages.Clear();
        }

        private void AddIterationTab(int iteration, List<double> P, List<double> Q, string description)
        {
            TabPage tabPage = new TabPage($"Итерация {iteration}");
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            tabPage.Controls.Add(dgv);
            iterationTabs.TabPages.Add(tabPage);
            dgv.Columns.Add("Index", "i");
            dgv.Columns.Add("P", "P[i]");
            dgv.Columns.Add("Q", "Q[i]");
            dgv.Columns.Add("Description", "Описание");
            int maxLength = Math.Max(P.Count, Q.Count);
            for (int i = 0; i < maxLength; i++)
            {
                dgv.Rows.Add(new object[] {
            i,
            i < P.Count ? Math.Round(P[i], 4).ToString() : "-",
            i < Q.Count ? Math.Round(Q[i], 4).ToString() : "-",
            i == maxLength - 1 ? description : ""
        });
            }
            foreach (DataGridViewColumn column in dgv.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private bool CheckMatrix()
        {
            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                    if (Math.Abs(i - j) > 1 && matrix[i, j] != 0)
                        return false;
            return true;
        }
        private void Method_Runned()
        {
            ClearTable();
            if (!CheckMatrix())
            {
                MessageBox.Show("Матрица не трёхдиагональная!");
                return;
            }
            List<double> P = new List<double>();
            List<double> Q = new List<double>();
            if (matrix[0, 0] == 0)
            {
                if (matrix[0, order] == 0)
                {
                    MessageBox.Show("Система имеет бесконечно много решений");
                    return;
                }
                else
                {
                    MessageBox.Show("Система не имеет решений");
                    return;
                }
            }
            P.Add(-matrix[0, 1] / matrix[0, 0]);
            Q.Add(matrix[0, order] / matrix[0, 0]);
            AddIterationTab(1, P, Q, "Инициализация P[0] и Q[0]");
            for (int i = 1; i < order - 1; i++)
            {
                double denominator = matrix[i, i] + matrix[i, i - 1] * P[i - 1];
                if (denominator == 0)
                {
                    if (matrix[i, order] - matrix[i, i - 1] * Q[i - 1] == 0)
                    {
                        MessageBox.Show($"Система имеет бесконечно много решений");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"Система не имеет решений");
                        return;
                    }
                }
                P.Add(-matrix[i, i + 1] / denominator);
                Q.Add((matrix[i, order] - matrix[i, i - 1] * Q[i - 1]) / denominator);
                AddIterationTab(i + 1, P, Q, $"Вычисление P[{i}] и Q[{i}]");
            }
            int last = order - 1;
            double lastDenominator = matrix[last, last] + matrix[last, last - 1] * P[last - 1];
            Q.Add((matrix[last, order] - matrix[last, last - 1] * Q[last - 1]) / lastDenominator);
            AddIterationTab(order, P, Q, "Финальные P и Q");
            List<double> X = new List<double>();
            X.Add(Q[last]);
            for (int j = last - 1; j >= 0; j--)
                X.Add(P[j] * X[last - j - 1] + Q[j]);
            X.Reverse();
            for (int k = 0; k < order; k++)
                dataGridView2.Columns.Add($"x{k + 1}", $"x{k + 1}");
            dataGridView2.Rows.Add();
            for (int i = 0; i < order; i++)
                dataGridView2.Rows[0].Cells[i].Value = Math.Round(X[i], 4);
            for(int k = 0; k < order; k++)
                dataGridView3.Columns.Add($"{k + 1} строка", $"{k + 1} строка");
            dataGridView3.Rows.Add();
            double result = 0.0;
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                    result += matrix[i, j] * X[j];
                dataGridView3.Rows[0].Cells[i].Value = result;
                result = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Labs_VA_2_8.Form1().Show();
        }
    }
}
