using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser;
using System;
using System.Collections.Generic;

namespace FileParserTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestCsvFile()
        {
            string startupPath = Environment.CurrentDirectory;
            string csv_file = startupPath + "\\TestData\\data.csv";
            string[] args = { csv_file };
            Program program = new Program(args);
            List<uint> expected = new List<uint>()
            {
                3, 6, 25, 38, 50
            };

            List<uint> actual = program.ParseFile(csv_file);
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
