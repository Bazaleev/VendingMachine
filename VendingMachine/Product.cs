namespace VendingMachine
{
    /// <summary>
    /// Abstraction of vending machine products
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets unique product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets product's category
        /// </summary>
        public string CategoryId { get; set; }
    }
}
