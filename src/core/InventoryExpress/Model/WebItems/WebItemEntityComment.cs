using InventoryExpress.Model.Entity;
using System;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Journal
    /// </summary>
    public class WebItemEntityComment : WebItem
    {
        /// <summary>
        /// Liefert oder setzt den Kommentar
        /// </summary>
        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityComment()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="comment">Das Datenbakobjekt mit dem Kommentar</param>
        public WebItemEntityComment(InventoryComment comment)
        {
            ID = comment.Guid;
            Comment = comment.Comment;
            Created = comment.Created;
            Updated = comment.Updated;
        }
    }
}
