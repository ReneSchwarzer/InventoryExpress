using InventoryExpress.Model.Entity;
using System;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Journalparameter
    /// </summary>
    public class WebItemEntityJournalParameter : WebItem
    {
        /// <summary>
        /// Der alte Wert
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// Der neue Wert
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityJournalParameter()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="parameter">Das Datenbankobjekt</param>
        public WebItemEntityJournalParameter(InventoryJournalParameter parameter)
        {
            ID = parameter.Guid;
            OldValue = parameter.OldValue;
            NewValue = parameter.NewValue;
        }
    }
}
