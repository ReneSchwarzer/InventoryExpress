using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Kommentare eines Inventars
    /// </summary>
    [Table("INVENTORYCOMMENT")]
    public partial class InventoryComment
    {
        /// <summary>
        /// Die ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Das Inventar
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Der Wert
        /// </summary>
        [Column("COMMENT")]
        public string Comment { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [Column("CREATED")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        [Column("UPDATED")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Die GUID
        /// </summary>
        [Column("GUID")]
        public string Guid { get; set; }

        public virtual Inventory Inventory { get; set; }
    }
}
