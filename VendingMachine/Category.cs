namespace VendingMachine
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Describes product category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Category" /> class
        /// </summary>
        public Category() : this(RegionInfo.CurrentRegion)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="Category" /> class
        /// </summary>
        /// <param name="region">currency region information</param>
        public Category(RegionInfo region)
        {
            PriceCurrency = region.ISOCurrencySymbol;
        }

        /// <summary>
        /// Gets or sets categories id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets used currency (ISO 4217 format)
        /// </summary>
        public string PriceCurrency { get; set; }
    }
}
