using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The cost center.
    /// </summary>
    internal class CostCenter : ItemTag
    {
        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenter()
            : base()
        {
        }
    }
}
