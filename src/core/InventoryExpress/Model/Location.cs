using System.Collections.Generic;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Standort
    /// </summary>
    public class Location : ItemAaddress
    {
        /// <summary>
        /// Das Gebäude
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Der Raum innerhalb des Gebäudes
        /// </summary>
        public string Room { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Location()
            : base()
        {
        }
    }
}
