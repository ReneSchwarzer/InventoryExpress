using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Attribut
    /// </summary>
    public class WebItemEntityAttribute : WebItemEntity
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityAttribute()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="attribute">Das Datenbankobjektes des Attributs</param>
        public WebItemEntityAttribute(Attribute attribute)
            : base(attribute)
        {
            Uri = ViewModel.GetAttributeUri(attribute.Guid);
        }
    }
}
