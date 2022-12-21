using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;
using CodingTracker.Models;
using System.Globalization;

namespace CodingTracker
{
    internal class CodingSessionRepository
    {
        static string connection = ConfigurationManager.ConnectionStrings["CnnString"].ConnectionString;
        private static SQLiteConnection _sqliteConnection;

        internal static SQLiteConnection DatabaseConnection()
        {
            _sqliteConnection = new SQLiteConnection(connection);
            _sqliteConnection.Open();
            return _sqliteConnection;

        }

        internal static void CreateTable()
        {
            using (var cmd = DatabaseConnection().CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding_session (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT
                    )";

                cmd.ExecuteNonQuery();
            }

        }

        internal static void GetRecords(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            var cmd = conn.CreateCommand();

            cmd.CommandText =
                "SELECT * FROM coding_session";

            reader = cmd.ExecuteReader();

            List<CodingSession> tableData = new();

            while (reader.Read())
            {
                tableData.Add(
                    new CodingSession
                    {
                        Id = reader.GetInt32(0),
                        StartTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy HH:mm", CultureInfo.InvariantCulture),
                        EndTime = DateTime.ParseExact(reader.GetString(2), "dd-MM-yy HH:mm", CultureInfo.InvariantCulture),
                        Duration = DateTime.ParseExact(reader.GetString(3), "dd-MM-yy HH:mm", CultureInfo.InvariantCulture)
                    }); ;
            }

            PrintRecords(tableData);

            conn.Close();

        }

        internal static void PrintRecords(List<CodingSession> tableData)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------\n");
            
            foreach (var record in tableData)
            {
                Console.WriteLine($"{record.Id} - Start Time: {record.StartTime} | End Time: {record.EndTime} | Duration: {record.EndTime.Subtract(record.StartTime)}");
            }

            Console.WriteLine("\n-----------------------------------------------------------------------------------------\n");

        }

        internal static void Insert(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();

            cmd.CommandText =
                @"INSERT INTO coding_session(StartTime, EndTime, Duration) VALUES('21-12-22 11:10', '22-12-22 20:20', '22-12-22 20:40')";

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
