using System;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Attribute eines Inventars
    /// </summary>
    public partial class InventoryAttribute
    {
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }

        /// <summary>
        /// Der Wert
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
