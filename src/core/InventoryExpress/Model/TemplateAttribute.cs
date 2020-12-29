using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    [Table("TEMPLATEATTRIBUTE")]
    public class TemplateAttribute
    {
        /// <summary>
        /// Der Verweis auf die Vorlage
        /// </summary>
        [Key, Column("TEMPLATEID", Order = 0)]
        public int TemplateId { get; set; }

        /// <summary>
        /// Der Verweis auf das Attribut
        /// </summary>
        [Key, Column("ATTRIBUTEID", Order = 1)]
        public int AttributeId { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [Column("CREATED")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Verweis auf den Mitarbeiter
        /// </summary>
        [ForeignKey("TemplateId")]
        public virtual Template Template { get; set; }

        /// <summary>
        /// Der Verweis auf die Rolle
        /// </summary> 
        [ForeignKey("AttributeId")]
        public virtual Attribute Attribute { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TemplateAttribute()
        {
        }
    }
}
