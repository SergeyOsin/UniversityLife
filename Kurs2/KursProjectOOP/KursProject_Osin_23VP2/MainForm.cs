using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KursProject_Osin_23VP2
{
    public partial class MainForm : Form
    {
        private Dealership dealer;
        private int SelectedIndexTable = -1;
        private Random rand;
        private WorkWithTable table;
        private DataBase dB;

        /// <summary>
        /// Основной конструктор формы. Инициализация компонентов и настройка интерфейса.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            rand = new Random();
            table = new WorkWithTable(dataGridView1);
            dB = new DataBase(dataGridView1);
            LockControlsExceptMenuAndButton();
            dataGridView1.CellClick += DataGridView1_CellClick;
            table.InitialTable();
        }

        /// <summary>
        /// Блокирует все элементы управления, кроме меню и кнопки выхода.
        /// </summary>
        private void LockControlsExceptMenuAndButton()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Name == "menuStrip1" || ctrl == button1)
                    ctrl.Enabled = true;
                else
                    ctrl.Enabled = false;
            }
        }

        /// <summary>
        /// Обработчик загрузки формы. Настройка элементов интерфейса и начальных данных.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Настройка границ формы
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Установка названий вкладок и групп
            tabPage1.Text = "Добавление";
            tabPage2.Text = "Обновление";
            groupBox1.Text = "Операции";

            // Заполнение комбобоксов
            string[] array_strCol = { "Марка", "Цена", "Год выпуска", "Производитель", "Цвет", "Пробег" };
            comboBox3.Items.AddRange(array_strCol);
            comboBox3.SelectedIndex = rand.Next(0, array_strCol.Length - 1);

            string[] array_strCol1 = { "По возрастанию", "По убыванию" };
            comboBox4.Items.AddRange(array_strCol1);
            comboBox4.SelectedIndex = rand.Next(0, 1);

            // Настройка привязки элементов
            dataGridView1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button9.Anchor= AnchorStyles.Bottom | AnchorStyles.Left;
            button10.Anchor=AnchorStyles.Bottom | AnchorStyles.Left;
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            groupBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

            // Конфигурация таблицы
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            Array.ForEach(new[] { dataGridView1 }, dt =>
            {
                dt.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dt.AllowUserToAddRows = false;
                dt.RowHeadersVisible = false;
                dt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dt.AllowUserToResizeColumns = false;
                dt.AllowUserToResizeRows = false;
                dt.EnableHeadersVisualStyles = false;
            });

            // Настройки NumericUpDown
            int min_runner = (int)Math.Pow(10, 6);
            int max_runner = (int)Math.Pow(10, 7);
            numericUpDown1.Maximum = max_runner;
            numericUpDown1.Minimum = min_runner;
            numericUpDown4.Maximum = max_runner;
            numericUpDown4.Minimum = min_runner;
            numericUpDown1.Value = rand.Next(min_runner, max_runner);
            numericUpDown2.Maximum = 2025;
            numericUpDown2.Minimum = 2015;
            numericUpDown6.Maximum = 2025;
            numericUpDown6.Minimum = 2015;
            numericUpDown2.Value = rand.Next(2020, 2025);
            numericUpDown3.Maximum = 120000;
            numericUpDown3.Minimum = 10000;
            numericUpDown5.Maximum = 120000;
            numericUpDown5.Minimum = 10000;
            numericUpDown3.Value = rand.Next(10000, 90000);

            //Отключение сортировки строк при нажатии на колонку
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            //Отключение изменений текста
            Array.ForEach(new[] { comboBox1, comboBox2, comboBox3, comboBox4 }, cB => cB.DropDownStyle = ComboBoxStyle.DropDownList);

            // Цвета для ComboBox
            string[] colors = { "Белый", "Черный", "Красный", "Синий", "Зелёный", "Серый" };
            comboBox1.Items.AddRange(colors);
            comboBox2.Items.AddRange(colors);
            comboBox1.SelectedIndex = rand.Next(0, colors.Length);

            // Предварительно заполненные поля
            textBox1.Text = "Toyota Camry";
            textBox2.Text = "Toyota";
        }

        /// <summary>
        /// Обработчик кнопки выхода. Закрытие приложения по подтверждению пользователя.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (message == DialogResult.Yes)
                Application.Exit();
        }

        /// <summary>
        /// Инициализация объекта Dealership на основании текущих данных формы.
        /// </summary>
        private void InitialInput() => dealer = new Dealership(
                textBox1.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value,
                textBox2.Text, comboBox1.SelectedItem.ToString(), (int)numericUpDown3.Value
        );

        /// <summary>
        /// Обработчик добавления новой машины.
        /// Проверяет заполнение полей и добавляет машину в список и таблицу.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                new Exceptions("Поля не могут быть пустыми!");
                return;
            }
            InitialInput();
            Dealership.AddCar(dealer);
            table.AddCartoTable(dealer);
            dB.SaveCarsToDatabase();
        }
        /// <summary>
        /// Обработчик клика по ячейке таблицы. Выбирает строку и заполняет поля формы данными из выбранной строки.
        /// </summary>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                new Exceptions("Выберите строку таблицы!");
                return;
            }
            dataGridView1.Rows[e.RowIndex].Selected = true;
            SelectedIndexTable = e.RowIndex;
            var dSIT = dataGridView1.Rows[SelectedIndexTable];

            // Заполнение полей формы данными выбранной строки
            (textBox1.Text, numericUpDown1.Value, numericUpDown2.Value, textBox2.Text, comboBox2.Text, numericUpDown3.Value) = (
                dSIT.Cells[0].Value.ToString(), (int)dSIT.Cells[1].Value, (int)dSIT.Cells[2].Value, dSIT.Cells[3].Value.ToString(),
                dSIT.Cells[4].Value.ToString(), (int)dSIT.Cells[5].Value);
            (textBox3.Text, numericUpDown4.Value, numericUpDown6.Value, textBox4.Text, comboBox1.Text, numericUpDown5.Value) = (
                textBox1.Text, numericUpDown1.Value, numericUpDown2.Value, textBox2.Text, comboBox1.Text,
                numericUpDown3.Value);
        }

        /// <summary>
        /// Обновление выбранной машины в списке по данным из формы.
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectedIndexTable < 0)
            {
                new Exceptions("Выберите строку!");
                return;
            }
            Dealership updatedDealer = new Dealership(
                textBox3.Text,
               (int)numericUpDown4.Value,
                (int)numericUpDown6.Value,
                textBox4.Text,
                comboBox2.Text,
                (int)numericUpDown5.Value
            );
            Dealership.UpdateCar(SelectedIndexTable, updatedDealer);
            table.UpdateTable();
            dB.SaveCarsToDatabase();
            dataGridView1.Rows[SelectedIndexTable].Selected = true;
        }

        /// <summary>
        /// Удаление выбранной строки из таблицы.
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            if (SelectedIndexTable < 0)
            {
                new Exceptions("Выберите строку");
                return;
            }
            table.RemoveCarFromTable(SelectedIndexTable);
            dB.SaveCarsToDatabase();
        }

        /// <summary>
        /// Сортировка таблицы по выбранному параметру и типу сортировки.
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            string par = comboBox3.SelectedItem.ToString();
            string TypeSort = comboBox4.SelectedItem.ToString();
            table.SortTable(par, TypeSort);
        }

        /// <summary>
        /// Фильтрация таблицы по текстовому полю.
        /// </summary>
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == " ")
            {
                new Exceptions("Поле не может быть пустым!");
                return;
            }
            table.FilterTable(textBox5.Text);
        }

        /// <summary>
        /// Поиск по строке в таблице.
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == " ")
            {
                new Exceptions("Поле не может быть пустым!");
                return;
            }
            table.FindStronTable(textBox7.Text);
        }

        /// <summary>
        /// Отмена сортировки.
        /// </summary>
        private void button10_Click(object sender, EventArgs e) => table.CancelSort();

        /// <summary>
        /// Отмена фильтрации.
        /// </summary>
        private void button9_Click(object sender, EventArgs e) => table.CancelFilter();

        /// <summary>
        /// Отмена поиска.
        /// </summary>
        private void button3_Click(object sender, EventArgs e) => table.CancelFinder();

        /// <summary>
        /// Создание базы данных через диалоговое окно.
        /// </summary>
        private void создатьБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var inputForm = new CreationDataBase())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        table.ClearTable();
                        dB.CreateDatabase(inputForm.DatabaseName);
                        Text = "Автосалон/" + inputForm.DatabaseName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при создании базы данных: " + ex.Message);
                    }
                }
            }
            if (Text!="Автосалон")
                foreach (Control ctrl in Controls)
                    ctrl.Enabled = true;
        }

        /// <summary>
        /// Сохранение базы данных в файл.
        /// </summary>
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "SQL (*.sql)|*.sql",
                Title = "Сохранить базу данных"
            };
            string defaultFileName = $"{dB.currentDatabase}.sql";
            saveDialog.FileName = defaultFileName;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveDialog.FileName.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                {
                    saveDialog.FileName += ".sql";
                }
                dB.SaveDatabase(saveDialog.FileName);
            }
        }

        /// <summary>
        /// Открытие базы данных из файла.
        /// </summary>
        private void открытьБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "SQL файлы (*.sql)|*.sql|PDF файлы (*.pdf)|*.pdf",
                Title = "Открыть файл базы данных",
                Multiselect = false
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                table.ClearTable();
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    bool success = dB.OpenSqlFile(openDialog.FileName);
                    if (success)
                    {
                        if (Path.GetExtension(openDialog.FileName).ToLower() == ".sql")
                        {
                            Invoke(new Action(() =>
                            {
                                this.Text = "Автосалон/" + Path.GetFileNameWithoutExtension(openDialog.FileName);
                            }));
                        }
                        foreach (Control ctrl in Controls)
                            ctrl.Enabled = true;
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Удаление текущей базы данных по подтверждению пользователя.
        /// </summary>
        private void удалитьБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dB.currentDatabase))
            {
                MessageBox.Show("Нет подключённой базы данных для удаления",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Вы действительно хотите удалить базу данных '{dB.currentDatabase}'?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                LockControlsExceptMenuAndButton();
                Dealership.ClearAllCars();
                string dbName = dB.currentDatabase;
                if (dB.DeleteDatabase(dbName))
                {
                    table.ClearTable();
                    Text = "Автосалон";
                    dB.currentDatabase = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении базы данных: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Создание отчета в PDF файле.
        /// </summary>
        private void создатьОтчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "PDF файлы (*.pdf)|*.pdf",
                Title = "Сохранить отчет",
                FileName = "Отчёт.pdf"
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
                dB.SaveCarsTableToPdf(saveDialog.FileName); 
        }
    }
}