using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The location.
    /// </summary>
    internal class Location : ItemAddress
    {
        /// <summary>
        /// Returns or sets the building.
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Returns or sets the room inside the building.
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
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
