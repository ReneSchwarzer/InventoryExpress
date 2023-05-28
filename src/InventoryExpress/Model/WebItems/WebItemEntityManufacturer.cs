using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Hersteller
    /// </summary>
    public class WebItemEntityManufacturer : WebItemEntityBaseAddress
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityManufacturer()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manufacturer">Das Datenbankobjektes des Herstellers</param>
        public WebItemEntityManufacturer(Manufacturer manufacturer)
            : base(manufacturer)
        {
            Uri = ViewModel.GetManufacturerUri(manufacturer.Guid);
        }
    }
}
