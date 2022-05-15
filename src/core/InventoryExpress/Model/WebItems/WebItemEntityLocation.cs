using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Standort
    /// </summary>
    public class WebItemEntityLocation : WebItemEntityBaseAddress
    {
        /// <summary>
        /// Das Gebäude
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Der Raum innerhalb des Gebäudes
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityLocation()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="location">Das Datenbankobjektes des Standortes</param>
        public WebItemEntityLocation(Location location)
            : base(location)
        {
            Building = location.Building;
            Room = location.Room;
            Uri = ViewModel.GetLocationUri(location.Guid);
        }
    }
}
