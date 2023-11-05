using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityBaseTag : WebItemEntity
    {
        /// <summary>
        /// Returns or sets the tags.
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityBaseTag()
        {

        }

        /// <summary>
        /// Copy-Konstruktor
        /// Creates a deep copy.
        /// </summary>
        /// <param name="item">The object to be copied.</param>
        public WebItemEntityBaseTag(WebItemEntityBaseTag item)
            : base(item)
        {
            Tag = item.Tag;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Das Datenbankobjektes des Herstellers</param>
        public WebItemEntityBaseTag(ItemTag item)
            : base(item)
        {
            Tag = item.Tag;
        }
    }
}
