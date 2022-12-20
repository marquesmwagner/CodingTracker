using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CodingTracker
{
    internal class CodingSessionRepository
    {
        static string connectionString = "coding-tracker.db";

        internal static void CreateDatabase()
        {
            SQLiteConnection.CreateFile(connectionString);
        }
    }
}
