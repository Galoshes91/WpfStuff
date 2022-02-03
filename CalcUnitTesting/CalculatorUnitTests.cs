using static Calculator.Calculate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass()]
    public class CalculatorUnitTests
    {
        Calculate calculate = new Calculate();

        [TestMethod]
        public void setNumber_Test()
        {
            calculate.setNumber(3);
            Assert.AreEqual(3, calculate.numOne);
            Assert.AreEqual(0, calculate.numTwo);

            calculate.setNumber(4);
            Assert.AreEqual(3, calculate.numOne);
            Assert.AreEqual(4, calculate.numTwo);
        }

        [TestMethod]
        public void clearNumbers_Test()
        {
            calculate.numOne = 3;
            calculate.numTwo = 4;

            calculate.clearNumbers();
            Assert.AreEqual(0, calculate.numOne);
            Assert.AreEqual(0, calculate.numTwo);
        }

        [TestMethod]
        public void AddNumbers_Test()
        {
            calculate.numOne = 3;
            calculate.numTwo = 4;

            calculate.AddNumbers();

            Assert.AreEqual(7, calculate.result);
        }

        [TestMethod]
        public void SubtractNumbers_Test()
        {
            calculate.numOne = 3;
            calculate.numTwo = 4;

            calculate.SubtractNumbers();

            Assert.AreEqual(-1, calculate.result);
        }
    }
}