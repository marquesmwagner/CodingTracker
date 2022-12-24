using System;
using System.Collections.Generic;
using System.Configuration;
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
            var validationDate = ConfigurationManager.AppSettings.Get("ValidationDate");

            Console.WriteLine(message);
            var input = Console.ReadLine();

            if (input == "0") return input;

            while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm:ss", provider, DateTimeStyles.None, out _))
            {
                Console.WriteLine($"\n{validationDate}");
                input = Console.ReadLine();
                if (input == "0") return input;
            }    

            return input;

        }
        
        internal static string GetId(string message)
        {
            var validationId = ConfigurationManager.AppSettings.Get("ValidationId");

            Console.Out.WriteLine(message);
            var input = Console.ReadLine();

            while (!int.TryParse(input, out _))
            {
                Console.WriteLine($"\n{validationId}");
                input = Console.ReadLine(); 
            }

            return input;

        }
    }
}
