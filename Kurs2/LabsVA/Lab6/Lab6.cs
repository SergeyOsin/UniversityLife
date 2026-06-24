using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace VA6
{
    public partial class MainForm : Form
    {
        private const double LEFT = 2.0;
        private const double RIGHT = 5.0;
        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridViews();
            tabPage1.Text = "Интерполяция";
            tabPage2.Text = "Кубический Сплайн";
            Array.ForEach(new[] { dgvCubicSpline, dgvLagrange }, tb => {
                tb.ColumnHeadersVisible = false;
                tb.RowHeadersVisible = false;
                
            });
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeDataGridViews()
        {
            dgvLagrange.ColumnHeadersVisible = false;
            dgvLagrange.Columns.Add("var", "");
            dgvLagrange.Rows.Add("x");
            dgvLagrange.Rows.Add("y(x)");

            dgvCubicSpline.Columns.Add("var", "");
            dgvCubicSpline.Rows.Add("x");
            dgvCubicSpline.Rows.Add("y(x)");
            dgvCubicSpline.Columns[0].ReadOnly = true;
        }

        #region Lagrange Interpolation Methods
        private static double LagrangeFunction(double x)
        {
            return Math.Pow(Math.E, -(x + Math.Sin(x)));
        }

        private (List<double>, List<double>) GetLagrangeValues(int n)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            // Генерируем n+1 равномерно распределенных точек на интервале [LEFT, RIGHT]
            for (int i = 0; i <= n; i++)
            {
                double x = LEFT + i * (RIGHT - LEFT) / n;
                xValues.Add(x);
                yValues.Add(LagrangeFunction(x));
            }

            return (xValues, yValues);
        }

        private (List<double>, List<double>) InterpolateLagrange(int partitions, List<double> points, List<double> values)
        {
            List<double> x_values = new List<double>();
            List<double> y_values = new List<double>();

            // Для каждого подынтервала между точками делаем partitions разбиений
            for (int i = 0; i < points.Count - 1; i++)
            {
                double a = points[i], b = points[i + 1];
                double step = (b - a) / partitions;

                for (int j = 0; j <= partitions; j++)
                {
                    double xj = a + j * step;
                    x_values.Add(xj);
                    y_values.Add(InterpolateAtPoint(xj, points, values));
                }
            }

            return (x_values, y_values);
        }

        private double InterpolateAtPoint(double x, List<double> points, List<double> values)
        {
            double result = 0;
            for (int j = 0; j < values.Count; j++)
            {
                double term = values[j];
                for (int k = 0; k < points.Count; k++)
                {
                    if (k != j)
                    {
                        term *= (x - points[k]) / (points[j] - points[k]);
                    }
                }
                result += term;
            }
            return result;
        }
        private (double, double, double) CalculateDeviation(double x, List<double> points, List<double> values)
        {
            double function_value = LagrangeFunction(x);
            double interpolate_value = InterpolateAtPoint(x, points, values);
            double deviation = Math.Abs(function_value - interpolate_value);
            return (function_value, interpolate_value, deviation);
        }
        #endregion

        #region Cubic Spline Methods
        private static double Spline(double a, double b, double c, double d, double x, double x0)
        {
            return a + b * (x - x0) + c * (x - x0) * (x - x0) + d * (x - x0) * (x - x0) * (x - x0);
        }

        private List<double> CalculateSteps(List<double> points)
        {
            List<double> steps = new List<double>();
            for (int i = 1; i < points.Count; i++)
            {
                steps.Add(points[i] - points[i - 1]);
            }
            return steps;
        }

        private List<double> SplitInterval(double a, double b, int partitions)
        {
            List<double> result = new List<double> { a };
            double step = (b - a) / partitions;
            for (int i = 1; i <= partitions; i++)
            {
                result.Add(a + i * step);
            }
            return result;
        }

        private double[,] CreateSplineMatrix(List<double> points)
        {
            double[,] matrix = new double[points.Count, points.Count];
            List<double> steps = CalculateSteps(points);

            matrix[0, 0] = 1;
            matrix[points.Count - 1, points.Count - 1] = 1;

            for (int i = 1; i < points.Count - 1; i++)
            {
                matrix[i, i] = 2 * (steps[i - 1] + steps[i]);
                matrix[i, i - 1] = steps[i - 1];
                matrix[i, i + 1] = steps[i];
            }
            return matrix;
        }

        private double[] SolveSplineSystem(double[,] matrix, List<double> points, List<double> values)
        {
            double[] D = new double[matrix.GetLength(0)];
            D[0] = 0;
            D[D.Length - 1] = 0;

            List<double> steps = CalculateSteps(points);
            for (int i = 1; i < D.Length - 1; i++)
            {
                D[i] = 3 * ((values[i + 1] - values[i]) / steps[i] - (values[i] - values[i - 1]) / steps[i - 1]);
            }

            // Прямой ход метода прогонки
            double[] P = new double[matrix.GetLength(0)];
            double[] Q = new double[matrix.GetLength(0)];

            P[0] = -matrix[0, 1] / matrix[0, 0];
            Q[0] = D[0] / matrix[0, 0];

            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
            {
                double denominator = matrix[i, i] + matrix[i, i - 1] * P[i - 1];
                P[i] = -matrix[i, i + 1] / denominator;
                Q[i] = (D[i] - matrix[i, i - 1] * Q[i - 1]) / denominator;
            }

            // Обратный ход
            double[] X = new double[matrix.GetLength(0)];
            X[X.Length - 1] = (D[D.Length - 1] - matrix[matrix.GetLength(0) - 1, matrix.GetLength(0) - 2] * Q[Q.Length - 2]) /
                             (matrix[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1] + matrix[matrix.GetLength(0) - 1, matrix.GetLength(0) - 2] * P[P.Length - 2]);

            for (int i = X.Length - 2; i >= 0; i--)
            {
                X[i] = P[i] * X[i + 1] + Q[i];
            }

            return X;
        }

        private (List<double>, List<double>) GetSplineValues(List<double> points, List<double> values, int partitions)
        {
            double[,] matrix = CreateSplineMatrix(points);
            double[] C = SolveSplineSystem(matrix, points, values);

            int numSegments = points.Count - 1;
            List<double> h = CalculateSteps(points);

            double[] A = new double[numSegments];
            double[] B = new double[numSegments];
            double[] D = new double[numSegments];

            for (int i = 0; i < numSegments; i++)
            {
                A[i] = values[i];
                B[i] = (values[i + 1] - values[i]) / h[i] - h[i] * (C[i + 1] + 2 * C[i]) / 3.0;
                D[i] = (C[i + 1] - C[i]) / (3.0 * h[i]);
            }

            List<double> splinePoints = new List<double>();
            List<double> splineValues = new List<double>();

            for (int i = 0; i < numSegments; i++)
            {
                List<double> segmentPoints = SplitInterval(points[i], points[i + 1], partitions);
                foreach (double x in segmentPoints)
                {
                    splinePoints.Add(x);
                    splineValues.Add(Spline(A[i], B[i], C[i], D[i], x, points[i]));
                }
            }

            return (splinePoints, splineValues);
        }
        #endregion

        #region Event Handlers
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCreateEmpty_Click(object sender, EventArgs e)
        {
            while (dgvCubicSpline.Columns.Count > 1)
            {
                dgvCubicSpline.Columns.RemoveAt(1);
            }
            dgvCubicSpline.ColumnCount = (int)nudCount.Value + 1;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            List<double> points = new List<double>();
            List<double> values = new List<double>();

            // Парсинг данных из таблицы
            for (int i = 1; i < dgvCubicSpline.Columns.Count; i++)
            {
                if (!double.TryParse(dgvCubicSpline.Rows[0].Cells[i].Value?.ToString(), out double point))
                {
                    MessageBox.Show($"Неверное значение в строке 1 столбца {i}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(dgvCubicSpline.Rows[1].Cells[i].Value?.ToString(), out double value))
                {
                    MessageBox.Show($"Неверное значение в строке 2 столбца {i}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                points.Add(point);
                values.Add(value);
            }

            // Проверка данных
            if (points.Count < 2)
            {
                MessageBox.Show("Для построения сплайна нужно минимум 2 точки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сортировка точек
            var combined = points.Zip(values, (p, v) => new { Point = p, Value = v })
                               .OrderBy(x => x.Point)
                               .ToList();

            points = combined.Select(x => x.Point).ToList();
            values = combined.Select(x => x.Value).ToList();

            // Очистка графика
            chartCubicSpline.Series.Clear();
            chartCubicSpline.ChartAreas.Clear();

            // Настройка графика
            ChartArea area = new ChartArea();
            area.AxisX.LabelStyle.Format = "N4";
            area.AxisY.LabelStyle.Format = "N4";
            chartCubicSpline.ChartAreas.Add(area);
            Series splineSeries = new Series
            {
                Name = "Кубический сплайн",
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2
            };

            chartCubicSpline.Series.Add(splineSeries);

            // Построение сплайна
            var splinePoints = GetSplineValues(points, values, (int)nudCubicPartitions.Value);
            for (int i = 0; i < splinePoints.Item1.Count; i++)
            {
                splineSeries.Points.AddXY(splinePoints.Item1[i], splinePoints.Item2[i]);
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var functionValues = GetLagrangeValues((int)nudPartitions.Value);
                List<double> points = functionValues.Item1;
                List<double> values = functionValues.Item2;

                double x = Convert.ToDouble(textBox1.Text);
                if (x < 2 || x > 5)
                    throw new Exception("Введите число от 2 до 5!");
                var deviation = CalculateDeviation(x, points, values);
                tbFunctionValue.Text = deviation.Item1.ToString("F6");
                tbPolinomValue.Text = deviation.Item2.ToString("F6");
                tbOtkl.Text = deviation.Item3.ToString("F6");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) => Application.Exit();

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvLagrange.AllowUserToAddRows = false;
            dgvCubicSpline.AllowUserToAddRows = false;
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            new Labs_VA_2_8.Form1().Show();
            this.Hide();
        }

        private void btnDrawLagrange_Click(object sender, EventArgs e)
        {
            chartLagrange.Series.Clear();
            chartLagrange.ChartAreas.Clear();
            while (dgvLagrange.Columns.Count > 1)
                dgvLagrange.Columns.RemoveAt(1);

            var functionValues = GetLagrangeValues((int)nudPartitions.Value);
            List<double> points = functionValues.Item1;
            List<double> values = functionValues.Item2;
            for (int i = 0; i < points.Count; i++)
            {
                dgvLagrange.Columns.Add($"x{i}", "");
                dgvLagrange.Rows[0].Cells[i + 1].Value = points[i].ToString("F3");
                dgvLagrange.Rows[1].Cells[i + 1].Value = values[i].ToString("F3");
            }

            var interpolatePoints = InterpolateLagrange((int)nudPartitions.Value, points, values);
            ChartArea area = new ChartArea();
            area.AxisX.LabelStyle.Format = "N4";
            area.AxisY.LabelStyle.Format = "N4";
            chartLagrange.ChartAreas.Add(area);

            Series functionSeries = new Series
            {
                Name = "y(x) = e^-(x + sin(x))",
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 4,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6,
                MarkerColor = Color.Red
            };

            Series interpolateSeries = new Series
            {
                Name = "L(x)",
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 3,
                MarkerSize = 6,
                MarkerColor = Color.Blue
            };

            chartLagrange.Series.Add(functionSeries);
            chartLagrange.Series.Add(interpolateSeries);

            // Добавление точек на график
            for (int i = 0; i < points.Count; i++)
            {
                functionSeries.Points.AddXY(points[i], values[i]);
            }

            for (int i = 0; i < interpolatePoints.Item1.Count; i++)
            {
                interpolateSeries.Points.AddXY(interpolatePoints.Item1[i], interpolatePoints.Item2[i]);
            }
        }
    }
}