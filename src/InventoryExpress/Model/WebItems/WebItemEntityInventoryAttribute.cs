using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityInventoryAttribute()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        public WebItemEntityInventoryAttribute(WebItemEntityInventoryAttribute attribute)
            : base(attribute)
        {
            Value = attribute.Value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attribute">Das Attribut</param>
        public WebItemEntityInventoryAttribute(WebItemEntityAttribute attribute)
            : base(attribute)
        {
            Value = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attribute">Das Datenbankobjektes des Inventarattributes</param>
        /// <param name="attribute">Das Datenbankobjektes des Attributs</param>
        public WebItemEntityInventoryAttribute(InventoryAttribute inventoryAttribute, Attribute attribute)
            : base(attribute)
        {
            Value = inventoryAttribute?.Value;
            Created = inventoryAttribute != null ? inventoryAttribute.Created : System.DateTime.Now;
        }
    }
}
