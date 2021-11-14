using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    public class LedgerAccount : ItemTag
    {
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public LedgerAccount()
            : base()
        {
        }
    }
}
