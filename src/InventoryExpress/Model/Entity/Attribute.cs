using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Vorlagenattribute
    /// </summary>
    public class Attribute : Item
    {
        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public virtual ICollection<TemplateAttribute> TemplateAttributes { get; set; }
        public virtual ICollection<InventoryAttribute> InventoryAttributes { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Attribute()
            : base()
        {
            //InventoryAttributes = new HashSet<InventoryAttribute>();
            //TemplateAttributes = new HashSet<TemplateAttribute>();
        }
    }
}
