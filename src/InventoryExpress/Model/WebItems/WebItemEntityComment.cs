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
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// The timestamp of the last change.
        /// </summary>
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityComment()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="comment">Das Datenbakobjekt mit dem Kommentar</param>
        public WebItemEntityComment(InventoryComment comment)
        {
            Id = comment.Guid;
            Comment = comment.Comment;
            Created = comment.Created;
            Updated = comment.Updated;
        }
    }
}
