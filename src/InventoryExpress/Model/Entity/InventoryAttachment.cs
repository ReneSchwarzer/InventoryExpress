using System;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// File attachments of an inventory item.
    /// </summary>
    public partial class InventoryAttachment
    {
        /// <summary>
        /// The id of the inventory item.
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Returns or sets the id of the media.
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Returns or sets the inventory.
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Returns or sets the media.
        /// </summary>
        public virtual Media Media { get; set; }
    }
}
