using InventoryExpress.Model.Entity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Vorlage
    /// </summary>
    public class WebItemEntityTemplate : WebItemEntityBaseTag
    {
        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        [JsonPropertyName("attributes")]
        public IEnumerable<WebItemEntityAttribute> Attributes { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityTemplate()
        {
            Attributes = new List<WebItemEntityAttribute>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="template">Das Datenbankobjektes der Vorlage</param>
        public WebItemEntityTemplate(Template template)
            : base(template)
        {
            Uri = ViewModel.GetTemplateUri(template.Guid);
            Attributes = ViewModel.GetAttributes(this);
        }
    }
}
