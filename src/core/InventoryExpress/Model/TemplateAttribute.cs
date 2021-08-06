using System;

namespace InventoryExpress.Model
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
        /// Der Zeitstempel der Erstellung
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
        /// Konstruktor
        /// </summary>
        public TemplateAttribute()
        {
        }
    }
}
