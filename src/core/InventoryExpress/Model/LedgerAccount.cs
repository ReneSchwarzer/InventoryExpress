using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    [Table("LEDGERACCOUNT")]
    public class LedgerAccount : ItemTag
    {
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public LedgerAccount()
            : base()
        {
            Inventories = new HashSet<Inventory>();
        }
    }
}
