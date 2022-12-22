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

        internal static List<CodingSession> GetRecords(SQLiteConnection conn)
        {
            List<CodingSession> tableData = new();

            SQLiteDataReader reader;
            var cmd = conn.CreateCommand();

            cmd.CommandText =
                "SELECT * FROM coding_session";

            reader = cmd.ExecuteReader();

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

            conn.Close();

            return tableData;
        }

        internal static void Insert(SQLiteConnection conn)
        {
            var cmd = conn.CreateCommand();

            var msgStart = ConfigurationManager.AppSettings.Get("StartTime");
            var startTime = Helpers.GetInput($"\n{msgStart}");

            if (startTime.Equals("0")) return;

            var msgEnd = ConfigurationManager.AppSettings.Get("EndTime");
            var endTime = Helpers.GetInput($"\n{msgEnd}");

            if (endTime.Equals("0")) return;

            cmd.CommandText =
                $"INSERT INTO coding_session(StartTime, EndTime, Duration) VALUES('{startTime}', '{endTime}', '22-12-22 20:40')";

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}

