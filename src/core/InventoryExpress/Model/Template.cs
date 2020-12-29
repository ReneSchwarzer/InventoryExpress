using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Vorlage
    /// </summary>
    [Table("TEMPLATE")]
    public class Template : ItemTag
    {
        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public virtual ICollection<TemplateAttribute> TemplateAttributes { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Template()
            : base()
        {
            Inventories = new HashSet<Inventory>();
            TemplateAttributes = new HashSet<TemplateAttribute>();
        }
    }
}
