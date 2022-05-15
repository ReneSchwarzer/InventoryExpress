using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Hersteller
    /// </summary>
    public class WebItemEntityManufacturer : WebItemEntityBaseAddress
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityManufacturer()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="manufacturer">Das Datenbankobjektes des Herstellers</param>
        public WebItemEntityManufacturer(Manufacturer manufacturer)
            : base(manufacturer)
        {
            Uri = ViewModel.GetManufacturerUri(manufacturer.Guid);
        }
    }
}
