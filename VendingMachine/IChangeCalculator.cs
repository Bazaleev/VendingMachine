namespace VendingMachine
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a methods for change calculation
    /// </summary>
    public interface IChangeCalculator
    {
        /// <summary>
        /// Calculates change 
        /// </summary>
        /// <param name="moneys">input amount of money - dictionary where key - denomination, value - units amount</param>
        /// <param name="sum">sum that should be composed</param>
        /// <returns>dictionary where key - denomination and value - amount</returns>
        IDictionary<decimal, int> Calculate(IDictionary<decimal, int> money, decimal sum);
    }
}
