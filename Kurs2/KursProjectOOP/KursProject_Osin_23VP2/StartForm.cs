using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursProject_Osin_23VP2
{
    /// <summary>
    /// Начальная форма приложения, отображается при запуске.
    /// Автоматически закрывается через заданное время или по нажатию кнопки.
    /// </summary>
    public partial class StartForm : Form
    {
        /// <summary>
        /// Константа интервала таймера в миллисекундах.
        /// </summary>
        const int SECONDS = 10000;

        /// <summary>
        /// Основная форма приложения, которая отображается после стартовой.
        /// </summary>
        MainForm form1 = new MainForm();

        /// <summary>
        /// Таймер для автоматического закрытия стартовой формы.
        /// </summary>
        Timer _timer = new Timer();

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public StartForm()
        {
            InitializeComponent();
            form1.Show(); // Показываем основную форму при запуске стартовой.
        }

        /// <summary>
        /// Метод для скрытия стартовой формы.
        /// </summary>
        private void CloseStartForm() => this.Hide();

        /// <summary>
        /// Обработчик загрузки формы.
        /// Настраивает таймер и запускает его.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void StartForm_Load(object sender, EventArgs e)
        {
            _timer.Interval = SECONDS; 
            _timer.Tick += (s, args) => CloseStartForm(); 
            _timer.Start(); 
        }

        /// <summary>
        /// Обработчик нажатия кнопки для пропуска автозакрытия.
        /// Останавливает таймер и скрывает стартовую форму.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            _timer.Stop(); // Останавливаем таймер.
            this.Hide();   // Скрываем стартовую форму.
        }
    }
}