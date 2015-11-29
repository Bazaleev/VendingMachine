namespace VendingMachine.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test ChangeCalculator
    /// </summary>
    [TestClass]
    public class ChangeCalculatorTests
    {
        [TestMethod, Description("пустой словарь должне возвращаться если невозможно рассчитать сдачу")]
        public void EmptyDictionaryShouldBeReturnedIsCalculationIsImpossible()
        {
            var input = new Dictionary<decimal, int>();

            var calculator = new ChangeCalculator();
            var result = calculator.Calculate(input, 0);
            Assert.AreEqual(0, result.Count, "Empty dictionary is expected if sum is 0");

            input.Add(10m, 1);
            result = calculator.Calculate(input, 11);
            Assert.AreEqual(0, result.Count, "Empty dictionary is expected if not enough values");

            input.Add(2m, 10);
            result = calculator.Calculate(input, 11);
            Assert.AreEqual(0, result.Count, "Empty dictionary is expected if it is impossible to calc a change");
        }

        [TestMethod, Description("сумма сдачи должна рассчитыаться корректно")]
        public void ValidChangeSumShouldBeCalculate()
        {

            var input = new Dictionary<decimal, int>();
            input.Add(10m, 1);

            var calculator = new ChangeCalculator();
            var result = calculator.Calculate(input, 10);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[10m]);

            input.Add(5m, 10);
            input.Add(2m, 10);
            input.Add(1m, 10);

            result = calculator.Calculate(input, 18);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(1, result[10m]);
            Assert.AreEqual(1, result[5m]);
            Assert.AreEqual(1, result[2m]);
            Assert.AreEqual(1, result[1m]);

            input = new Dictionary<decimal, int>
            {
                { 10m, 1 },
                { 1m, 9 },
                { 0.1m, 10 }
            };

            result = calculator.Calculate(input, 9.4m);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(9, result[1m]);
            Assert.AreEqual(4, result[0.1m]);

            input = new Dictionary<decimal, int>
            {
                { 10m, 10 },
                { 5m, 9 },
                { 3m, 10 }
            };

            result = calculator.Calculate(input, 18m);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[3m]);
            Assert.AreEqual(1, result[5m]);
            Assert.AreEqual(1, result[10m]);

            input = new Dictionary<decimal, int>
            {
                { 10m, 10 },
                { 4m, 9 },
                { 3m, 10 }
            };

            result = calculator.Calculate(input, 15m);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[3m]);
            Assert.AreEqual(3, result[4m]);
        }

        [TestMethod, Description("примерный тест на призводительность")]
        public void PerformanceTest()
        {
            var input = new Dictionary<decimal, int>();
            input.Add(0.1m, Int32.MaxValue);
            input.Add(0.5m, Int32.MaxValue);
            input.Add(1m, Int32.MaxValue);
            input.Add(2m, Int32.MaxValue);
            input.Add(3m, Int32.MaxValue);
            input.Add(5m, Int32.MaxValue);
            input.Add(7m, Int32.MaxValue);
            input.Add(10m, Int32.MaxValue);

            var calculator = new ChangeCalculator();
            var result = calculator.Calculate(input, Int32.MaxValue);
        }
    }
}
