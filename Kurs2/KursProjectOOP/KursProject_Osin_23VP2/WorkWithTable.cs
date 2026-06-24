using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursProject_Osin_23VP2
{
    /// <summary>
    /// Класс для работы с таблицей DataGridView и списком автомобилей.
    /// Включает методы для инициализации, добавления, обновления,удаления, сортировки, фильтрации и поиска по определённому критерию.
    /// </summary>
    internal class WorkWithTable
    {
        private DataGridView datagridview1;
        private List<Dealership> default_List;

        /// <summary>
        /// Конструктор класса, инициализирующий DataGridView и сохраняющий исходный список автомобилей.
        /// </summary>
        /// <param name="_datagridview1">Объект DataGridView для отображения данных.</param>
        public WorkWithTable(DataGridView _datagridview1)
        {
            datagridview1 = _datagridview1;
            default_List = Dealership.Cars;
        }

        /// <summary>
        /// Инициализация таблицы, добавление колонок.
        /// </summary>
        public void InitialTable()
        {
            datagridview1.Columns.Add("Brand", "Марка");
            datagridview1.Columns.Add("Cost", "Цена");
            datagridview1.Columns.Add("Year", "Год выпуска");
            datagridview1.Columns.Add("Maker", "Производитель");
            datagridview1.Columns.Add("Color", "Цвет");
            datagridview1.Columns.Add("Mileage", "Пробег");
        }

        /// <summary>
        /// Добавляет объект автомобиля в таблицу.
        /// </summary>
        /// <param name="car">Объект Dealership (автомобиль).</param>
        public void AddCartoTable(Dealership car)
        {
            int rowNumber = datagridview1.Rows.Add();
            int colIndex = 0;
            datagridview1.Rows[rowNumber].Cells[colIndex++].Value = car.BrandCar;
            datagridview1.Rows[rowNumber].Cells[colIndex++].Value = car.Cost;
            datagridview1.Rows[rowNumber].Cells[colIndex++].Value = car.ReleaseYear;
            datagridview1.Rows[rowNumber].Cells[colIndex++].Value = car.Maker;
            datagridview1.Rows[rowNumber].Cells[colIndex++].Value = car.Color;
            datagridview1.Rows[rowNumber].Cells[colIndex++].Value = car.Mileage;
        }

        /// <summary>
        /// Обновляет таблицу, заполняя её данными из текущего списка автомобилей.
        /// </summary>
        public void UpdateTable()
        {
            datagridview1.Rows.Clear();
            foreach (var car in Dealership.Cars)
                AddCartoTable(car);
        }

        /// <summary>
        /// Удаляет автомобиль из таблицы и списка по индексу.
        /// </summary>
        /// <param name="index">Индекс строки для удаления.</param>
        public void RemoveCarFromTable(int index)
        {
            if (index >= 0 && index < datagridview1.Rows.Count)
            {
                datagridview1.Rows.RemoveAt(index);
                Dealership.RemoveCar(index);
            }
        }
        /// <summary>
        /// Очищает таблицу и список автомобилей в коллекции
        /// </summary>
        public void ClearTable()
        {
            datagridview1.Rows.Clear();
            Dealership.ClearAllCars();
        }

        /// <summary>
        /// Сортирует список автомобилей по выбранному параметру и типу сортировки.
        /// </summary>
        /// <param name="parametr">Параметр сортировки (например, "марка").</param>
        /// <param name="typeSort">Тип сортировки ("По убыванию" или что-то другое).</param>
        public void SortTable(string parametr, string typeSort)
        {
            switch (parametr.ToLower())
            {
                case "марка":
                    Dealership.Cars = typeSort == "По убыванию"
                        ? Dealership.Cars.OrderByDescending(car => car.BrandCar).ToList()
                        : Dealership.Cars.OrderBy(car => car.BrandCar).ToList();
                    break;
                case "цена":
                    Dealership.Cars = typeSort == "По убыванию"
                        ? Dealership.Cars.OrderByDescending(car => car.Cost).ToList()
                        : Dealership.Cars.OrderBy(car => car.Cost).ToList();
                    break;
                case "год выпуска":
                    Dealership.Cars = typeSort == "По убыванию"
                        ? Dealership.Cars.OrderByDescending(car => car.ReleaseYear).ToList()
                        : Dealership.Cars.OrderBy(car => car.ReleaseYear).ToList();
                    break;
                case "производитель":
                    Dealership.Cars = typeSort == "По убыванию"
                        ? Dealership.Cars.OrderByDescending(car => car.Maker).ToList()
                        : Dealership.Cars.OrderBy(car => car.Maker).ToList();
                    break;
                case "цвет":
                    Dealership.Cars = typeSort == "По убыванию"
                        ? Dealership.Cars.OrderByDescending(car => car.Color).ToList()
                        : Dealership.Cars.OrderBy(car => car.Color).ToList();
                    break;
                default:
                    Dealership.Cars = typeSort == "По убыванию"
                        ? Dealership.Cars.OrderByDescending(car => car.Mileage).ToList()
                        : Dealership.Cars.OrderBy(car => car.Mileage).ToList();
                    break;
            }
            UpdateTable();
        }

        /// <summary>
        /// Фильтрует список автомобилей по введенному тексту.
        /// </summary>
        /// <param name="text">Текст для поиска.</param>
        public void FilterTable(string text)
        {
            string searchText = text.ToLower();
            var filteredCars = Dealership.Cars
                .Where(car => car.BrandCar.ToLower().Contains(searchText) ||
                              car.Maker.ToLower().Contains(searchText) ||
                              car.Color.ToLower().Contains(searchText) ||
                              car.Cost.ToString().Contains(searchText) ||
                              car.ReleaseYear.ToString().Contains(searchText) ||
                              car.Mileage.ToString().Contains(searchText))
                .ToList();
            Dealership.Cars = filteredCars;
            UpdateTable();
        }

        /// <summary>
        /// Выделяет строки таблицы, содержащие заданный текст.
        /// </summary>
        /// <param name="param">Текст для поиска.</param>
        public void FindStronTable(string param)
        {
            string searchText = param.ToLower();
            foreach (DataGridViewRow row in datagridview1.Rows)
            {
                bool containsText = row.Cells["Brand"].Value.ToString().ToLower().Contains(searchText) ||
                                    row.Cells["Maker"].Value.ToString().ToLower().Contains(searchText) ||
                                    row.Cells["Color"].Value.ToString().ToLower().Contains(searchText) ||
                                    row.Cells["Cost"].Value.ToString().ToLower().Contains(searchText) ||
                                    row.Cells["Year"].Value.ToString().ToLower().Contains(searchText) ||
                                    row.Cells["Mileage"].Value.ToString().ToLower().Contains(searchText);

                if (containsText)
                    row.DefaultCellStyle.BackColor = Color.YellowGreen;
            }
        }

        /// <summary>
        /// Возвращает таблицу к исходному состоянию.
        /// </summary>
        public void CancelSort()
        {
            Dealership.Cars = default_List;
            UpdateTable();
        }

        /// <summary>
        /// Сброс фильтрации, возвращая исходный список.
        /// </summary>
        public void CancelFilter()
        {
            Dealership.Cars = default_List;
            UpdateTable();
        }

        /// <summary>
        /// Снимает выделение с строк таблицы.
        /// </summary>
        public void CancelFinder()
        {
            foreach (DataGridViewRow rows in datagridview1.Rows)
                rows.DefaultCellStyle.BackColor = Color.White;
        }
    }
}