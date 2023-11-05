using System;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The template attributes.
    /// </summary>
    public class TemplateAttribute
    {
        /// <summary>
        /// Returns or sets the id of the template.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Returns or sets the id of the attribute.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Returns or sets the template.
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Returns or sets the attribute.
        /// </summary> 
        public Attribute Attribute { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateAttribute()
        {
        }
    }
}
