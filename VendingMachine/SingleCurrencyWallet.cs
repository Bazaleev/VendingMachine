namespace VendingMachine
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Abstraction for wallet that stores single currency only
    /// </summary>
    public class SingleCurrencyWallet
    {
        /// <summary>
        /// Current currency code
        /// </summary>
        private readonly string _currency;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleCurrencyWallet"/> class.
        /// </summary>
        public SingleCurrencyWallet() : this(RegionInfo.CurrentRegion)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleCurrencyWallet"/> class.
        /// </summary>
        /// <param name="regionInfo">currency region</param>
        /// <exception cref="ArgumentNullException">throws if <paramref name="regionInfo"/> is null</exception>
        public SingleCurrencyWallet(RegionInfo regionInfo)
        {
            _currency = regionInfo.ISOCurrencySymbol;
        }

        /// <summary>
        /// Pull necessary amount of coins from the wallet
        /// </summary>
        /// <param name="sumToWithdraw">sum that should be withdrawn</param>
        /// <returns>pulled coins</returns>
        public ICollection<Coin> Withdraw(decimal sumToWithdraw)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates total sum of money in the wallet
        /// </summary>
        /// <returns>total sum</returns>
        public decimal CalculateTotalSum()
        {
            throw new NotImplementedException();
        }
    }
}
