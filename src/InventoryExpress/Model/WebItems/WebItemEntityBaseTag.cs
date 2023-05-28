using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityBaseTag : WebItemEntity
    {
        /// <summary>
        /// Die Schlagwörter
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
        /// Erstellt eine Tiefenkopie.
        /// </summary>
        /// <param name="item">Das zu kopierende Objekt</param>
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
