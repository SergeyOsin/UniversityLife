using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProject_Osin_23VP2
{
    /// <summary>
    /// Класс для описания автомобиля и управления списком автомобилей.
    /// </summary>
    internal class Dealership
    {
        /// <summary>
        /// Марка автомобиля.
        /// </summary>
        public string BrandCar { get; set; }

        /// <summary>
        /// Цена автомобиля.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Год выпуска автомобиля.
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Производитель автомобиля.
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// Цвет автомобиля.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Пробег автомобиля.
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// Статический список всех автомобилей.
        /// </summary>
        public static List<Dealership> Cars { get; set; } = new List<Dealership>();

        /// <summary>
        /// Конструктор для создания нового автомобиля.
        /// </summary>
        /// <param name="brandCar">Марка автомобиля.</param>
        /// <param name="cost">Цена.</param>
        /// <param name="releaseYear">Год выпуска.</param>
        /// <param name="maker">Производитель.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="mileage">Пробег.</param>
        public Dealership(string brandCar, int cost, int releaseYear, string maker, string color, int mileage) =>
            (BrandCar, Cost, ReleaseYear, Maker, Color, Mileage) = (brandCar, cost, releaseYear, maker, color, mileage);

        /// <summary>
        /// Добавляет новый автомобиль в список.
        /// </summary>
        /// <param name="car">Объект Dealership (автомобиль).</param>
        public static void AddCar(Dealership car) => Cars.Add(car);

        /// <summary>
        /// Удаляет автомобиль по индексу.
        /// </summary>
        /// <param name="index">Индекс автомобиля для удаления.</param>
        public static void RemoveCar(int index)
        {
            if (index >= 0 && index < Cars.Count)
                Cars.RemoveAt(index);
        }

        /// <summary>
        /// Обновляет данные автомобиля по индексу.
        /// </summary>
        /// <param name="index">Индекс автомобиля для обновления.</param>
        /// <param name="updatedCar">Объект с новыми данными автомобиля.</param>
        public static void UpdateCar(int index, Dealership updatedCar)
        {
            if (index >= 0 && index < Cars.Count)
                Cars[index] = updatedCar;
        }

        /// <summary>
        /// Очищает весь список автомобилей.
        /// </summary>
        public static void ClearAllCars() => Cars.Clear();
    }
}