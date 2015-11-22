namespace VendingMachine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Abstraction for class that store single currency coins
    /// </summary>
    public class Wallet : IWallet
    {
        /// <summary>
        /// Internal coins storage
        /// </summary>
        private IDictionary<Coin, Stack<Coin>> _coins;

        /// <summary>
        /// Initializes a new instance of the <see cref="Wallet"/> class.
        /// </summary>
        public Wallet()
        {
            _coins = new Dictionary<Coin, Stack<Coin>>();
        }

        /// <summary>
        /// Calculates coins and their amount in the wallet
        /// </summary>
        /// <returns>Dictionary where key - coin, value - coins amount</returns>
        public IDictionary<Coin, int> GetCoinsAmount()
        {
            return _coins.ToDictionary(pair => pair.Key, pair => pair.Value.Count);
        }

        /// <summary>
        /// Remove coin from the wallet
        /// </summary>
        /// <param name="coin">coin to remove</param>
        /// <returns>removed coin</returns>
        /// <exceprion cref="InvalidOperationException">throws exceprion if specified coins is absent</exceprion>
        public Coin Pop(Coin coin)
        {
            if (!_coins.ContainsKey(coin))
            {
                throw new InvalidOperationException("There are not specified coins");
            }

            return _coins[coin].Pop();
        }

        /// <summary>
        /// Adds coin to the wallet
        /// </summary>
        /// <param name="coin">coin to add</param>
        public void Push(Coin coin)
        {
            if (!_coins.ContainsKey(coin))
            {
                _coins.Add(coin, new Stack<Coin>());
            }

            _coins[coin].Push(coin);
        }

        /// <summary>
        /// Calculates amount of specified coins
        /// </summary>
        /// <param name="coin">coin is required to check</param>
        /// <returns>specified coins amount</returns>
        public int GetAmount(Coin coin)
        {
            if (!_coins.ContainsKey(coin))
            {
                return 0;
            }

            return _coins[coin].Count;
        }
    }
}
