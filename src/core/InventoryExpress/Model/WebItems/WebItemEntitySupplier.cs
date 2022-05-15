using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Lieferant
    /// </summary>
    public class WebItemEntitySupplier : WebItemEntityBaseAddress
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntitySupplier()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="supplier">Das Datenbankobjektes des Lieferanten</param>
        public WebItemEntitySupplier(Supplier supplier)
            : base(supplier)
        {
            Uri = ViewModel.GetSupplierUri(supplier.Guid);
        }
    }
}
