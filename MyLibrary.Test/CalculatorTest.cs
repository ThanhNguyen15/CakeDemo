using System;
using Xunit;

namespace MyLibrary.Test
{
    public class CalculatorTest
    {
        private readonly Calculator _calculator;
        public CalculatorTest()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [InlineData(5, 4, 9)]
        [InlineData(100, 200, 300)]
        [InlineData(346, 4, 350)]
        public void Add_ShouldWork(double x, double y, double expected)
        {
            double actual = _calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(20, 10, 10)]
        [InlineData(500, 200, 300)]
        [InlineData(354, 4, 350)]
        public void Subtract_ShouldWork(double x, double y, double expected)
        {
            double actual = _calculator.Subtrac(x, y);
            Assert.Equal(expected, actual);
        }
    }
}
