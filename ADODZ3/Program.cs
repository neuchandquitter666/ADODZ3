using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODZ3
{
    internal class Program
    {
        private static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Connect Timeout=30;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Вставка нового канцтовара");
                Console.WriteLine("2. Вставка нового типа канцтовара");
                Console.WriteLine("3. Вставка нового менеджера");
                Console.WriteLine("4. Вставка новой фирмы покупателя");
                Console.WriteLine("5. Обновление канцтоваров");
                Console.WriteLine("6. Обновление фирм");
                Console.WriteLine("7. Обновление менеджеров");
                Console.WriteLine("8. Показать популярные канцтовары");
                Console.WriteLine("9. Показать канцтовары, которые не продавались в заданное количество дней");
                Console.WriteLine("10. Удаление канцтоваров");
                Console.WriteLine("0. Выйти");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": InsertStationery(); break;
                    case "2": InsertStationeryType(); break;
                    case "3": InsertSalesManager(); break;
                    case "4": InsertCustomerFirm(); break;
                    case "5": UpdateStationery(); break;
                    case "6": UpdateCustomerFirm(); break;
                    case "7": UpdateSalesManager(); break;
                    case "8": ShowPopularStationery(); break;
                    case "9": ShowUnsoldStationery(); break;
                    case "10": DeleteStationery(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                        break;
                }
            }
        }

        private static void InsertStationery()
        {
            Console.Write("Введите название канцтовара: ");
            string name = Console.ReadLine();
            Console.Write("Введите ID типа канцтовара: ");
            int typeId = int.Parse(Console.ReadLine());
            Console.Write("Введите цену канцтовара: ");
            decimal price = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Stationery (Name, TypeID, Price) VALUES (@Name, @TypeID, @Price)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@TypeID", typeId);
                command.Parameters.AddWithValue("@Price", price);
                command.ExecuteNonQuery();
                Console.WriteLine("Канцтовар успешно добавлен.");
            }
        }

        private static void InsertStationeryType()
        {
            Console.Write("Введите название типа канцтовара: ");
            string typeName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO StationeryType (TypeName) VALUES (@TypeName)", connection);
                command.Parameters.AddWithValue("@TypeName", typeName);
                command.ExecuteNonQuery();
                Console.WriteLine("Тип канцтовара успешно добавлен.");
            }
        }

        private static void InsertSalesManager()
        {
            Console.Write("Введите имя менеджера: ");
            string name = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO SalesManager (Name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.ExecuteNonQuery();
                Console.WriteLine("Менеджер успешно добавлен.");
            }
        }

        private static void InsertCustomerFirm()
        {
            Console.Write("Введите название фирмы покупателя: ");
            string firmName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO CustomerFirm (FirmName) VALUES (@FirmName)", connection);
                command.Parameters.AddWithValue("@FirmName", firmName);
                command.ExecuteNonQuery();
                Console.WriteLine("Фирма покупателя успешно добавлена.");
            }
        }
    
private static void UpdateStationery()
        {
            Console.Write("Введите ID канцтовара для обновления: ");
            int stationeryId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название канцтовара: ");
            string name = Console.ReadLine();
            Console.Write("Введите новую цену канцтовара: ");
            decimal price = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Stationery SET Name = @Name, Price = @Price WHERE StationeryID = @StationeryID", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@StationeryID", stationeryId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о канцтоваре успешно обновлена.");
            }
        }

        private static void UpdateCustomerFirm()
        {
            Console.Write("Введите ID фирмы для обновления: ");
            int firmId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название фирмы: ");
            string firmName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE CustomerFirm SET FirmName = @FirmName WHERE FirmID = @FirmID", connection);
                command.Parameters.AddWithValue("@FirmName", firmName);
                command.Parameters.AddWithValue("@FirmID", firmId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о фирме покупателе успешно обновлена.");
            }
        }

        private static void UpdateSalesManager()
        {
            Console.Write("Введите ID менеджера для обновления: ");
            int managerId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое имя менеджера: ");
            string name = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE SalesManager SET Name = @Name WHERE ManagerID = @ManagerID", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@ManagerID", managerId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о менеджере успешно обновлена.");
            }
        }

        private static void ShowMostPopularStationery()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Name, SoldUnits FROM Stationery ORDER BY SoldUnits DESC", connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Самые популярные канцтовары:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} - продано единиц: {reader["SoldUnits"]}");
                }
                reader.Close();
            }
        }

        private static void ShowUnsoldStationery(int days)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Name FROM Stationery WHERE LastSaleDate IS NULL OR DATEDIFF(day, LastSaleDate, GETDATE()) >= @Days", connection);
                command.Parameters.AddWithValue("@Days", days);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"Канцтовары, которые не продавались последние {days} дней:");
                while (reader.Read())
                {
                    Console.WriteLine(reader["Name"]);
                }
                reader.Close();
            }
        }

        private static void UpdateStationeryType()
        {
            Console.Write("Введите ID типа канцтовара для обновления: ");
            int typeId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название типа канцтовара: ");
            string typeName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE StationeryType SET TypeName = @TypeName WHERE TypeID = @TypeID", connection);
                command.Parameters.AddWithValue("@TypeName", typeName);
                command.Parameters.AddWithValue("@TypeID", typeId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о типе канцтовара успешно обновлена.");
            }
        }
 

private static void DeleteStationery()
        {
            Console.Write("Введите ID канцтовара для удаления: ");
            int stationeryId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Сначала переносим данные в архив
                SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedStationery SELECT * FROM Stationery WHERE StationeryID = @StationeryID", connection);
                archiveCommand.Parameters.AddWithValue("@StationeryID", stationeryId);
                archiveCommand.ExecuteNonQuery();

                // Затем удаляем канцтовар
                SqlCommand deleteCommand = new SqlCommand("DELETE FROM Stationery WHERE StationeryID = @StationeryID", connection);
                deleteCommand.Parameters.AddWithValue("@StationeryID", stationeryId);
                deleteCommand.ExecuteNonQuery();

                Console.WriteLine("Канцтовар успешно удален и перенесен в архив.");
            }
        }

        private static void DeleteSalesManager()
        {
            Console.Write("Введите ID менеджера для удаления: ");
            int managerId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Сначала переносим данные в архив
                SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedSalesManager SELECT * FROM SalesManager WHERE ManagerID = @ManagerID", connection);
                archiveCommand.Parameters.AddWithValue("@ManagerID", managerId);
                archiveCommand.ExecuteNonQuery();

                // Затем удаляем менеджера
                SqlCommand deleteCommand = new SqlCommand("DELETE FROM SalesManager WHERE ManagerID = @ManagerID", connection);
                deleteCommand.Parameters.AddWithValue("@ManagerID", managerId);
                deleteCommand.ExecuteNonQuery();

                Console.WriteLine("Менеджер успешно удален и перенесен в архив.");
            }
        }

        private static void DeleteStationeryType()
        {
            Console.Write("Введите ID типа канцтовара для удаления: ");
            int typeId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Сначала переносим данные в архив
                SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedStationeryType SELECT * FROM StationeryType WHERE TypeID = @TypeID", connection);
                archiveCommand.Parameters.AddWithValue("@TypeID", typeId);
                archiveCommand.ExecuteNonQuery();

                // Затем удаляем тип канцтовара
                SqlCommand deleteCommand = new SqlCommand("DELETE FROM StationeryType WHERE TypeID = @TypeID", connection);
                deleteCommand.Parameters.AddWithValue("@TypeID", typeId);
                deleteCommand.ExecuteNonQuery();

                Console.WriteLine("Тип канцтовара успешно удален и перенесен в архив.");
            }
        }

        private static void DeleteCustomerFirm()
        {
            Console.Write("Введите ID фирмы покупателя для удаления: ");
            int firmId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Сначала переносим данные в архив
                SqlCommand archiveCommand = new SqlCommand("INSERT INTO ArchivedCustomerFirm SELECT * FROM CustomerFirm WHERE FirmID = @FirmID", connection);
                archiveCommand.Parameters.AddWithValue("@FirmID", firmId);
                archiveCommand.ExecuteNonQuery();

                // Затем удаляем фирму покупателя
                SqlCommand deleteCommand = new SqlCommand("DELETE FROM CustomerFirm WHERE FirmID = @FirmID", connection);
                deleteCommand.Parameters.AddWithValue("@FirmID", firmId);
                deleteCommand.ExecuteNonQuery();

                Console.WriteLine("Фирма покупателя успешно удалена и перенесена в архив.");
            }
        }
        
private static void ShowTopSalesManagerByUnits()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ManagerID, Name, SUM(SoldUnits) AS TotalSalesUnits FROM SalesManager s JOIN Stationery st ON s.ManagerID = st.ManagerID GROUP BY ManagerID, Name ORDER BY TotalSalesUnits DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Менеджер с наибольшим количеством продаж: {reader["Name"]} - продано единиц: {reader["TotalSalesUnits"]}");
                }
                reader.Close();
            }
        }

        private static void ShowTopSalesManagerByProfit()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ManagerID, Name, SUM(Price * SoldUnits) AS TotalProfit FROM SalesManager s JOIN Stationery st ON s.ManagerID = st.ManagerID GROUP BY ManagerID, Name ORDER BY TotalProfit DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Менеджер с наибольшей общей суммой прибыли: {reader["Name"]} - общая прибыль: {reader["TotalProfit"]}");
                }
                reader.Close();
            }
        }

        private static void ShowTopCustomerByPurchase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT FirmID, FirmName, TotalPurchase FROM CustomerFirm ORDER BY TotalPurchase DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Фирма, купившая на самую большую сумму: {reader["FirmName"]} - сумма покупок: {reader["TotalPurchase"]}");
                }
                reader.Close();
            }
        }

        private static void ShowTypeWithMostSales()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT TypeID, TypeName, SUM(SoldUnits) AS TotalUnits FROM StationeryType st JOIN Stationery s ON st.TypeID = s.TypeID GROUP BY TypeID, TypeName ORDER BY TotalUnits DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Тип канцтоваров с наибольшим количеством продаж: {reader["TypeName"]} - продано единиц: {reader["TotalUnits"]}");
                }
                reader.Close();
            }
        }

        private static void ShowMostProfitableStationery()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Name, (Price * SoldUnits) AS TotalProfit FROM Stationery ORDER BY TotalProfit DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Самый прибыльный канцтовар: {reader["Name"]} - общая прибыль: {reader["TotalProfit"]}");
                }
                reader.Close();
            }
        }
      

        private static void ShowPopularStationery()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetPopularStationery", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Самые популярные канцтовары:");
                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Проданные единицы: {reader["SoldUnits"]}");
                }
            }
        }

        private static void ShowUnsoldStationery()
        {
            Console.Write("Введите количество дней: ");
            int days = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUnsoldStationery", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Days", days);

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Канцтовары, которые не продавались больше указанного количества дней:");
                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}");
                }
            }
        }

       
    }
}
