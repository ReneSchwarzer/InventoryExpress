using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The manufacturer.
    /// </summary>
    public class Manufacturer : ItemAddress
    {
        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Manufacturer()
            : base()
        {
        }
    }
}
