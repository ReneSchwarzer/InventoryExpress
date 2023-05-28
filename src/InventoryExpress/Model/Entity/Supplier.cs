using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Lieferant
    /// </summary>
    [Table("SUPPLIER")]
    public class Supplier : ItemAddress
    {
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
