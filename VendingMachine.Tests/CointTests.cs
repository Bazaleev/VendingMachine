namespace VendingMachine.Tests
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CointTests
    {
        [TestMethod, Description("Если входные параметры не корректны должны генерироваться соответствюущие исключения")]
        public void ProperExceptionShouldBeThrownIfArgumentsArentValid()
        {
            try
            {
                new Coin(1, null);
                Assert.Fail("ArgumentNullException is expected");
            }
            catch (ArgumentNullException) { }

            try
            {
                new Coin(-1, RegionInfo.CurrentRegion);
                Assert.Fail("ArgumentOutOfRangeException is expected for negative values");
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                new Coin(0);
                Assert.Fail("ArgumentOutOfRangeException is expected if denomination is zero");
            }
            catch (ArgumentOutOfRangeException) { }
        }

        [TestMethod, Description("Свойства объекта должны корректно присваиваться в конструкторе")]
        public void PropertyShouldBeAssigned()
        {
            var regionInfo = new RegionInfo("RU-ru");
            var expectedDenomination = 1.2m;
            var coin = new Coin(expectedDenomination, regionInfo);

            Assert.AreEqual(expectedDenomination, coin.Denomination);
            Assert.AreEqual(regionInfo.ISOCurrencySymbol, coin.Currency);
        }

        [TestMethod, Description("равенство и неравенство должны проверяться корректно")]
        public void EqualityAndInequalityShouldBeCheckedCorrectly()
        {
            var coin1 = new Coin(1);
            var coin2 = new Coin(1);

            Assert.IsTrue(coin1 == coin2);

            var coin3 = new Coin(2);
            Assert.IsTrue(coin1 != coin3);
        }
    }
}
