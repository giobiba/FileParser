using System;
using System.IO;

namespace FileParser
{
    public class TxtParser : Parser
    {
        public TxtParser(string path) : base(path) { }

        public override string[] ParseLine(string line)
        {

            string[] result = new string[3];

            if (string.IsNullOrWhiteSpace(line))
            {
                return new string[0];
            }

            string[] split_line = line.Split(":", 2);

            if (split_line.Length != 2)
            {
                return new string[0];
            }

            result[0] = split_line[0].Trim();
            string rest = split_line[1].Trim();

            string[] split_rest = rest.Split(" ", 2);

            if (split_rest.Length != 2)
            {
                return new string[0];
            }

            result[1] = split_rest[0].Trim();
            result[2] = split_rest[1].Trim();

            return result;
        }

    }
}
