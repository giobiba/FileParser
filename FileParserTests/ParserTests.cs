using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser;

namespace FileParserTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseCorrectTxtLine()
        {
            // Arrange
            Parser parser = new TxtParser("");
            string error_line = "Error: L19 Implicitly typed variables must be initialized";
            string[] error_expected = { "Error", "L19", "Implicitly typed variables must be initialized" };

            string info_line = "Info: L103 Thread Initialized";
            string[] info_expected = { "Info", "L103", "Thread Initialized" };

            string warning_line = "Warning: L33 Variable \"x\" unused";
            string[] warning_expected = { "Warning", "L33" , "Variable \"x\" unused"};

            //Act
            string[] error_actual = parser.ParseLine(error_line);
            string[] info_actual = parser.ParseLine(info_line);
            string[] warning_actual = parser.ParseLine(warning_line);


            //Assert
            CollectionAssert.AreEquivalent(error_expected, error_actual);
            CollectionAssert.AreEquivalent(info_expected, info_actual);
            CollectionAssert.AreEquivalent(warning_expected, warning_actual);
        }

        [TestMethod]
        public void ParseIncorrectTxtLine()
        {
            Parser parser = new TxtParser("");
            string faulty_line = "Info: L87 ";
            string[] expected = { };

            string[] actual = parser.ParseLine(faulty_line);

            CollectionAssert.AreEquivalent(expected, actual);
        }
        
        [TestMethod]
        public void ParseCorrectCsvLine()
        {
            //Arrange
            Parser parser = new CsvParser("");
            string line = "11,Alasdair,Fullerlove,17/10/2020";
            string[] expected = { "11", "Alasdair", "Fullerlove", "17/10/2020" };

            //Act
            string[] actual = parser.ParseLine(line);

            //Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
