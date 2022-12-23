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
                .ExportAndWrite();

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
