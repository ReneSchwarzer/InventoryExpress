using System;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The attributes of an inventory.
    /// </summary>
    internal partial class InventoryAttribute
    {
        /// <summary>
        /// Returns or sets the id of the inventory.
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Returns or sets the inventory.
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Returns or sets the id of the attribue.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Returns or sets the attribute.
        /// </summary>
        public Attribute Attribute { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The timestamp of the last change.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public InventoryAttribute()
            : base()
        {
        }
    }
}
