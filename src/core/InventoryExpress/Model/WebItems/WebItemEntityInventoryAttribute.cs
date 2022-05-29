using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Attribut
    /// </summary>
    public class WebItemEntityInventoryAttribute : WebItemEntityAttribute
    {
        /// <summary>
        /// Der Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityInventoryAttribute()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="attribute">Das Datenbankobjektes des Inventarattributes</param>
        /// <param name="attribute">Das Datenbankobjektes des Attributs</param>
        public WebItemEntityInventoryAttribute(InventoryAttribute inventoryAttribute, Attribute attribute)
            : base(attribute)
        {
            Value = inventoryAttribute.Value;
            Created = inventoryAttribute.Created;
        }
    }
}
