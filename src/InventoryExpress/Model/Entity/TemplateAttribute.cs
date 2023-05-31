using System;

namespace InventoryExpress.Model.Entity
{
    public class TemplateAttribute
    {
        /// <summary>
        /// Der Verweis auf die Vorlage
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Der Verweis auf das Attribut
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Verweis auf den Mitarbeiter
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Der Verweis auf die Rolle
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
