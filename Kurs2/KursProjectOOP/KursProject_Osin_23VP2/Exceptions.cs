using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursProject_Osin_23VP2
{
    /// <summary>
    /// Пользовательское исключение, показывающее сообщение об ошибке и выводящее сообщение в MessageBox.
    /// </summary>
    internal class Exceptions : Exception
    {
        /// <summary>
        /// Сообщение, отображаемое пользователю.
        /// </summary>
        public string UserMessage { get; }

        /// <summary>
        /// Конструктор исключения, принимает сообщение для отображения.
        /// </summary>
        /// <param name="userMessage">Сообщение для пользователя.</param>
        public Exceptions(string userMessage) : base("Произошла ошибка: " + userMessage)
        {
            UserMessage = userMessage;
            MessageBox.Show(UserMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Возвращает пользовательское сообщение исключения.
        /// </summary>
        /// <returns>Строка с сообщением пользователя.</returns>
        public override string ToString() => UserMessage;
    }
}