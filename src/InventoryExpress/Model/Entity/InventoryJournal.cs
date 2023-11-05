using System;
using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The journal of an inventory.
    /// </summary>
    public partial class InventoryJournal
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the inventory id.
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Returns or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Returns or sets the guid.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Returns or sets the reference to the associated inventory item.
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Returns or sets the reference to the parameters.
        /// </summary>
        public virtual ICollection<InventoryJournalParameter> InventoryJournalParameters { get; set; }
    }
}
