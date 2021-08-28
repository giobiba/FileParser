using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser;

namespace FileParserTests
{
    [TestClass]
    public class HelperFunctionsTests
    {
        [TestMethod]
        public void TestValidDate()
        {
            string date = "17/05/2000";
            bool expected = true;

            bool actual = HelperFunctions.isDateValid(date);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidDate()
        {
            string date = "01/13/2000";
            bool expected = false;

            bool actual = HelperFunctions.isDateValid(date);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ErrorLineDetectionTest()
        {
            string[] parsed_line = { "Error", "L19", "Implicitly typed variables must be initialized" };
            bool expected = true;

            bool actual = HelperFunctions.DetectError(parsed_line);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NonErrorLineDetectionTest()
        {
            string[] parsed_line = { "Info", "L123", "Loaded \"LINQ\" package"};
            bool expected = false;

            bool actual = HelperFunctions.DetectError(parsed_line);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CorrectDateDetectionTest()
        {
            string[] parsed_line = { "17", "Nettie", "Largent", "07/04/2021" };
            bool expected = false;

            bool actual = HelperFunctions.DetectDateFormatError(parsed_line);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncorrectDateDetectionTest()
        {
            string[] parsed_line = { "3", "Sandie", "Huckstepp", "33/10/2019" };
            bool expected = true;

            bool actual = HelperFunctions.DetectDateFormatError(parsed_line);

            Assert.AreEqual(expected, actual);
        }
    }
}
