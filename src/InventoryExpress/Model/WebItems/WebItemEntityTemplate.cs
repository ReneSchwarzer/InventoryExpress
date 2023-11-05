using InventoryExpress.Model.Entity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The template.
    /// </summary>
    public class WebItemEntityTemplate : WebItemEntityBaseTag
    {
        /// <summary>
        /// Returns or sets the attributes.
        /// </summary>
        [JsonPropertyName("attributes")]
        public IEnumerable<WebItemEntityAttribute> Attributes { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityTemplate()
        {
            Attributes = new List<WebItemEntityAttribute>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="template">The database object of the template.</param>
        public WebItemEntityTemplate(Template template)
            : base(template)
        {
            Uri = ViewModel.GetTemplateUri(template.Guid);
            Attributes = ViewModel.GetAttributes(this);
        }
    }
}
