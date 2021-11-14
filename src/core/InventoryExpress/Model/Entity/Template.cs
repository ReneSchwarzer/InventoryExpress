using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Vorlage
    /// </summary>
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
        }
    }
}
