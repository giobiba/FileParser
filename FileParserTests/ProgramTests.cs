using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileParserTests
{
    [TestClass]
    public class ProgramTests
    {
        // metoda testeaza daca parserul gaseste toate errorile din fisierul citit 
        [TestMethod]
        public void TestCsvFile()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName; ;
            string csv_file = projectDirectory + "\\TestData\\data.csv";
            string[] args = { csv_file };
            Program program = new Program(args);
            List<uint> expected = new List<uint>()
            {
                3, 6, 8, 25, 38, 50
            };

            List<uint> actual = program.ParseFile(csv_file);
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
