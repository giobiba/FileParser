using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser
{
    public class Program
    {
        private string[] args;
        private List<string> paths = new List<string>();

        delegate bool DetectErrorLine(string[] parsed_line);

        private void addPathIfValid (string path, ref uint paths_read)
        {
            if (HelperFunctions.VerifyPath(path))
            {
                paths.Add(path);
                paths_read += 1;
            }
            else
            {
                Console.WriteLine($"The path: {path} is invalid.");
            }
        }

        private void PrintResults(string path, List<uint> lines_detected)
        {
            if (Path.GetExtension(path) == ".csv")
            {
                Console.Write($"Found {lines_detected.Count} mistakes in the date column");
            }
            else
            {
                Console.Write($"Found {lines_detected.Count} errors");
            }
            if (lines_detected.Count == 1) Console.WriteLine(", on the following line:");
            else if (lines_detected.Count > 1) Console.WriteLine(", on the following lines:");

            foreach (var k in lines_detected)
            {
                Console.Write($"{k} ");
            }
            Console.WriteLine();
        }

        public List<uint> ParseFile(string path)
        {
            DetectErrorLine validateLine;
            Parser parser;
            List<uint> lines_detected = new List<uint>();

            if (Path.GetExtension(path) == ".csv")
            {
                validateLine = HelperFunctions.DetectDateFormatError;
            }
            else
            {
                validateLine = HelperFunctions.DetectError;
            }

            // scriem in consola numele fisierului 
            Console.WriteLine($"Reading file {Path.GetFileName(path)}\n");

            Parser.CreateParser(out parser, path);

            uint i = 1;
            foreach (string[] parsed_line in parser.ParseFile())
            {
                if (validateLine(parsed_line))
                {
                    lines_detected.Add(i);
                }
                i++;
            }

            return lines_detected;
        }

        public Program(string[] args)
        {
            this.args = args;
        }

        public void Run()
        {
            uint paths_read = 0;
            // verificam daca au fost trimise argumente
            if (args.Length == 0)
            {
                string path;
                // daca nu, le introduce utilizatorul
                Console.WriteLine("Write a file path you want to read from.");
                while (true)
                {
                    path = Console.ReadLine().Trim();
                    addPathIfValid(path, ref paths_read);

                    Console.WriteLine("Do you want to add another file path?(y/n)");
                    var key = Console.ReadLine();
                    if (key == "n" && paths_read != 0) break;
                }
            }
            else
            {
                // daca da, verificam fiecare path in parte sa fie in regula
                foreach (string path in args)
                {
                    Console.Write(path);
                    addPathIfValid(path, ref paths_read);
                }
            }

            foreach (var path in paths)
            {
                List<uint> lines_detected = ParseFile(path);
                PrintResults(path, lines_detected);
            }

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
}
