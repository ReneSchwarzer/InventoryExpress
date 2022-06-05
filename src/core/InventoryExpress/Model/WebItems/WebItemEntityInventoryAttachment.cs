using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Attribut
    /// </summary>
    public class WebItemEntityInventoryAttachment : WebItemEntityMedia
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityInventoryAttachment()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="attachment">Das Attribut</param>
        public WebItemEntityInventoryAttachment(WebItemEntityInventoryAttachment attachment)
            : base(attachment)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="attachment">Das Datenbankobjektes der Anlage</param>
        public WebItemEntityInventoryAttachment(Media attachment)
            : base(attachment)
        {
        }
    }
}
