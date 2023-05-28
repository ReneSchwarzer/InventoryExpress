using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Standort
    /// </summary>
    public class Location : ItemAddress
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
        /// Constructor
        /// </summary>
        public Location()
            : base()
        {
        }
    }
}
