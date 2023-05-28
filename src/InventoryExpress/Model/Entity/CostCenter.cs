using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Kostenstelle
    /// </summary>
    public class CostCenter : ItemTag
    {
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
