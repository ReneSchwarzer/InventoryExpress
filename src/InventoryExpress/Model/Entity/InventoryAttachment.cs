using System;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Datei-Anlagen eines Inventargegenstandes
    /// </summary>
    public partial class InventoryAttachment
    {
        /// <summary>
        /// The id of the inventory item.
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// VDie Id des Dokumented
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Verweis auf dem Inventargegenstand
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Verweis auf das Dokument
        /// </summary>
        public virtual Media Media { get; set; }
    }
}
