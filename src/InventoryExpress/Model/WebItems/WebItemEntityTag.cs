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
        /// Constructor
        /// </summary>
        public WebItemEntityTag()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tag">Das Datenbankobjektes des Schlagwortes</param>
        public WebItemEntityTag(Tag tag)
        {
            Id = tag.Label;
            Name = tag.Label;
            Label = tag.Label;
        }
    }
}
