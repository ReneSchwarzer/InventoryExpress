using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Journalparameter eines Inventars
    /// </summary>
    public partial class InventoryJournalParameter
    {
        /// <summary>
        /// Die ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Das InventoryJournal
        /// </summary>
        public int InventoryJournalId { get; set; }

        /// <summary>
        /// Der Parametername
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Der alte Wert
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// Der neue Wert
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Die GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Verweis auf das Journal
        /// </summary>
        public virtual InventoryJournal InventoryJournal { get; set; }
    }
}
