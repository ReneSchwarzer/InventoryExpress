using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The attribut.
    /// </summary>
    internal class Attribute : Item
    {
        /// <summary>
        /// Returns or sets the template attributes.
        /// </summary>
        public virtual ICollection<TemplateAttribute> TemplateAttributes { get; set; }

        /// <summary>
        /// Returns or sets the inventory attributes.
        /// </summary>
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
