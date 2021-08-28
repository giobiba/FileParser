using System;
using System.Collections.Generic;
using System.IO;

namespace FileParser
{
    public abstract class Parser
    {
        protected StreamReader sv;
        protected string path;

        public Parser(string path)
        {
            this.path = path;
        }
        ~Parser()
        {
            sv.Close();
        }
        
        public string NextLine()
        {
            return sv.ReadLine();
        }

        public abstract string[] ParseLine(string line);


        public bool isEmpty { 
            get => sv.EndOfStream;
        }

        public IEnumerable<string[]> ParseFile()
        {
            using (sv = new StreamReader(path))
            {
                while (!this.isEmpty)
                {
                    var line = this.NextLine();
                    yield return this.ParseLine(line);
                }
            }

        }

        public static void CreateParser(out Parser parser, string path)
        {
            if (Path.GetExtension(path) == ".csv")
            {
                parser = new CsvParser(path);
            }
            else
            {
                parser = new TxtParser(path);
            }
        }
    }
}
