using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Kostenstelle
    /// </summary>
    [Table("COSTCENTER")]
    public class CostCenter : ItemTag
    {
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CostCenter()
            : base()
        {
            Inventories = new HashSet<Inventory>();
        }
    }
}
