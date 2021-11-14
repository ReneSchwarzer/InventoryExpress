using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Zustand
    /// </summary>
    public class Condition : Item
    {
        /// <summary>
        /// Zustand als Note
        /// </summary>
        public int Grade { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Condition()
            : base()
        {
        }
    }
}
