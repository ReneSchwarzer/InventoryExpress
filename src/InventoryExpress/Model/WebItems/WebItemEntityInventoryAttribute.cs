using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The attribute.
    /// </summary>
    public class WebItemEntityInventoryAttribute : WebItemEntityAttribute
    {
        /// <summary>
        /// Returns or sets the value.
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
        /// <param name="attribute">The attribute.</param>
        public WebItemEntityInventoryAttribute(WebItemEntityInventoryAttribute attribute)
            : base(attribute)
        {
            Value = attribute.Value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public WebItemEntityInventoryAttribute(WebItemEntityAttribute attribute)
            : base(attribute)
        {
            Value = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attribute">The database object of the inventory attribute.</param>
        /// <param name="attribute">The database object of the attribute.</param>
        public WebItemEntityInventoryAttribute(InventoryAttribute inventoryAttribute, Attribute attribute)
            : base(attribute)
        {
            Value = inventoryAttribute?.Value;
            Created = inventoryAttribute != null ? inventoryAttribute.Created : System.DateTime.Now;
        }
    }
}
