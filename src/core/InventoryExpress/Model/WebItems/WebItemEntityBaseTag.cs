using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityBaseTag : WebItemEntity
    {
        /// <summary>
        /// Die Schlagwörter
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityBaseTag()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das Datenbankobjektes des Herstellers</param>
        public WebItemEntityBaseTag(ItemTag item)
            :base(item)
        {
            Tag = item.Tag;
        }
    }
}
