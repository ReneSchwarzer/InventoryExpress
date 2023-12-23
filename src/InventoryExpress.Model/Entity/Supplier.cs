using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The supplier.
    /// </summary>
    [Table("SUPPLIER")]
    internal class Supplier : ItemAddress
    {
        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Supplier()
            : base()
        {
        }
    }
}
