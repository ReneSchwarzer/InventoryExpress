using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Lieferant
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
        /// <param name="supplier">Das Datenbankobjektes des Lieferanten</param>
        public WebItemEntitySupplier(Supplier supplier)
            : base(supplier)
        {
            Uri = ViewModel.GetSupplierUri(supplier.Guid);
        }
    }
}
