using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;
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
        [JsonPropertyName("oldvalue")]
        public string OldValue { get; set; }

        /// <summary>
        /// Der neue Wert
        /// </summary>
        [JsonPropertyName("newvalue")]
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
            Id = parameter.Guid;
            Name = parameter.Name;
            OldValue = parameter.OldValue;
            NewValue = parameter.NewValue;
        }
    }
}
