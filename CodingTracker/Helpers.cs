using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class Helpers
    {
        internal static string GetInputTime(string message)
        {
            var provider = CultureInfo.InvariantCulture;

            Console.WriteLine(message);
            var input = Console.ReadLine();

            if (input == "0") return input;

            while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm:ss", provider, DateTimeStyles.None, out _))
            {
                Console.WriteLine("\nInvalid input (Format: dd-mm-yy hh:mm:ss). Type 0 to go back to menu or try again.");
                input = Console.ReadLine();
            }    

            return input;

        }
        
        internal static string GetId(string message)
        {
            Console.Out.WriteLine(message);
            var input = Console.ReadLine();

            return input;

        }
    }
}
