using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The ledger account.
    /// </summary>
    internal class LedgerAccount : ItemTag
    {
        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LedgerAccount()
            : base()
        {
        }
    }
}
