using static CodingTracker.TableVisualization;
using static CodingTracker.CodingSessionRepository;

namespace CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateTable();
            Insert(DatabaseConnection());
            PrintTable(GetRecords(DatabaseConnection()));
            
        }
    
    }

}