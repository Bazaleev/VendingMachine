namespace VendingMachine
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a base operation with coins
    /// </summary>
    public interface IWallet
    {
        /// <summary>
        /// Adds coin to the wallet
        /// </summary>
        /// <param name="coin">coin to add</param>
        void Push(Coin coin);

        /// <summary>
        /// Remove coin from the wallet
        /// </summary>
        /// <param name="coin">coin to remove</param>
        /// <returns>removed coin</returns>
        Coin Pop(Coin coin);

        /// <summary>
        /// Calculates amount of specified coins
        /// </summary>
        /// <param name="coin">coin is required to check</param>
        /// <returns>specified coins amount</returns>
        int GetAmount(Coin coin);

        /// <summary>
        /// Calculates coins and their amount in the wallet
        /// </summary>
        /// <returns>Dictionary where key - coin, value - coins amount</returns>
        IDictionary<Coin, int> GetCoinsAmount();
    }
}
