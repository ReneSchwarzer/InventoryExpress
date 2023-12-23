namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The assignment of the tag to the inventory items.
    /// </summary>
    internal partial class InventoryTag
    {
        /// <summary>
        /// Returns or sets the id of the inventory.
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Returns or sets the id of the tag.
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Returns or sets the inventory.
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Returns or sets the tag.
        /// </summary>
        public virtual Tag Tag { get; set; }
    }
}
