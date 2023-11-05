using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The state.
    /// </summary>
    public class Condition : Item
    {
        /// <summary>
        /// Returns or sets the state as a note.
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Condition()
            : base()
        {
        }
    }
}
