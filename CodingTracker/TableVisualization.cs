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
    }
}
