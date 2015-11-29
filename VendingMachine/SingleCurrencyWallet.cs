namespace VendingMachine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Abstraction for wallet that stores single currency only
    /// </summary>
    public class SingleCurrencyWallet : Wallet, ISingleCurrencyWallet
    {
        /// <summary>
        /// Current currency code
        /// </summary>
        private readonly string _currency;

        /// <summary>
        /// Currency region
        /// </summary>
        private readonly RegionInfo _regionInfo;

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
        /// <exception cref="ArgumentNullException">is thrown if <paramref name="regionInfo"/> is null</exception>
        public SingleCurrencyWallet(RegionInfo regionInfo)
        {
            if (regionInfo == null)
            {
                throw new ArgumentNullException("regionInfo");
            }

            _currency = regionInfo.ISOCurrencySymbol;
            _regionInfo = regionInfo;
        }

        /// <summary>
        /// Gets or sets object that calculates change
        /// </summary>
        public IChangeCalculator ChangeCalculator { get; set; }

        /// <summary>
        /// Adds coin to the wallet
        /// </summary>
        /// <param name="coin">coin to add</param>
        /// <exception cref="ArgumentException">is thrown if unsupported coin currency is provided</exception>
        public override void Push(Coin coin)
        {
            if (coin.Currency != _currency)
            {
                throw new ArgumentException("Provided coins currency isn't supported", "coin");
            }

            base.Push(coin);
        }

        /// <summary>
        /// Pull necessary amount of coins from the wallet
        /// </summary>
        /// <param name="sumToWithdraw">sum that should be withdrawn</param>
        /// <returns>pulled coins</returns>
        /// <exception cref="ArgumentOutOfRangeException">is thrown if <paramref name="sumToWithdraw" /> - null</exception>
        /// <exception cref="InvalidOperationException">is thrown if ChangeCalculator isn't provided</exception>
        public ICollection<Coin> Withdraw(decimal sumToWithdraw)
        {
            if (sumToWithdraw == 0)
            {
                return new Coin[0];
            }

            if (sumToWithdraw < 0)
            {
                throw new ArgumentOutOfRangeException("sumToWithdraw");
            }

            if (sumToWithdraw > CalculateTotalSum())
            {
                // ToDo introduce specific exception
                throw new InvalidOperationException("Not enough money");
            }

            var coins = GetCoinsToWithdraw(sumToWithdraw);
            if (coins.Count == 0)
            {
                // ToDo use specific exception
                throw new InvalidOperationException("It is impossible to calculate a change");
            }

            foreach (var coin in coins)
            {
                Pop(coin);
            }

            return coins;
        }

        /// <summary>
        /// Calculates total sum of money in the wallet
        /// </summary>
        /// <returns>total sum</returns>
        public decimal CalculateTotalSum()
        {
            var coins = GetCoinsAmount();

            return coins.Select(p => p.Key.Denomination * p.Value).Sum();
        }

        /// <summary>
        /// Get coins that should be pop from wallet
        /// </summary>
        /// <param name="sumToWithdraw">sum of money that should be withdrawn</param>
        /// <returns>list of coins to withdraw</returns>
        /// <exception cref="InvalidOperationException">is thrown if failed to calculate change </exception>
        private ICollection<Coin> GetCoinsToWithdraw(decimal sumToWithdraw)
        {
            Contract.Requires(sumToWithdraw > 0);
            Contract.Ensures(Contract.Result<ICollection<Coin>>() != null);

            if (ChangeCalculator == null)
            {
                throw new InvalidOperationException("Change calculator isn't provided");
            }

            var coins = GetCoinsAmount();
            var denominationsAndTheirAmount = coins.ToDictionary(c => c.Key.Denomination, c => c.Value);
            var change = ChangeCalculator.Calculate(denominationsAndTheirAmount, sumToWithdraw);

            var result = new List<Coin>(change.Values.Sum());
            foreach (var denomination in change.Keys)
            {
                var amount = change[denomination];
                for (int i = 0; i < amount; i++)
                {
                    result.Add(new Coin(denomination, _regionInfo));
                }
            }

            return result;
        }
    }
}
