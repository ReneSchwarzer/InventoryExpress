using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Journal
    /// </summary>
    public class WebItemEntityJournal : WebItem
    {
        /// <summary>
        /// Die Aktion
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Verweis auf die Parameter
        /// </summary>
        public virtual IEnumerable<WebItemEntityJournalParameter> Parameters { get; set; } = new List<WebItemEntityJournalParameter>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityJournal()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityJournal(params WebItemEntityJournalParameter[] paramertes)
            :this()
        {
            Parameters = new List<WebItemEntityJournalParameter>(paramertes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="journal">Das Datenbankobjekt</param>
        public WebItemEntityJournal(InventoryJournal journal)
        {
            Id = journal.Guid;
            Action = journal.Action;
            Created = journal.Created;
        }
    }
}
