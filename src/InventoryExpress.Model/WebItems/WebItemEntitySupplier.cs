using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The supplier.
    /// </summary>
    public class WebItemEntitySupplier : WebItemEntityBaseAddress
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntitySupplier()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="supplier">The database object of the supplier.</param>
        internal WebItemEntitySupplier(Supplier supplier)
            : base(supplier)
        {
        }
    }
}
