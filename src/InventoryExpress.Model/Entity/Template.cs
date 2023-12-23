using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The template.
    /// </summary>
    internal class Template : ItemTag
    {
        /// <summary>
        /// Returns or sets the template attributes.
        /// </summary>
        public virtual ICollection<TemplateAttribute> TemplateAttributes { get; set; }

        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Template()
            : base()
        {
        }
    }
}
