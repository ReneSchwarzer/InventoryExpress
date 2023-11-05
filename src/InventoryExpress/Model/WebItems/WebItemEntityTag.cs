using InventoryExpress.Model.Entity;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The tag.
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
        /// <param name="tag">The database object of the tag.</param>
        public WebItemEntityTag(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Label;
            Label = tag.Label;
        }
    }
}
