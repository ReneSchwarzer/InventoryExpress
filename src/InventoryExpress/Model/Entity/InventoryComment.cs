using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The comments of an inventory.
    /// </summary>
    [Table("INVENTORYCOMMENT")]
    public partial class InventoryComment
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the id of the inventory.
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Returns or sets the comment.
        /// </summary>
        [Column("COMMENT")]
        public string Comment { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        [Column("CREATED")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the last change.
        /// </summary>
        [Column("UPDATED")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Returns or sets the guid.
        /// </summary>
        [Column("Guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Returns or sets the inventory.
        /// </summary>
        public virtual Inventory Inventory { get; set; }
    }
}
