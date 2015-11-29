namespace VendingMachine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for change caluclation that uses Best first search
    /// </summary>
    public class ChangeCalculator : IChangeCalculator
    {
        /// <summary>
        /// Helper class for operating on money
        /// </summary>
        private class Balance
        {
            private readonly IDictionary<decimal, int> _money;

            /// <summary>
            /// Initializes a new instance of <see cref="Balance"/> class
            /// </summary>
            /// <param name="money">dictionary with denomination as keys and amount as values</param>
            public Balance(IDictionary<decimal, int> money)
            {
                _money = money.OrderByDescending(pair => pair.Key)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
            }

            /// <summary>
            /// Gets list of available denominations
            /// </summary>
            /// <returns>list of available denominations</returns>
            public ICollection<decimal> GetDenominations()
            {
                return _money.Where(pair => pair.Value > 0).Select(pair => pair.Key).ToList();
            }

            /// <summary>
            /// Adds denomination to queue
            /// </summary>
            /// <param name="denomination">denomination to add</param>
            public void Push(decimal denomination)
            {
                _money[denomination]++;
            }

            /// <summary>
            /// Pops the same denomination or less if there is no more this denomination
            /// </summary>
            /// <param name="denomination">specified denomination or less then that</param>
            /// <returns>found denomiation or zero</returns>
            public decimal PopTheSameOrLess(decimal denomination)
            {
                if (_money.ContainsKey(denomination) && _money[denomination] > 0)
                {
                    _money[denomination]--;
                    return denomination;
                }

                return PopLessThat(denomination);
            }

            /// <summary>
            /// Pops first available denomination that is less then specified
            /// </summary>
            /// <param name="denomination">denomination to look for</param>
            /// <returns>found denomination or zero</returns>
            public decimal PopLessThat(decimal denomination)
            {
                var nextDenomination = _money
                    .Where(pair => pair.Key < denomination)
                    .Where(pair => pair.Value > 0)
                    .Select(pair => pair.Key).
                    FirstOrDefault();

                if (nextDenomination == 0)
                {
                    return nextDenomination;
                }

                _money[nextDenomination]++;
                return nextDenomination;
            }

            /// <summary>
            /// Gets the maximum available denomination
            /// </summary>
            /// <returns>maximum available denomination or 0</returns>
            public decimal GetMaxAvailable()
            {
                return GetDenominations().FirstOrDefault();
            }
        }

        /// <summary>
        /// Calculates change 
        /// </summary>
        /// <param name="money">input amount of money - dictionary where key - denomination, value - units amount</param>
        /// <param name="sum">sum that should be composed</param>
        /// <returns>dictionary where key - denomination and value - amount</returns>
        public IDictionary<decimal, int> Calculate(IDictionary<decimal, int> money, decimal sum)
        {
            if (money == null)
            {
                throw new ArgumentNullException("money");
            }

            var balance = new Balance(money);
            var result = new Stack<decimal>();
            var currentSum = 0m;
            var denomination = balance.PopTheSameOrLess(balance.GetMaxAvailable());

            while (currentSum != sum)
            {
                if (denomination == 0 && result.Count == 0)
                {
                    break;
                }

                if (currentSum + denomination <= sum && denomination > 0)
                {
                    result.Push(denomination);
                    currentSum += denomination;
                    denomination = balance.PopTheSameOrLess(denomination);
                }
                else if (denomination > 0)
                {
                    balance.Push(denomination);
                    denomination = balance.PopLessThat(denomination);
                }
                else
                {
                    while (denomination == 0 && result.Count > 0)
                    {
                        var previous = result.Pop();
                        currentSum -= previous;
                        balance.Push(previous);
                        denomination = balance.PopLessThat(previous);
                    }
                }
            }

            return result.GroupBy(item => item).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
