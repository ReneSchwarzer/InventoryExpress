using InventoryExpress.Model.Entity;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Standort
    /// </summary>
    public class WebItemEntityTag : WebItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityTag()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="tag">Das Datenbankobjektes des Schlagwortes</param>
        public WebItemEntityTag(Tag tag)
        {
            ID = tag.Label;
            Name = tag.Label;
            Label = tag.Label;
        }
    }
}
