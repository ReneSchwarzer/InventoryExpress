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
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
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
