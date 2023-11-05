using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The manufacturer.
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
        /// <param name="manufacturer">The database object of the manufacturer.</param>
        public WebItemEntityManufacturer(Manufacturer manufacturer)
            : base(manufacturer)
        {
            Uri = ViewModel.GetManufacturerUri(manufacturer.Guid);
        }
    }
}
