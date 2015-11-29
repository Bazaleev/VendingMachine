namespace VendingMachine.Tests
{
    using System;
    using System.Globalization;
    using System.Collections.Generic;
    using FakeItEasy;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test for <see cref="SingleCurrencyWallet"/> class
    /// </summary>
    [TestClass]
    public class SingleCurrenctyWalletTests
    {
        [TestMethod, Description("Если используется валюта отличаная от указанной при создании объекта, должно генерироваться исключение")]
        public void ExceptionShouldBeThrownIfUsedUnconsistentCurrency()
        {
            var wallet = GetWallet();
            wallet.Push(new Coin(1));
            wallet.Pop(new Coin(1));

            try
            {
                wallet.Push(new Coin(1, new RegionInfo("UA")));
                Assert.Fail("ArgumentException is expected");
            }
            catch (ArgumentException) { }
        }

        [TestMethod, Description("общая сумма в кошельке должна подсчиываться корректно")]
        public void GetTotalSumShouldProvideValidResults()
        {
            var wallet = GetWallet();

            Assert.AreEqual(0, wallet.CalculateTotalSum());

            wallet.Push(new Coin(1));
            wallet.Pop(new Coin(1));
            Assert.AreEqual(0, wallet.CalculateTotalSum());

            wallet.Push(new Coin(1));
            wallet.Push(new Coin(1));
            wallet.Push(new Coin(2));
            wallet.Push(new Coin(0.1m));
            Assert.AreEqual(4.1m, wallet.CalculateTotalSum());
        }

        [TestMethod, Description("рассчитанная сдача, должна корректно вычитаться из кошелька")]
        public void CalculatedChangeShouldBeWithdrawn()
        {
            var wallet = GetWallet();

            wallet.Push(new Coin(1));
            wallet.Push(new Coin(1));
            wallet.Push(new Coin(2));
            wallet.Push(new Coin(2));

            var fakedCalculator = A.Fake<IChangeCalculator>();
            wallet.ChangeCalculator = fakedCalculator;

            var expectedDictionary = new Dictionary<decimal, int>
            {
                { 1m, 2 },
                { 2m, 1 }
            };

            A.CallTo(() => fakedCalculator.Calculate(A<IDictionary<decimal, int>>._, A<decimal>._))
                .Returns(expectedDictionary);

            var coins = wallet.Withdraw(4);
            Assert.AreEqual(3, coins.Count, "Invalid coins count in resulting collection");
            Assert.AreEqual(2m, wallet.CalculateTotalSum(), "Invalid sum rest in wallet");
        }

        [TestMethod, Description("при некорректных входных данных должны генерироваться соответствующие исключения")]
        public void RespectiveExceptionsShouldBeThrownIfInputDataIsInvalid()
        {
            var wallet = GetWallet();

            try
            {
                wallet.Withdraw(-1);
                Assert.Fail("ArgumentOutOfRangeException is expected");
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                wallet.Withdraw(1);
                Assert.Fail("InvalidOperationException is expected");
            }
            catch (InvalidOperationException) { }
        }

        /// <summary>
        /// Инициализирует класс для тестирования
        /// </summary>
        /// <returns>инициализированный объект</returns>
        private SingleCurrencyWallet GetWallet()
        {
            return new SingleCurrencyWallet();
        }
    }
}
