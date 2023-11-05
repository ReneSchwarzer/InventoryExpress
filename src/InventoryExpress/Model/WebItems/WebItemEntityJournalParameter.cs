using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The journal parameter.
    /// </summary>
    public class WebItemEntityJournalParameter : WebItem
    {
        /// <summary>
        /// Returns or sets the old value.
        /// </summary>
        [JsonPropertyName("oldvalue")]
        public string OldValue { get; set; }

        /// <summary>
        /// Returns or sets the new value.
        /// </summary>
        [JsonPropertyName("newvalue")]
        public string NewValue { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityJournalParameter()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameter">The database object of the journal parameter.</param>
        public WebItemEntityJournalParameter(InventoryJournalParameter parameter)
        {
            Id = parameter.Id;
            Guid = parameter.Guid;
            Name = parameter.Name;
            OldValue = parameter.OldValue;
            NewValue = parameter.NewValue;
        }
    }
}
