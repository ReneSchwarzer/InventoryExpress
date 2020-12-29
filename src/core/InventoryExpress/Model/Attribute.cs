using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Vorlagenattribute
    /// </summary>
    [Table("ATTRIBUTE")]
    public class Attribute : Item
    {
        public virtual ICollection<InventoryAttribute> InventoryAttributes { get; set; }
        public virtual ICollection<TemplateAttribute> TemplateAttributes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Attribute()
            : base()
        {
            InventoryAttributes = new HashSet<InventoryAttribute>();
            TemplateAttributes = new HashSet<TemplateAttribute>();
        }
    }
}
