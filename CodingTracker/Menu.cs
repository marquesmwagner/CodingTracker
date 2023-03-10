using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodingTracker.TableVisualization;
using static CodingTracker.CodingSessionRepository;
using System.Runtime.Intrinsics.X86;

namespace CodingTracker
{
    internal class Menu
    {
        internal static void ShowMenu()
        {
            var tableMenu = new List<List<object>>
            {
                new List<object> {"0", "Close application"},
                new List<object> {"1", "View records"},
                new List<object> {"2", "View records by specific start and end time"},
                new List<object> {"3", "Insert record"},
                new List<object> {"4", "Insert via StopWatch"},
                new List<object> {"5", "Delete record"},
                new List<object> {"6", "Update record"}
            };

            CreateTable();

            var isRun = true;
            while (isRun) 
            {
                Console.WriteLine();
                PrintMenu(tableMenu);
                Console.WriteLine("\n\nEnter a option:");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("\nApplication Closed. Thank you for use");
                        isRun = false;
                        break;
                    case "1":
                        Console.Clear();
                        PrintTable(GetRecords(DatabaseConnection()));
                        Console.WriteLine("\nType Enter to go back to menu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        PrintTable(GetRecordsBySpecificTime(DatabaseConnection()));
                        Console.WriteLine("\nType Enter to go back to menu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        Insert(DatabaseConnection());
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        InsertStopWatch(DatabaseConnection());
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        Delete(DatabaseConnection());
                        Console.Clear();
                        break;
                    case "6":
                        Console.Clear();
                        Update(DatabaseConnection());
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nInvalid input. Please type a valid number.");
                        break;
                }
            }
        }
    }
}
