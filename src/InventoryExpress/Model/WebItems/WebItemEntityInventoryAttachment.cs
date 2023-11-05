using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The inventory attatchment.
    /// </summary>
    public class WebItemEntityInventoryAttachment : WebItemEntityMedia
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityInventoryAttachment()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        public WebItemEntityInventoryAttachment(WebItemEntityInventoryAttachment attachment)
            : base(attachment)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attachment">The database object of the attatchment.</param>
        public WebItemEntityInventoryAttachment(Media attachment)
            : base(attachment)
        {
        }
    }
}
