namespace VendingMachine
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines methods for wallet that store single currency only
    /// </summary>
    public interface ISingleCurrencyWallet : IWallet
    {
        /// <summary>
        /// Calculates total sum of money in the wallet
        /// </summary>
        /// <returns>total sum</returns>
        decimal CalculateTotalSum();

        /// <summary>
        /// Pull necessary amount of coins from the wallet
        /// </summary>
        /// <param name="sumToWithdraw">sum that should be withdrawn</param>
        /// <returns>pulled coins</returns>
        ICollection<Coin> Withdraw(decimal sumToWithdraw);
    }
}
