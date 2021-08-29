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

        private void addPathIfValid (string path)
        {
            if (HelperFunctions.VerifyPath(path))
            {
                paths.Add(path);
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
                Console.WriteLine($"Found {lines_detected.Count} mistakes in the date column, on the following lines:");
            }
            else
            {
                Console.WriteLine($"Found {lines_detected.Count} errors, on the following lines:");
            }

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
            // verificam daca au fost trimise argumente
            if (args.Length == 0)
            {
                string path;
                // daca nu, le introduce utilizatorul
                Console.WriteLine("Write a file path you want to read from.");
                while (true)
                {
                    path = Console.ReadLine();
                    addPathIfValid(path);

                    Console.WriteLine("Do you want to add another file path?(y/n)");
                    if (Console.Read() == 'n') break;
                }

            }
            else
            {
                // daca da, verificam fiecare path in parte sa fie in regula
                foreach (string path in args)
                {
                    addPathIfValid(path);
                }
            }

            foreach (var path in paths)
            {
                List<uint> lines_detected = ParseFile(path);
                PrintResults(path, lines_detected);
            }
        }
    }
}
