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
    /// Форма для создания базы данных, позволяет пользователю ввести название.
    /// </summary>
    public partial class CreationDataBase : Form
    {
        /// <summary>
        /// Имя базы данных, выбранное пользователем.
        /// </summary>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public CreationDataBase() => InitializeComponent();

        /// <summary>
        /// Обработчик загрузки формы. Можно использовать для инициализации.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void CreationDataBase_Load(object sender, EventArgs e) { 
            
        }

        /// <summary>
        /// Обработчик нажатия кнопки для подтверждения (обычно "ОК" или "Создать").
        /// Проверяет, чтобы было введено имя базы данных, сохраняет его и закрывает форму.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()))
            {
                MessageBox.Show("Введите имя базы данных.");
                return;
            }
            DatabaseName = textBox1.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки отмены (обычно "Отмена").
        /// Закрывает форму без сохранения данных.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Данные события.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}