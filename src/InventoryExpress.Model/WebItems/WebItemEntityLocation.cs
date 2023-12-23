using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The location.
    /// </summary>
    public class WebItemEntityLocation : WebItemEntityBaseAddress
    {
        /// <summary>
        /// Returns or sets the building.
        /// </summary>
        [JsonPropertyName("building")]
        public string Building { get; set; }

        /// <summary>
        /// Returns or sets the room inside the building.
        /// </summary>
        [JsonPropertyName("room")]
        public string Room { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityLocation()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">The database object of the location.</param>
        internal WebItemEntityLocation(Location location)
            : base(location)
        {
            Building = location.Building;
            Room = location.Room;
        }
    }
}
