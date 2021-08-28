namespace FileParser
{
    public class CsvParser : Parser
    {
        public CsvParser(string path) : base(path) { }
         
        public override string[] ParseLine(string line)
        {
            string[] split_line = line.Split(",");
            for(int i = 0; i < split_line.Length; i++)
            {
                split_line[i] = split_line[i].Trim();
            }

            return split_line;
        }
    }
}
