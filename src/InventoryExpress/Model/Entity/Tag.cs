using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<InventoryTag> InventoryTag { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Tag()
        {
        }
    }
}
