using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The journal.
    /// </summary>
    public class WebItemEntityJournal : WebItem
    {
        /// <summary>
        /// Returns or sets the action.
        /// </summary>
        [JsonPropertyName("action")]
        public string Action { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Returns or sets the reference to the parameters.
        /// </summary>
        public virtual IEnumerable<WebItemEntityJournalParameter> Parameters { get; set; } = new List<WebItemEntityJournalParameter>();

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityJournal()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityJournal(params WebItemEntityJournalParameter[] paramertes)
            : this()
        {
            Parameters = new List<WebItemEntityJournalParameter>(paramertes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="journal">The database object of the journal.</param>
        internal WebItemEntityJournal(InventoryJournal journal)
        {
            Id = journal.Id;
            Guid = journal.Guid;
            Action = journal.Action;
            Created = journal.Created;
        }
    }
}
