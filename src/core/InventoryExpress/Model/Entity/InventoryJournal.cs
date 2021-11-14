using System;
using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Journal eines Inventars
    /// </summary>
    public partial class InventoryJournal
    {
        /// <summary>
        /// Die ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Das Inventar
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Die Aktion
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Die GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Verweis auf den zugehörige Inventargegenstand
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Verweis auf die Parameter
        /// </summary>
        public virtual ICollection<InventoryJournalParameter> InventoryJournalParameters { get; set; }
    }
}
