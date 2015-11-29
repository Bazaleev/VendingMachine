namespace VendingMachine.Tests
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Wallet class tests
    /// </summary>
    [TestClass]
    public class WalletTests
    {
        [TestMethod, Description("при попытке извлечь монету, которой нет в кошельке должно генерироваться исключение")]
        public void UnexistedCoinPopShouldThrownAnException()
        {
            var wallet = new Wallet();

            try
            {
                wallet.Pop(new Coin(1));
                Assert.Fail("InvalidOperationException is expected");
            }
            catch (InvalidOperationException) { }

            wallet.Push(new Coin(2));
            try
            {
                wallet.Pop(new Coin(1));
                Assert.Fail("InvalidOperationException is expected");
            }
            catch (InvalidOperationException) { }

            wallet.Push(new Coin(1, new RegionInfo("UA")));
            try
            {
                wallet.Pop(new Coin(1));
                Assert.Fail("InvalidOperationException is expected");
            }
            catch (InvalidOperationException) { }
        }

        [TestMethod, Description("количество монет должно подсчитываться корректно")]
        public void GetCoinsAmountShouldProvideCorrectResult()
        {
            var wallet = new Wallet();

            var result = wallet.GetCoinsAmount();
            Assert.AreEqual(0, result.Count, "Empty dictionary is expected");

            wallet.Push(new Coin(1));
            result = wallet.GetCoinsAmount();
            Assert.AreEqual(1, result.Count, "Invalid result is provided for single coin");

            wallet.Push(new Coin(1));
            wallet.Push(new Coin(2));
            wallet.Push(new Coin(2));
            wallet.Pop(new Coin(1));

            var expectedCoin = new Coin(1);
            result = wallet.GetCoinsAmount();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[expectedCoin], "Invalid {0} coins amount", expectedCoin);

            expectedCoin = new Coin(2);
            Assert.AreEqual(2, result[expectedCoin], "Invalid {0} coins amount", expectedCoin);
        }
    }
}
