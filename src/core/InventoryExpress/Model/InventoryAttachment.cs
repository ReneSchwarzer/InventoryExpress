using System;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Datei-Anlagen eines Inventargegenstandes
    /// </summary>
    public partial class InventoryAttachment
    {
        public int InventoryId { get; set; }

        public int MediaId { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        public virtual Media Media { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
