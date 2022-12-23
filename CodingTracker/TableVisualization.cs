using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Models;
using ConsoleTableExt;

namespace CodingTracker
{
    internal class TableVisualization
    {
        internal static void PrintTable(List<CodingSession> tableData)
        {
            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("RECORDS")
                .WithColumn("ID", "Start Time", "End Time", "Duration")
                .ExportAndWrite();

            Console.WriteLine("\nType Enter to go back to menu.");
            Console.ReadKey();
        }
        
        internal static void PrintMenu(List<List<object>> tableMenu)
        {
            ConsoleTableBuilder
                .From(tableMenu)
                .WithTitle("CODING TRACKER MENU")
                .WithColumn("Option", "Description")
                .ExportAndWrite();
        }
    }
}
