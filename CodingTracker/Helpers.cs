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

            while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", provider, DateTimeStyles.None, out _))
            {
                Console.WriteLine("\nInvalid input (Format: dd-mm-yy hh:mm). Type 0 to go back to menu or try again.");
                input = Console.ReadLine();
            }    

            return input;

        }
    
    }

}
