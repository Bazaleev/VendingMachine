namespace VendingMachine
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Abstraction that represents coins
    /// </summary>
    public struct Coin : IEquatable<Coin>
    {
        /// <summary>
        /// Current currency symbol
        /// </summary>
        private string _currencySymbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="Coin"/> struct.
        /// </summary>
        /// <param name="denomination">denomination of current coin</param>
        /// <exception cref="ArgumentOutOfRangeException">throws if denomination non positive</exception>
        /// <exception cref="ArgumentNullException">throws if <paramref name="region" /> is null</exception>
        public Coin(decimal denomination) : this(denomination, RegionInfo.CurrentRegion)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coin"/> struct.
        /// </summary>
        /// <param name="denomination">denomination of current coin</param>
        /// <param name="region">culture that represents country coin belongs to</param>
        /// <exception cref="ArgumentOutOfRangeException">throws if denomination non positive</exception>
        /// <exception cref="ArgumentNullException">throws if <paramref name="region" /> is null</exception>
        public Coin(decimal denomination, RegionInfo region)
        {
            if (region == null)
            {
                throw new ArgumentNullException("region");
            }

            if (denomination <= 0)
            {
                throw new ArgumentOutOfRangeException("denomination");
            }

            Denomination = denomination;
            Currency = region.ISOCurrencySymbol;
            _currencySymbol = region.CurrencySymbol;
        }

        /// <summary>
        /// Gets the coins denomination
        /// </summary>
        public decimal Denomination { get; private set; }

        /// <summary>
        /// Gets the three-character ISO 4217 currency symbol associated with the country/region.
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Checks are object equal
        /// </summary>
        /// <param name="first">first object to compare</param>
        /// <param name="second">second object to compare</param>
        /// <returns>true if objects are equal otherwise - false</returns>
        public static bool operator ==(Coin first, Coin second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Checks objects for inequality
        /// </summary>
        /// <param name="first">first object to compare</param>
        /// <param name="second">second object to compare</param>
        /// <returns>true if object aren't equal, false - otherwise</returns>
        public static bool operator !=(Coin first, Coin second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Determines the equality of instances
        /// </summary>
        /// <param name="other">other object to compare</param>
        /// <returns>true is objects are equal, otherwise - false</returns>
        public bool Equals(Coin other)
        {
            return Denomination == other.Denomination &&
                Currency == other.Currency;
        }

        /// <summary>
        /// Determines the equality
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>true if objects are equal, otherwise - false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj is Coin ? this.Equals((Coin)obj) : false;
        }

        /// <summary>
        /// Calculates the hash of current object
        /// </summary>
        /// <returns>hash value</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = Denomination.GetHashCode();
                result = (result * 397) ^ (Currency == null ? 0 : Currency.GetHashCode());

                return result;
            }
        }

        /// <summary>
        /// Returns object's string representation
        /// </summary>
        /// <returns>object's string representation</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0} {1}", _currencySymbol, Denomination);
        }
    }
}
