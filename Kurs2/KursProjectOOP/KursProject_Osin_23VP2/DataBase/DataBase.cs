using KursProject_Osin_23VP2;
using Npgsql;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

/// <summary>
/// Класс для работы с базой данных PostgreSQL, включающий функции открытия, сохранения, экспорта и управления базой.
/// </summary>
public class DataBase
{
    private DataGridView dataGridView;
    private string connectionString;
    public string currentDatabase;
    private const string PgPassword = "234535"; // Пароль для подключения к PostgreSQL
    private const string PgUser = "postgres";   // Имя пользователя PostgreSQL
    private const string PgHost = "localhost";  // Адрес хоста базы данных
    private const int PgPort = 5432;             // Порт подключения

    /// <summary>
    /// Конструктор класса, инициализирующий DataGridView и задающий начальную базу данных.
    /// </summary>
    /// <param name="dgv">Объект DataGridView для отображения данных.</param>
    public DataBase(DataGridView dgv)
    {
        dataGridView = dgv;
        currentDatabase = "postgres";
        UpdateConnectionString("postgres");
    }

    /// <summary>
    /// Обновляет строку подключения к базе данных, основываясь на имени базы.
    /// </summary>
    /// <param name="dbName">Имя базы данных.</param>
    private void UpdateConnectionString(string dbName)
    {
        currentDatabase = dbName;
        connectionString = $"Host={PgHost};Port={PgPort};Username={PgUser};Password={PgPassword};Database={dbName};";
    }

    /// <summary>
    /// Открывает файл SQL и восстанавливает базу данных из него.
    /// </summary>
    /// <param name="filePath">Путь к файлу .sql.</param>
    /// <returns>Истина, если восстановление прошло успешно; иначе ложь.</returns>
    public bool OpenSqlFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath) || Path.GetExtension(filePath).ToLower() != ".sql")
        {
            MessageBox.Show("Выберите корректный файл .sql");
            return false;
        }

        string dbName = Path.GetFileNameWithoutExtension(filePath);
        string psqlPath = @"C:\Program Files\PostgreSQL\17\bin\psql.exe"; // Путь к psql.exe

        var psi = new ProcessStartInfo
        {
            FileName = psqlPath,
            Arguments = $"-U {PgUser} -d postgres -f \"{filePath}\"",
            UseShellExecute = false,
            RedirectStandardError = true,
            CreateNoWindow = true,
            Environment = { ["PGPASSWORD"] = PgPassword }
        };

        try
        {
            using (var process = Process.Start(psi))
            {
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Ошибка восстановления БД:\n{error}");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при запуске процесса восстановления БД: {ex.Message}");
            return false;
        }

        UpdateConnectionString(dbName);

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения к базе данных после восстановления: {ex.Message}");
            return false;
        }
        LoadCarsToDataGridView();
        MessageBox.Show($"База данных '{dbName}' восстановлена и подключена.");
        return true;
    }

    /// <summary>
    /// Загружает данные из таблицы cars в DataGridView и список Dealership.
    /// </summary>
    public void LoadCarsToDataGridView()
    {
        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmdCheck = new NpgsqlCommand("SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name='cars')", conn);
                if (!(bool)cmdCheck.ExecuteScalar())
                {
                    MessageBox.Show("Таблица cars не найдена.");
                    return;
                }

                // Запрос данных из таблицы cars
                var dt = new DataTable();
                var cmd = new NpgsqlCommand("SELECT * FROM cars", conn);
                var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    var car = new Dealership(
                        row["Марка"].ToString(),
                        Convert.ToInt32(row["Цена"]),
                        Convert.ToInt32(row["Год"]),
                        row["Производитель"].ToString(),
                        row["Цвет"].ToString(),
                        Convert.ToInt32(row["Пробег"])
                    );
                    Dealership.AddCar(car);
                }

                // Обновление DataGridView с учетом потокобезопасности
                if (dataGridView.InvokeRequired)
                {
                    dataGridView.Invoke(new Action(() => UpdateDataGridView(dt)));
                }
                else
                {
                    UpdateDataGridView(dt);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
        }
    }

    /// <summary>
    /// Обновляет DataGridView данными из DataTable.
    /// </summary>
    /// <param name="dt">DataTable с данными для отображения.</param>
    private void UpdateDataGridView(DataTable dt)
    {
        // Создаем столбцы, если их еще нет
        if (dataGridView.Columns.Count == 0)
        {
            foreach (DataColumn column in dt.Columns)
            {
                dataGridView.Columns.Add(column.ColumnName, column.ColumnName);
            }
        }
        // Заполняем строки
        foreach (DataRow row in dt.Rows)
        {
            int rowIndex = dataGridView.Rows.Add();
            for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
            {
                dataGridView.Rows[rowIndex].Cells[colIndex].Value = row[colIndex];
            }
        }
    }

    /// <summary>
    /// Создает резервную копию базы данных в виде SQL-файла.
    /// </summary>
    /// <param name="outputPath">Путь для сохранения файла.</param>
    /// <returns>Истина, если успешно; иначе ложь.</returns>
    public bool SaveDatabase(string outputPath)
    {
        SaveCarsToDatabase();

        string pgDumpPathSql = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe";

        if (!File.Exists(pgDumpPathSql))
        {
            MessageBox.Show("pg_dump.exe не найден по пути: " + pgDumpPathSql);
            return false;
        }

        string sqlExtension = ".sql";
        outputPath = Path.ChangeExtension(outputPath, sqlExtension);
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? string.Empty);

        string sqlFormatFlag = "-F p"; // Plain формат
        string sqlArgs = $"-U {PgUser} -d \"{currentDatabase}\" {sqlFormatFlag} -f \"{outputPath}\"";

        var sqlPsi = new ProcessStartInfo
        {
            FileName = pgDumpPathSql,
            Arguments = sqlArgs,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            Environment = { ["PGPASSWORD"] = PgPassword }
        };

        try
        {
            using (var process = Process.Start(sqlPsi))
            {
                string errorOutput = process.StandardError.ReadToEnd();
                string standardOutput = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Ошибка при сохранении базы:\n{errorOutput}");
                    Console.WriteLine($"Ошибка при сохранении базы: {errorOutput}");
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(standardOutput))
                {
                    Console.WriteLine($"Стандартный вывод:\n{standardOutput}");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при запуске pg_dump: {ex.Message}");
            Console.WriteLine($"Ошибка при запуске pg_dump: {ex.Message}");
            return false;
        }

        MessageBox.Show($"База данных сохранена в SQL-файл по пути: {outputPath}");
        return true;
    }

    /// <summary>
    /// Экспортирует таблицу cars в PDF-файл.
    /// </summary>
    /// <param name="outputPath">Путь к сохраняемому PDF.</param>
    public void SaveCarsTableToPdf(string outputPath)
    {
        try
        {
            outputPath = Path.ChangeExtension(outputPath, ".pdf");
            DataTable carsTable;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand("SELECT * FROM cars", conn);
                var adapter = new NpgsqlDataAdapter(cmd);
                carsTable = new DataTable();
                adapter.Fill(carsTable);
            }

            var doc = new PdfDocument();
            doc.Info.Title = "Таблица машин";
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            var headerFont = new XFont("Arial", 14, XFontStyle.Bold);
            var cellFont = new XFont("Arial", 10);
            double margin = 20, rowHeight = 20, y = margin;
            int colCount = carsTable.Columns.Count;
            double colWidth = (page.Width - 2 * margin) / colCount;

            // Заголовок
            gfx.DrawString("Список машин в автосалоне", headerFont, XBrushes.Black,
                new XRect(0, y, page.Width, rowHeight), XStringFormats.TopCenter);
            y += rowHeight * 1.5;

            // Заголовки таблицы
            for (int i = 0; i < colCount; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, new XRect(margin + i * colWidth, y, colWidth, rowHeight));
                gfx.DrawString(carsTable.Columns[i].ColumnName, cellFont, XBrushes.Black,
                    new XRect(margin + i * colWidth, y, colWidth, rowHeight), XStringFormats.Center);
                gfx.DrawRectangle(XPens.Black, new XRect(margin + i * colWidth, y, colWidth, rowHeight));
            }
            y += rowHeight;

            // Данные таблицы
            foreach (DataRow row in carsTable.Rows)
            {
                if (y + rowHeight > page.Height - margin)
                {
                    page = doc.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = margin;
                }

                for (int i = 0; i < colCount; i++)
                {
                    gfx.DrawRectangle(XBrushes.White, new XRect(margin + i * colWidth, y, colWidth, rowHeight));
                    gfx.DrawString(row[i]?.ToString() ?? "", cellFont, XBrushes.Black,
                        new XRect(margin + i * colWidth, y, colWidth, rowHeight), XStringFormats.Center);
                    gfx.DrawRectangle(XPens.Black, new XRect(margin + i * colWidth, y, colWidth, rowHeight));
                }
                y += rowHeight;
            }
            doc.Save(outputPath);
            MessageBox.Show($"Данные таблицы сохранены в PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении PDF: {ex.Message}");
        }
    }

    /// <summary>
    /// Удаляет базу данных по имени.
    /// </summary>
    /// <param name="dbName">Имя базы данных для удаления.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public bool DeleteDatabase(string dbName)
    {
        try
        {
            // Удаление базы данных PostgreSQL
            var masterConnStr = $"Host={PgHost};Port={PgPort};Username={PgUser};Password={PgPassword};Database=postgres;";
            using (var conn = new NpgsqlConnection(masterConnStr))
            {
                conn.Open();
                // Завершаем все соединения с целевой БД
                var terminateCmd = new NpgsqlCommand(@"
            SELECT pg_terminate_backend(pid) 
            FROM pg_stat_activity 
            WHERE datname = @db AND pid <> pg_backend_pid();", conn);
                terminateCmd.Parameters.AddWithValue("db", dbName);
                terminateCmd.ExecuteNonQuery();

                // Удаляем саму базу данных
                var dropCmd = new NpgsqlCommand($"DROP DATABASE IF EXISTS \"{dbName}\";", conn);
                dropCmd.ExecuteNonQuery();
            }
            bool fileDeleted = false;
            string baseDirectory = Application.StartupPath;
            string searchPattern = $"{dbName}.sql";
            try
            {
                var foundFiles = Directory.GetFiles(baseDirectory, searchPattern, SearchOption.AllDirectories);
                foreach (string filePath in foundFiles)
                {
                    try
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            fileDeleted = true;
                        }
                    }
                    catch (Exception fileEx)
                    {
                        Console.WriteLine($"Ошибка при удалении файла {filePath}: {fileEx.Message}");
                    }
                }

                if (fileDeleted)
                {
                    MessageBox.Show($"База данных '{dbName}' и соответствующий .sql файл успешно удалены.",
                                  "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"База данных '{dbName}' удалена, но не удалось удалить .sql файл(ы).",
                                  "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception searchEx)
            {
                MessageBox.Show($"База данных '{dbName}' удалена, но произошла ошибка при поиске .sql файла: {searchEx.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка удаления базы данных: {ex.Message}",
                          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }


    /// <summary>
    /// Создает новую базу данных с указанным именем.
    /// </summary>
    /// <param name="dbName">Имя создаваемой базы.</param>
    public void CreateDatabase(string dbName)
    {
        try
        {
            var masterConnStr = $"Host={PgHost};Port={PgPort};Username={PgUser};Password={PgPassword};Database=postgres;";
            using (var conn = new NpgsqlConnection(masterConnStr))
            {
                conn.Open();
                var createCmd = new NpgsqlCommand($"CREATE DATABASE \"{dbName}\";", conn);
                createCmd.ExecuteNonQuery();
            }
            CreateCarsTable(dbName);
            UpdateConnectionString(dbName);
            MessageBox.Show($"База данных '{dbName}' успешно создана.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при создании базы данных: {ex.Message}");
        }
    }

    /// <summary>
    /// Создает таблицу cars в базе данных.
    /// </summary>
    /// <param name="dbName">Имя базы данных, в которой создается таблица.</param>
    private void CreateCarsTable(string dbName)
    {
        try
        {
            string connStr = $"Host={PgHost};Port={PgPort};Username={PgUser};Password={PgPassword};Database={dbName};";
            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                var createCmd = new NpgsqlCommand(@"
                CREATE TABLE IF NOT EXISTS cars (
                    ""Марка"" VARCHAR(100) NOT NULL,
                    ""Цена"" INTEGER NOT NULL,
                    ""Год"" INTEGER NOT NULL,
                    ""Производитель"" VARCHAR(100) NOT NULL,
                    ""Цвет"" VARCHAR(50) NOT NULL,
                    ""Пробег"" INTEGER NOT NULL
                );", conn);
                createCmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при создании таблицы cars: {ex.Message}");
        }
    }

    /// <summary>
    /// Сохраняет текущие данные автомобилей в таблицу базы данных.
    /// </summary>
    public void SaveCarsToDatabase()
    {
        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Очистка таблицы перед вставкой
                        new NpgsqlCommand("TRUNCATE TABLE cars", conn, transaction).ExecuteNonQuery();

                        // Вставка каждого автомобиля из списка
                        foreach (var car in Dealership.Cars)
                        {
                            var cmd = new NpgsqlCommand(
                                "INSERT INTO cars (Марка, Цена, Год, Производитель, Цвет, Пробег) " +
                                "VALUES (@Марка, @Цена, @Год, @Производитель, @Цвет, @Пробег)", conn, transaction);
                            cmd.Parameters.AddWithValue("Марка", car.BrandCar);
                            cmd.Parameters.AddWithValue("Цена", car.Cost);
                            cmd.Parameters.AddWithValue("Год", car.ReleaseYear);
                            cmd.Parameters.AddWithValue("Производитель", car.Maker);
                            cmd.Parameters.AddWithValue("Цвет", car.Color);
                            cmd.Parameters.AddWithValue("Пробег", car.Mileage);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Подтверждение транзакции
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Откат при ошибке
                        MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
        }
    }
}