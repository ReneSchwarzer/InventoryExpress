using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Zustand
    /// </summary>
    [Table("CONDITION")]
    public class Condition : Item
    {
        /// <summary>
        /// Zustand als Note
        /// </summary>
        [Column("GRADE")]
        public int Grade { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Condition()
            : base()
        {
            Inventories = new HashSet<Inventory>();
        }
    }
}
