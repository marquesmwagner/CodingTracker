using System.Data.SQLite;
using System.Configuration;
using CodingTracker.Models;
using System.Globalization;

namespace CodingTracker
{
    internal class CodingSessionRepository
    {
        private static string connection = ConfigurationManager.ConnectionStrings["CnnString"].ConnectionString;
        private static SQLiteConnection _sqliteConnection;
        private static bool listIsEmpty = true;

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

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableData.Add(
                        new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            StartTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader.GetString(2), "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture),
                            Duration = reader.GetString(3)
                        }); ;
                }
                listIsEmpty = false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nNo records found. The list is empty.");
                listIsEmpty = true;
            }

            return tableData;

        }

        internal static void Insert(SQLiteConnection conn)
        {
            TableVisualization.PrintTable(GetRecords(conn));

            var msgStart = ConfigurationManager.AppSettings.Get("StartTime");
            var startTime = Helpers.GetInputTime($"\n{msgStart}");

            if (startTime.Equals("0")) return;

            var msgEnd = ConfigurationManager.AppSettings.Get("EndTime");
            var endTime = Helpers.GetInputTime($"\n{msgEnd}");

            if (endTime.Equals("0")) return;

            var durationTime = DateTime.ParseExact(endTime, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture) - DateTime.ParseExact(startTime, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture);

            TimeSpan timeTicks = new TimeSpan(durationTime.Ticks);

            var duration = string.Format("{0:00}:{1:00}:{2:00}", (int)timeTicks.TotalHours, timeTicks.Minutes, timeTicks.Seconds);

            var cmd = conn.CreateCommand();

            cmd.CommandText =
                $"INSERT INTO coding_session(StartTime, EndTime, Duration) VALUES('{startTime}', '{endTime}', '{duration}')";

            cmd.ExecuteNonQuery();

            Console.WriteLine("\nSucessfully inserted record. Type Enter to go back to menu.");
            Console.ReadKey();

            conn.Close();

        }

        internal static void Delete(SQLiteConnection conn)
        {
            TableVisualization.PrintTable(GetRecords(conn));

            if (listIsEmpty) return;

            var msgDelete = ConfigurationManager.AppSettings.Get("IdDelete");
            var inputId = Helpers.GetId($"\n\n{msgDelete}");

            if (inputId.Equals("0")) return;

            var cmd = conn.CreateCommand();

            cmd.CommandText =
                $"DELETE FROM coding_session WHERE Id = '{inputId}'";

            var rowCount = cmd.ExecuteNonQuery();

            if (rowCount == 0)
            {
                Console.Clear();
                Console.WriteLine($"\nRecord with Id {inputId} doesn't exist. Try again.\n");
                Delete(conn);
            }
            else
            {
                Console.WriteLine($"\nRecord with Id {inputId} was deleted.");
                Console.WriteLine("\nSucessfully deleted record. Type Enter to go back to menu.");
                Console.ReadKey();
            }

            conn.Close();

        }

        internal static void Update(SQLiteConnection conn)
        {
            TableVisualization.PrintTable(GetRecords(conn));

            if (listIsEmpty) return;

            var msgUpdate = ConfigurationManager.AppSettings.Get("IdUpdate");
            var inputId = Helpers.GetId($"\n{msgUpdate}");

            if (inputId.Equals("0")) return;

            var id = Convert.ToInt32(inputId);

            var checkCmd = conn.CreateCommand();

            checkCmd.CommandText =
                $"SELECT EXISTS(SELECT 1 FROM coding_session WHERE Id = {id})";

            int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (checkQuery == 0)
            {
                Console.Clear();
                Console.WriteLine($"\nRecord with Id {inputId} doesn't exist. Try again.\n");
                Update(conn);
                return;
            }

            var msgStart = ConfigurationManager.AppSettings.Get("StartTime");
            var startTime = Helpers.GetInputTime($"\n{msgStart}");

            if (startTime.Equals("0")) return;

            var msgEnd = ConfigurationManager.AppSettings.Get("EndTime");
            var endTime = Helpers.GetInputTime($"\n{msgEnd}");

            if (endTime.Equals("0")) return;

            var durationTime = DateTime.ParseExact(endTime, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture) - DateTime.ParseExact(startTime, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture);

            TimeSpan timeTicks = new TimeSpan(durationTime.Ticks);

            var duration = string.Format("{0:00}:{1:00}:{2:00}", (int)timeTicks.Hours, timeTicks.Minutes, timeTicks.Seconds);

            var cmd = conn.CreateCommand();
            cmd.CommandText =
                $"UPDATE coding_session SET StartTime = '{startTime}', EndTime = '{endTime}', Duration = '{duration}' WHERE Id = {id}";

            cmd.ExecuteNonQuery();

            Console.WriteLine("\nSucessfully updated record. Type Enter to go back to menu.");
            Console.ReadKey();

            conn.Close();

        }

        internal static void InsertStopWatch(SQLiteConnection conn)
        {
            var msgStart = ConfigurationManager.AppSettings.Get("StopWatchStart");
            var inputStart = Helpers.GetId($"\n{msgStart}");

            if (inputStart.Equals("0")) return;

            var timeStart = DateTime.Now;

            var startTime = timeStart.ToString("dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture);

            var msgEnd = ConfigurationManager.AppSettings.Get("StopWatchEnd");
            var inputEnd = Helpers.GetId($"\n{msgEnd}");

            if (inputEnd.Equals("0")) return;

            var timeEnd = DateTime.Now;

            var endTime = timeEnd.ToString("dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture);

            var durationTime = timeEnd - timeStart;

            var duration = string.Format("{0:00}:{1:00}:{2:00}", (int)durationTime.Hours, durationTime.Minutes, durationTime.Seconds);

            var cmd = conn.CreateCommand();
            cmd.CommandText =
                $"INSERT INTO coding_session(StartTime, EndTime, Duration) VALUES('{startTime}', '{endTime}', '{duration}')";

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        internal static List<CodingSession> GetRecordsBySpecificTime(SQLiteConnection conn)
        {
            TableVisualization.PrintTable(GetRecords(conn));

            List<CodingSession> tableData = new();

            if (listIsEmpty) return tableData;

            var msgStart = ConfigurationManager.AppSettings.Get("PeriodStart");
            var inputStart = Helpers.GetPeriod($"\n{msgStart}");

            if (inputStart == "0") return tableData;

            var msgEnd = ConfigurationManager.AppSettings.Get("PeriodEnd");
            var inputEnd = Helpers.GetPeriod($"\n{msgEnd}");
            Console.Clear();

            if (inputEnd == "0") return tableData;

            SQLiteDataReader reader;
            var cmd = conn.CreateCommand();

            cmd.CommandText =
                $"SELECT * FROM coding_session WHERE substr(StartTime, 1, 8) = '{inputStart}' AND substr(EndTime, 1, 8) = '{inputEnd}' ORDER BY StartTime";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tableData.Add(
                    new CodingSession
                    {
                        Id = reader.GetInt32(0),
                        StartTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture),
                        EndTime = DateTime.ParseExact(reader.GetString(2), "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture),
                        Duration = reader.GetString(3)
                    });
            }

            return tableData;
        }
    }
}
