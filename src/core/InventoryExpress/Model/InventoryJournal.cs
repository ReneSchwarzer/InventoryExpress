using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Journal eines Inventars
    /// </summary>
    [Table("INVENTORYJOURNAL")]
    public partial class InventoryJournal
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
        /// Die Aktion
        /// </summary>
        [Column("ACTION")]
        public string Action { get; set; }

        /// <summary>
        /// Parameter der Aktion
        /// </summary>
        [Column("ACTIONPARAM")]
        public string ActionParam { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [Column("CREATED")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Die GUID
        /// </summary>
        [Column("GUID")]
        public string Guid { get; set; }

        public virtual Inventory Inventory { get; set; }
    }
}
