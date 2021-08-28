using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace FileParser
{
    public static class HelperFunctions
    {
        // format chosen for validation is dd/mm/yyyy
        public static bool isDateValid(string input)
        {
            return DateTime.TryParseExact(input, "dd/MM/yyyy",  new CultureInfo("ro-RO"), DateTimeStyles.None, out _);
        }

        public static bool VerifyPath(string path)
        {
            return File.Exists(path) && Path.GetExtension(path) == ".txt" || Path.GetExtension(path) == ".csv";
        }

        // cele doua functii de jos returneaza true daca au descoperit o eroare
        public static bool DetectError(string[] parsed_line)
        {
            if (parsed_line.Length != 3)
                return true;

            return parsed_line[0].Equals("Error");
        }
        public static bool DetectDateFormatError(string[] parsed_line)
        {
            if (parsed_line.Length != 4)
                return true;

            return !HelperFunctions.isDateValid(parsed_line[3]);
        }
    }
}
