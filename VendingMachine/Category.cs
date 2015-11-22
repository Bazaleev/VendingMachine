namespace VendingMachine
{
    /// <summary>
    /// Describes product category
    /// </summary>
    public class Category
    {
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
